using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using RPG.DataManagement;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace RPG.Battle.StateMachine
{
    public abstract class BattleState
    {
        protected static BattleStateManager _battleStateManager { get; private set; }

        protected List<Being> currentActors;
        protected Being choosedActor;

        /// <summary>
        /// init the battle manager instance for each battle state
        /// </summary>
        /// <param name="battleStateManager"></param>
        public static void init(BattleStateManager battleStateManager)
        {
            _battleStateManager = battleStateManager;
        }

        /// <summary>
        /// init the state
        /// </summary>
        public abstract void start();

        /// <summary>
        /// Contain execute logic of the state
        /// </summary>
        public abstract void executeState();
        public abstract void useAction(BattleTarget target);
        public abstract void endTurn();

        public virtual void setActors(List<Being> actors) { currentActors = actors; }
        public virtual void setChoosedActor(Being actor) { choosedActor = actor; }

        protected virtual void ChangeState(Type stateType)
        {
            _battleStateManager.changeState(stateType);
        }
    }
}
