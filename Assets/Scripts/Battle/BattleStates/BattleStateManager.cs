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
        // States
        public BattleState currentBattleState { get; private set; }
        public Dictionary<Type, BattleState> battleStates { get; private set; }

        #region delegate
        public delegate void ActionPointUpdated(int remaining, int maxPoint);
        public ActionPointUpdated actionPointUpdated;

        public Action<List<Being>> displayMenu;
        public System.Action hideMenu;

        public delegate List<Being> RequestActor(bool includeDead);
        public RequestActor requestCharacters;
        public RequestActor requestEnemies;

        public delegate List<BattleTarget> RequestValidTarget(ActorType actorType, Action action);
        public RequestValidTarget requestValidTarget;

        public delegate BattleSpawningPoint GetBattleSpawn(Being forActor);
        public GetBattleSpawn getBattleSpawn;
        #endregion

        public void startBattle()
        {
            // init battle state
            BattleState.init(this);
            initBattleStates();

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

        /// <summary> End the current turn </summary>
        public void endTurn()
        {
            currentBattleState.end();
            
        }

        /// <summary> Change state and init what need to be inited </summary>
        /// <param name="battleStateType"></param>
        public void changeState(Type battleStateType)
        {
            if (updateCurrentState(battleStateType))
            {
                if (battleStateType == typeof(PlayerTurn))
                    currentBattleState.setActors(requestCharacters(true));
                else if (battleStateType == typeof(EnemyTurn))
                    currentBattleState.setActors(requestEnemies(false));

                currentBattleState.start();
            }
            else
                Debug.LogErrorFormat("State {0} not found !", battleStateType);
        }

        // used to react to the menu delegate
        // and transmit it to the current state
        #region Set current stat attribut returned from menu
        public void setActor(Being actor) { currentBattleState.setChoosedActor(actor); }
        public void setAction(Action action) { (currentBattleState as ActorTurnBattleState).setActionInUse(action); }
        public void setTarget(BattleTarget target) { (currentBattleState as ActorTurnBattleState).useAction(target); }
        #endregion

        public bool isPlayerTurn() { return currentBattleState is PlayerTurn; }
    }
}
