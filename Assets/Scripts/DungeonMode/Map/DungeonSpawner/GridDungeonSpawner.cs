using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RPG.DungeonMode.Dungeon
{

    public partial class GridDungeonSpawner : MonoBehaviour, IDungeonSpawner
    {
        [SerializeField] Transform _roomParent;

        public void spawnRooms(RoomData[,] rooms)
        {
            for (int i = 0; i < rooms.GetLength(0); i++)
            {
                for (int j = 0; j < rooms.GetLength(1); j++)
                {
                    RoomData roomData = rooms[i, j];
                    if(roomData != null)
                        spawn(i, j, roomData, roomData.prefab);
                }
            }
        }

        GameObject spawn(int heightIndex, int widthIndex, RoomData roomToSpawn, GameObject prefab)
        {
            Terrain terrain = prefab.GetComponent<Terrain>();
            RoomGameObject prefabRoomGameObject = prefab.GetComponent<RoomGameObject>();

            if (terrain == null || prefabRoomGameObject == null)
            {
                Debug.LogWarningFormat("{0} does not have a RoomGameObject or Terrain script attached", roomToSpawn.name);
                return null;
            }

            Vector3 size = terrain.terrainData.size;
            GameObject spawnedRoom = Instantiate(prefab, new Vector3(widthIndex * size.z, 0,  heightIndex * size.x), Quaternion.identity, _roomParent);

            if(spawnedRoom != null)
            {
                // assign the roomData to the RoomGameObject script
                RoomGameObject roomGameObject = spawnedRoom.GetComponent<RoomGameObject>();
                roomGameObject.setRoomData(roomToSpawn);
                roomToSpawn.setGameObject(spawnedRoom);
            }

            return spawnedRoom;
        }
    }
}
