using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace RPG.DungeonModule.View
{
    public class StartPointZone : MonoBehaviour
    {
        [Tooltip("Check if the ground object pivot is in  the center. " +
            "This will add an offset to the StartZone to align as if the pivot point was the corner.")]
        [SerializeField] bool _groundPivotIsCentered;
        [SerializeField] CustomGameObject customGameObject;
        [SerializeField] Vector3 _startPointsOffset;
        [Tooltip("A factor which will offset the spawning points from the center. This factor divide the zone width")]
        [SerializeField, Range(0, 100)] float _spawnPointsCenterPercentage;
        [SerializeField] Transform _transformToRotate;
        [SerializeField] Transform _characterZone;
        [SerializeField] float _spawnPerLine;
        [SerializeField] List<GameObject> _actorSpawningPoints = new List<GameObject>();
        public List<GameObject> actorSpawningPoints => _actorSpawningPoints;

        private void Start()
        {
            resizeZone();
        }

        [Button("Move The zone center point")]
        public void resizeZone()
        {
            // ground size
            Vector3 size = customGameObject.getSize();
            float _groundWidth = size.x;
            float _groundHeight = size.z;
            
            // ground
            GameObject ground = customGameObject.baseObject;

            // offset from the ground pivot point
            Vector3 centerOffset = Vector3.zero;
            if(_groundPivotIsCentered)
                centerOffset = new Vector3(_groundWidth / 2, 0, _groundHeight / 2);

            // Convert ground position to world pos (because ground and to transform to move do not share parents
            Vector3 groundWorldPos = ground.transform.parent.transform.TransformPoint(ground.transform.localPosition);

            // apply Offset to the ground position and apply it to the transform to move
            float x = _startPointsOffset.x - centerOffset.x +groundWorldPos.x;
            float y = _startPointsOffset.y + centerOffset.y + groundWorldPos.y;
            float z = _startPointsOffset.z - centerOffset.z + groundWorldPos.z;
            _characterZone.position = new Vector3(x, y, z);

            // center the transform to rotate (to allow it to rotate arround the center of the zone
            _transformToRotate.localPosition = new Vector3(_groundWidth / 2, _transformToRotate.localPosition.y ,_groundHeight / 2);

            // offset the spawning points from the transform to rotate center
            for (int i = 0; i < _actorSpawningPoints.Count; i++)
            {
                GameObject spawn = _actorSpawningPoints[i];
                float offset = 5f;
                float xOffset = (_spawnPointsCenterPercentage / 100) * (_groundWidth / 2) - ((i >= _spawnPerLine ? 1 : 0) * offset);
                spawn.transform.localPosition = new Vector3(-xOffset, spawn.transform.localPosition.y, spawn.transform.localPosition.z);
            }

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