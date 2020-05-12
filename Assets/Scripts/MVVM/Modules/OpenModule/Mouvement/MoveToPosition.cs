using UnityEngine;
using System.Collections;

namespace RPG.OpenModule.View
{

    public class MoveToPosition : MonoBehaviour, ITranslation
    {
        Transform _transformToMove;
        [SerializeField] float _speed;
        float _distantToStop;
        Vector3 _destination;
        public bool hasFinishedHisMovement { get; private set; }

        private void Start()
        {
            hasFinishedHisMovement = true;
        }

        public void startMovement(Vector3 destination, float distanceToStop = 3)
        {
            _distantToStop = distanceToStop;
            _destination = destination;
            hasFinishedHisMovement = false;
        }

        public Vector3 getDirection()
        {
            return hasFinishedHisMovement ? Vector3.zero : (_destination - _transformToMove.position).normalized;
        }

        public float getSpeed()
        {
            return _speed;
        }

        public void move(Transform transform)
        {
            _transformToMove = transform;
            if (!hasFinishedHisMovement)
                transform.position += getDirection() * getSpeed() * Time.deltaTime;
            else
                return;

            if (Vector3.Distance(transform.position, _destination) <= _distantToStop)
                hasFinishedHisMovement = true;
        }
    }

}