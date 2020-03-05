using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Battle.StateMachine
{
    public class PlayerTurn : ActorTurnBattleState
    {

        public override void start()
        {
            _battleManager.displayMenu(true);
        }

        public override void executeState()
        {
            endTurn();
        }

        public override void useAction(BattleTarget target, BattleSpawningPoint senderSpawn)
        {
            animateMovement(actionInUse, target, senderSpawn);
        }

        public override void endTurn()
        {
            _battleManager.resetMenu();
            _battleStateManager.battleOrder.updateBattleOrder();
            changeStateBasedOnOrderList();
        }
    }
}