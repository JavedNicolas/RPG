

using RPG.DataManagement;
using UnityEngine;

namespace RPG.Battle.StateMachine
{
    public class PlayerTurn : ActorTurnBattleState
    {
        public override void start()
        {
            executeState();
        }

        public override void executeState()
        {
            _battleStateManager.battleMenu.displayMenu(currentActors);
        }

        public override void useAction(BattleTarget target)
        {
            animateMovement(actionInUse, target, _battleStateManager.battleActorHandler.getSpawningPoint(choosedActor as Being));
        }

        public override void endTurn()
        {
            ChangeState(typeof(EnemyTurn));
        }
    }
}