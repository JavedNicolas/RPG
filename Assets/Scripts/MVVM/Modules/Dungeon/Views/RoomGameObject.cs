using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RPG.DungeonModule.View
{
    public class RoomGameObject : CustomGameObject
    {
        [SerializeField] StartPointZone _startPointZone;
        [SerializeField] Vector3 cameraOffset;
        public Vector3 startZoneCenter => _startPointZone.getCenterPosition();
        public List<GameObject> startPoints => _startPointZone.actorSpawningPoints;

        public Vector3 cameraPosition()
        {
            Vector3 size = getSize();
            float x = transform.position.x + (size.x / 2) + cameraOffset.x;
            float y = transform.position.y + 20 + cameraOffset.y;
            float z = transform.position.z + size.z + cameraOffset.z;

            return new Vector3(x, y, z);
        }

        public void rotateStartZone(Vector3 comingFrom)
        {
            _startPointZone?.rotateZone(comingFrom);
        }

    }

}
