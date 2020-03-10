

using RPG.Data;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Battle.StateMachine
{
    public class PlayerTurn : ActorTurnBattleState
    {
        int maxActionPoint;
        public int remainingActionPoint { get; private set; }

        public override void start()
        {
            // init action points
            maxActionPoint = 0;
            currentActors.ForEach(x => 
            {
                if(!x.isDead())
                    maxActionPoint += ((Character)x).actionPoint;
            });
            remainingActionPoint = maxActionPoint;

            // update the action point display
            updateActionPointDisplay();
            executeState();
        }

        public override void executeState()
        {
            // if there is no more enemy switch to player won
            if (_battleStateManager.battleActorHandler.getEnemies().Count == 0)
            {
                ChangeState(typeof(PlayerWonState));
                return;
            }

            // if there is no more action point hide the menu
            if (remainingActionPoint == 0)
            {
                _battleStateManager.battleMenu.hideMenu();
                return;
            }
                

            _battleStateManager.battleMenu.displayMenu(currentActors);
        }

        public override void useAction(BattleTarget target)
        {
            // remove the action point consummed
            remainingActionPoint -= actionInUse.cost;
            updateActionPointDisplay();

            // animate the action
            animateMovement(actionInUse, target, _battleStateManager.battleActorHandler.getSpawningPoint(choosedActor as Being));
        }

        /// <summary> update the action points display </summary>
        private void updateActionPointDisplay()
        {
            _battleStateManager.battleMenu.updateActionPointDisplay(remainingActionPoint, maxActionPoint);
        }

        public override void endTurn()
        {
            ChangeState(typeof(EnemyTurn));
        }
    }
}