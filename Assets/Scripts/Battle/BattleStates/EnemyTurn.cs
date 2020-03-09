
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG.Battle.StateMachine
{
    using RPG.DataManagement;

    public class EnemyTurn : ActorTurnBattleState
    {

        public override void start()
        {
            _battleStateManager.StartCoroutine(executeEnemyAction());
        }

        public override void executeState()
        {
            
        }

        IEnumerator executeEnemyAction()
        {
            foreach(Being enemy in currentActors)
            {
                isAnimatingAction = true;
                setActionInUse(enemy.actions.getRandomElement());
                List<BattleTarget> valideTargets = _battleStateManager.battleActorHandler.getValidCharacterTargets(actionInUse);
                BattleTarget target = valideTargets.getRandomElement();
                setChoosedActor(enemy);
                useAction(target);
                yield return new WaitUntil(() => isAnimatingAction);
            }
            endTurn();
        }

        public override void useAction(BattleTarget target)
        {
            animateMovement(actionInUse, target, _battleStateManager.battleActorHandler.getSpawningPoint(choosedActor));
        }

        public override void endTurn()
        {
            ChangeState(typeof(PlayerTurn));
        }
    }
}