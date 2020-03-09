using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG.Battle.StateMachine
{
    using RPG.Battle.UI;
    using RPG.DataManagement;

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

        private void initBattleStates()
        {
            battleStates = new Dictionary<Type, BattleState>();
            battleStates.Add(typeof(BattleStartState), new BattleStartState());
            battleStates.Add(typeof(PlayerTurn), new PlayerTurn());
            battleStates.Add(typeof(EnemyTurn), new EnemyTurn());
        }

        private bool updateCurrentState(Type battleStateType)
        {
            BattleState battleState = battleStates[battleStateType];
            if (battleState == null)
                return false;

            currentBattleState = battleState;
            return true;
        }

        private List<BattleTarget> getValidTarget(Action action){ return _battleActorHandler.getValidEnemyTargets(action); }

        public void endTurn()
        {
            currentBattleState.endTurn();
            _battleMenu.endTurnButton.gameObject.SetActive(false);
        }

        public void changeState(Type battleStateType)
        {
            if (updateCurrentState(battleStateType))
            {
                if (battleStateType == typeof(PlayerTurn))
                {
                    _battleMenu.endTurnButton.gameObject.SetActive(true);
                    currentBattleState.setActors(_battleActorHandler.getCharacters());
                }
                else if (battleStateType == typeof(EnemyTurn))
                    currentBattleState.setActors(_battleActorHandler.getEnemies());

                currentBattleState.start();
            }
            else
                Debug.LogErrorFormat("State {0} not found !", battleStateType);
        }

        #region Set current stat attribut returned from menu
        private void setActor(Being actor) { currentBattleState.setChoosedActor(actor); }
        private void setAction(Action action) { (currentBattleState as ActorTurnBattleState).setActionInUse(action); }
        private void useAction(BattleTarget target) { currentBattleState.useAction(target); }
        #endregion
    }
}
