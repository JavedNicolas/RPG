
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG.Battle.StateMachine
{
    using RPG.Data;

    public class EnemyTurn : ActorTurnBattleState
    {
        int _currentEnemyIndex;

        public override void start()
        {
            _currentEnemyIndex = 0;
            executeState();
        }

        public override void executeState()
        {
            if (_currentEnemyIndex < currentActors.Count)
                executeEnemyAction(_currentEnemyIndex);
            else
                endTurn();
        }

        void executeEnemyAction(int index)
        {
            Being enemy = currentActors[index];
            setActionInUse(enemy.actions.getRandomElement());
            List<BattleTarget> valideTargets = _battleStateManager.battleActorHandler.getValidCharacterTargets(actionInUse);
            BattleTarget target = valideTargets.getRandomElement();
            setChoosedActor(enemy);
            useAction(target);
            _currentEnemyIndex++;
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