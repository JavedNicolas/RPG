using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG.Battle.StateMachine
{
    using RPG.Battle.UI;
    using RPG.Data;

    public class BattleStateManager : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] BattleMenu _battleMenu;
        public BattleMenu battleMenu => _battleMenu;

        [Header("Battle")]
        [SerializeField] BattleActorHandler _battleActorHandler;
        public BattleActorHandler battleActorHandler => _battleActorHandler;

        // States
        public BattleState currentBattleState { get; private set; }
        public Dictionary<Type, BattleState> battleStates { get; private set; }

        public void Start()
        {
            // init battle state
            BattleState.init(this);
            initBattleStates();

            // init the menu
            _battleMenu.initMenus();
            _battleMenu.actorChoosed = setActor;
            _battleMenu.actionChoosed = setAction;
            _battleMenu.requestValidTarget = getValidTarget;
            _battleMenu.targetChoosen = useAction;

            // spawn actors
            _battleActorHandler.spawnActors();
            changeState(typeof(BattleStartState));
        }

        /// <summary> fill the battleStates array with all the available states </summary>
        private void initBattleStates()
        {
            battleStates = new Dictionary<Type, BattleState>();
            battleStates.Add(typeof(BattleStartState), new BattleStartState());
            battleStates.Add(typeof(PlayerTurn), new PlayerTurn());
            battleStates.Add(typeof(EnemyTurn), new EnemyTurn());
            battleStates.Add(typeof(PlayerWonState), new PlayerWonState());
            battleStates.Add(typeof(PlayerLostState), new PlayerLostState());
        }

        /// <summary> update the current state variable </summary>
        /// <returns> return true if the state exist, return false if not</returns>
        private bool updateCurrentState(Type battleStateType)
        {
            BattleState battleState = battleStates[battleStateType];
            if (battleState == null)
                return false;

            currentBattleState = battleState;
            return true;
        }

        /// <summary> get a valid target from the enemy list. Used by the BattleTargetSelector to know which target can be selected</summary>
        /// <param name="action">Valid target for this action</param>
        /// <returns>A list a valid battle target</returns>
        private List<BattleTarget> getValidTarget(Action action){ return _battleActorHandler.getValidEnemyTargets(action); }

        /// <summary> End the current turn </summary>
        public void endTurn()
        {
            currentBattleState.endTurn();
            _battleMenu.hideMenu();
            _battleMenu.endTurnButton.gameObject.SetActive(false);
        }

        /// <summary> Change state and init what need to be inited </summary>
        /// <param name="battleStateType"></param>
        public void changeState(Type battleStateType)
        {
            if (updateCurrentState(battleStateType))
            {
                if (battleStateType == typeof(PlayerTurn))
                {
                    _battleMenu.endTurnButton.gameObject.SetActive(true);
                    currentBattleState.setActors(_battleActorHandler.getCharacters(true));
                }
                else if (battleStateType == typeof(EnemyTurn))
                    currentBattleState.setActors(_battleActorHandler.getEnemies());

                currentBattleState.start();
            }
            else
                Debug.LogErrorFormat("State {0} not found !", battleStateType);
        }

        // used to react to the menu delegate
        // and transmit it to the current state
        #region Set current stat attribut returned from menu
        private void setActor(Being actor) { currentBattleState.setChoosedActor(actor); }
        private void setAction(Action action) { (currentBattleState as ActorTurnBattleState).setActionInUse(action); }
        private void useAction(BattleTarget target) { (currentBattleState as ActorTurnBattleState).useAction(target); }
        #endregion

    }
}
