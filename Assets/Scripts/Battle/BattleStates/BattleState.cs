using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using RPG.Data;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace RPG.Battle.StateMachine
{
    using RPG.State;

    public abstract class BattleState : State
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

        public virtual void setActors(List<Being> actors) { currentActors = actors; }
        public virtual void setChoosedActor(Being actor) { choosedActor = actor; }

        /// <summary> switch state </summary>
        /// <param name="stateType"></param>
        protected virtual void ChangeState(Type stateType)
        {
            _battleStateManager.changeState(stateType);
        }
    }
}
