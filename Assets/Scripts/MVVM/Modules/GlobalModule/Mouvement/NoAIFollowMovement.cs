using UnityEngine;
using System.Collections;

namespace RPG.GlobalModule.View
{
    public class NoAIFollowMovement : MonoBehaviour, ITranslation
    {
        [SerializeField] Transform _transformToFollow;
        [SerializeField] Vector3 _distanceToKeep;
        [SerializeField] float _smoothingFactor = 2f;

        public Vector3 getDirection()
        {
            return Vector3.zero;
        }

        public float getSpeed()
        {
            return 0f;
        }

        public void move(Transform transform)
        {
            transform.position = Vector3.Lerp(transform.position, _transformToFollow.position + _distanceToKeep, Time.deltaTime * _smoothingFactor);
        }
    }

}
