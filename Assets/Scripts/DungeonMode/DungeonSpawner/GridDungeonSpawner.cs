using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RPG.DungeonMode.Dungeon
{
    public partial class GridDungeonSpawner : MonoBehaviour, IDungeonSpawner
    {
        [SerializeField] Transform _roomParent;
        [SerializeField] List<GameObject> _roomPrefabs = new List<GameObject>();

        public void spawnRooms(Room[,] rooms)
        {
            // spawn rooms
            for (int i = 0; i < rooms.GetLength(0); i++)
            {
                for (int j = 0; j < rooms.GetLength(1); j++)
                {
                    Room roomData = rooms[i, j];
                    if(roomData != null)
                    {
                        spawn(i, j, roomData);
                    }
                }
            }
        }

        GameObject spawn(int heightIndex, int widthIndex, Room roomToSpawn)
        {
            GameObject prefab = getFittingPrefab(roomToSpawn);

            RoomGameObject prefabRoomGameObject = prefab?.GetComponent<RoomGameObject>();

            if (prefabRoomGameObject == null)
            {
                Debug.LogWarningFormat("{0} does not have a RoomGameObject script attached", roomToSpawn.name);
                return null;
            }

            Vector3 size = prefabRoomGameObject.getSize();

            GameObject spawnedRoom = Instantiate(prefab, new Vector3(widthIndex * size.z, 0,  heightIndex * size.x), Quaternion.identity, _roomParent);

            if(spawnedRoom != null)
            {
                roomToSpawn.setGameObject(spawnedRoom);
            }

            return spawnedRoom;
        }

        /// <summary>
        /// get the fitting model based on the layout
        /// </summary>
        /// <param name="roomToSpawn"></param>
        /// <returns></returns>
        GameObject getFittingPrefab(Room roomToSpawn)
        {
            List<GameObject> prefabs = new List<GameObject>(roomToSpawn.scriptableObject.isSpecialRoom ? roomToSpawn.scriptableObject.prefabs : _roomPrefabs);

            List<GameObject> machtingPrefabs = new List<GameObject>();
            foreach(GameObject prefab in prefabs)
            {
                string[] splitName = prefab.name.Split('_');
                if(splitName.Length > 0
                    && splitName[0].containUnOrdered(roomToSpawn.linkedRoomString)
                    && splitName[0].Length == roomToSpawn.linkedRoomString.Length)
                    machtingPrefabs.Add(prefab);
            }

            return machtingPrefabs.getRandomElement();
        }
    }
}
