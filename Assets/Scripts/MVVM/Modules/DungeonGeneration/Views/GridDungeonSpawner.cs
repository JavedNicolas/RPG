using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RPG.DungeonGenerationModule.View
{
    public partial class GridDungeonSpawner : MonoBehaviour
    {
        [SerializeField] Transform _roomParent;

        /// <summary>
        /// Spawn the room prefab to create the dungeons
        /// </summary>
        /// <param name="roomsPrefabs">The prefabs of the room to spawn orderr in a grid fashion</param>
        /// <returns> The list of room game Object spawned </returns>
        public GameObject[,] spawnRooms(GameObject[,] roomsPrefabs)
        {

            GameObject[,] spawnedRooms = new GameObject[roomsPrefabs.GetLength(0), roomsPrefabs.GetLength(1)];

            // spawn rooms
            for (int i = 0; i < roomsPrefabs.GetLength(0); i++)
            {
                for (int j = 0; j < roomsPrefabs.GetLength(1); j++)
                {
                    GameObject roomPrefab = roomsPrefabs[i, j];
                    spawnedRooms[i,j] = spawn(i, j, roomPrefab);
                }
            }

            return spawnedRooms;
        }

        /// <summary>
        /// Instatiate Room prefabs
        /// </summary>
        /// <param name="heightIndex"> the height index of the prefabs (used to position the gameObject in te scene) </param>
        /// <param name="widthIndex"> the width index of the prefabs (used to position the gameObject in te scene)</param>
        /// <param name="roomPrefabToSpawn">The prefabs to instantiate</param>
        /// <returns> The instantiated game object </returns>
        GameObject spawn(int heightIndex, int widthIndex, GameObject roomPrefabToSpawn)
        {
            GameObject prefab = roomPrefabToSpawn;

            CustomGameObject prefabRoomGameObject = prefab?.GetComponent<CustomGameObject>();

            if (prefabRoomGameObject == null)
            {
                Debug.LogWarningFormat("{0} does not have a RoomGameObject script attached", roomPrefabToSpawn?.name);
                return null;
            }

            Vector3 size = prefabRoomGameObject.getSize();

            GameObject spawnedRoom = Instantiate(prefab, new Vector3(widthIndex * size.z, 0,  heightIndex * size.x), Quaternion.identity, _roomParent);

            return spawnedRoom;
        }

    }
}
