using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;


namespace RPG.Battle.StateMachine
{
    public class BattleStartState : BattleState
    {
        public override void start()
        {
            endTurn();
        }

        public override void executeState()
        {

        }

        public override void useAction(BattleTarget target)
        {

        }

        public override void endTurn()
        {
            ChangeState(typeof(PlayerTurn));
        }
    }
}
