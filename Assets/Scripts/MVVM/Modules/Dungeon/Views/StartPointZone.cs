using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace RPG.DungeonModule.View
{
    public class StartPointZone : MonoBehaviour
    {
        [SerializeField] float _zoneWidth;
        [SerializeField] float _zoneHeight;
        [Tooltip("A factor which will offset the spawning points from the center. This factor divide the zone width")]
        [SerializeField, Range(1, 10)] float _zoneCenterXOffsetFactor;
        [SerializeField] Transform _transformToRotate;
        [SerializeField] Transform _characterZone;
        [SerializeField] List<GameObject> _actorSpawningPoints = new List<GameObject>();
        public List<GameObject> actorSpawningPoints => _actorSpawningPoints;

        private void Start()
        {
            resizeZone();
        }

        [Button("Move The zone center point")]
        public void resizeZone()
        {
            _transformToRotate.localPosition = new Vector3(_zoneWidth / 2, _transformToRotate.localPosition.y ,_zoneHeight / 2);
            float centerXOffset = -_zoneWidth / _zoneCenterXOffsetFactor;
            _characterZone.localPosition = new Vector3(centerXOffset, 0, 0);
        }

        public void rotateZone(Vector3 comingFrom)
        {
            float yRotation = 0;

            if(comingFrom == Vector3.left)
                yRotation = 0;
            if (comingFrom == Vector3.forward)
                yRotation = 90;
            if (comingFrom == Vector3.right)
                yRotation = 180;
            if (comingFrom == Vector3.back)
                yRotation = -90;

            _transformToRotate.eulerAngles = new Vector3(0, yRotation, 0);
        }

        public Vector3 getCenterPosition() { return _transformToRotate.transform.position; }
    }
}