
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
            // if there is no more player switch to player lost
            if (_battleStateManager.battleActorHandler.getCharacters().Count == 0)
            {
                ChangeState(typeof(PlayerLostState));
                return;
            }
                
            // get and use action for the current enemy
            if (_currentEnemyIndex < currentActors.Count)
                executeEnemyAction(_currentEnemyIndex);
            else
                endTurn();
        }

        /// <summary> get an action for the current enemy and use it</summary>
        /// <param name="index"></param>
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

        /// <summary> use the action choosed </summary>
        /// <param name="target"></param>
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