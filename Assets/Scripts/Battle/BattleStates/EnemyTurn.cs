
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG.Battle.StateMachine
{
    public class EnemyTurn : ActorTurnBattleState
    {
        public override void start()
        {
            _battleManager.displayMenu(false);
            getTargetAndAction();
        }

        public override void executeState()
        {
            endTurn();
        }

        public void getTargetAndAction()
        {
            List<BattleTarget> valideTargets = _battleManager.getCharacterValidtargetPoints(actionInUse);
            BattleTarget target = valideTargets.getRandomElement();
            actionInUse = currentActor.actions.getRandomElement();
            useAction(target, _battleManager.getActorSpawingPoint(currentActor));
        }

        public override void useAction(BattleTarget target, BattleSpawningPoint senderSpawn)
        {
            animateMovement(actionInUse, target, senderSpawn);
        }

        public override void endTurn()
        {
            _battleStateManager.battleOrder.updateBattleOrder();
            changeStateBasedOnOrderList();
        }
    }
}