﻿using UnityEngine;
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
            end();
        }

        public override void execute()
        {

        }

        public override void end()
        {
            ChangeState(typeof(PlayerTurn));
        }
    }
}
