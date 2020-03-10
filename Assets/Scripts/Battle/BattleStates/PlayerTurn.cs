

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
            maxActionPoint = 0;
            currentActors.ForEach(x => 
            {
                if(!x.isDead())
                    maxActionPoint += ((Character)x).actionPoint;
            });
            remainingActionPoint = maxActionPoint;

            updateActionPointDisplay();
            executeState();
        }

        public override void executeState()
        {
            _battleStateManager.battleMenu.displayMenu(currentActors);
        }

        public override void useAction(BattleTarget target)
        {
            remainingActionPoint -= actionInUse.cost;
            updateActionPointDisplay();
            animateMovement(actionInUse, target, _battleStateManager.battleActorHandler.getSpawningPoint(choosedActor as Being));
        }

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