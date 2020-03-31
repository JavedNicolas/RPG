using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RPG.DungeonMode.Map
{
    public class DungeonRoomSpawner : MonoBehaviour
    {
        public void spawnRooms(List<List<Room>> rooms, GameObject startZone)
        {
            GameObject spawnerRoom = startZone;

            for (int i = 0; i < rooms.Count; i++)
            {
                GameObjectExtender extender = spawnerRoom.GetComponent<GameObjectExtender>();

                List<Room> choice = rooms[i];

                for (int j = 0; j < choice.Count; j++)
                {

                    GameObject prefab = choice[j].prefab;
                    Terrain terrain = prefab.GetComponent<Terrain>();

                    if (extender == null || terrain == null)
                    {
                        Debug.LogWarningFormat("{0} does not have a GameObjectExtender script attached or a Terrain", spawnerRoom.name);
                        continue;
                    }

                    Vector3 size = terrain.terrainData.size;

                    // assign the new spawner room only if it's the last to be spawn for this choice
                    GameObject spawnedRoom = extender.spawnNewObjectFromPrefab(Vector3.right, size, prefab);
                    spawnedRoom.SetActive((j == choice.Count - 1) ? true : false);
                    spawnerRoom = (j == choice.Count - 1) ? spawnedRoom : spawnerRoom;
                }
            }
        }
    }
}
