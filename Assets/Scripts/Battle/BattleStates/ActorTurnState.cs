using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using RPG.DataManagement;

namespace RPG.Battle.StateMachine
{
    public abstract class ActorTurnBattleState : BattleState
    {
        protected Action actionInUse;

        protected bool isAnimatingAction = true;

        public void setActionInUse(Action action) { actionInUse = action; }

        #region action animation
        /// <summary> Move Character to the correct position for his action animation </summary>
        protected void animateMovement(Action action, BattleTarget target, BattleSpawningPoint senderSpawn)
        {
            switch (action.GetAnimationType())
            {
                case ActionAnimationType.Melee:
                    IEnumerator coroutine = animateMeleeAction(action, target, senderSpawn);
                    _battleStateManager.StartCoroutine(coroutine);
                    break;
                case ActionAnimationType.Distance: break;
                default: break;
            }
        }

        private IEnumerator animateMeleeAction(Action action, BattleTarget target, BattleSpawningPoint senderSpawn)
        {
            
            Vector3 targetPosition = target.model.transform.position;
            BattleSpawningPoint battleSpawningPoint = senderSpawn;
            GameObject actorGO = battleSpawningPoint.actorGameObject;
            Quaternion originalRotation = actorGO.transform.rotation;

            MoveToPosition characterMovement = actorGO.GetComponent<MoveToPosition>();
            characterMovement.startMovement(targetPosition);
            yield return new WaitUntil(() => characterMovement.hasFinishedHisMovement);

            actorGO.GetComponent<Animator>().SetTrigger("Attack");

            yield return new WaitForSeconds(1.2f);

            characterMovement.startMovement(battleSpawningPoint.gameObject.transform.position, 1f);

            yield return new WaitUntil(() => characterMovement.hasFinishedHisMovement);
            actorGO.transform.rotation = originalRotation;
            isAnimatingAction = false;
            executeState();
        }
        #endregion
    }
}
