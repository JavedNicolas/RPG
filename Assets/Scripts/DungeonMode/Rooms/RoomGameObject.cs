using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RPG.DungeonMode.Dungeon
{
    public class RoomGameObject : MonoBehaviour
    {
        [SerializeField] List<GameObject> _startPoints;
        [SerializeField] Terrain _terrain;
        [SerializeField] Vector3 cameraOffset;

        public Vector3 cameraPosition()
        {
            float x = transform.position.x + (_terrain.terrainData.bounds.size.x / 2) + cameraOffset.x;
            float y = transform.position.y + 20 + cameraOffset.y;
            float z = transform.position.z + (_terrain.terrainData.bounds.size.z) + cameraOffset.z;

            return new Vector3(x, y, z);
        }
    }

}
