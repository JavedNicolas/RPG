using UnityEngine;
using System.Collections;
using RPG.DungeonMode.Map;
using System.Collections.Generic;
using RPG.Data.Team;
using RPG.Data;

namespace RPG.DungeonMode
{
    public class DungeonManager : MonoBehaviour
    {
        int _dungeonSeed = 0;

        public static DungeonManager instance;
        public static DungeonMap map;
        public List<Team> team;

        [SerializeField] GameObject _startZone;

        [Header("Databases")]
        [SerializeField] CharacterDatabase _characterDatabase;
        [SerializeField] EnemyDatabase _enemyDatabase;
        [SerializeField] DungeonRoomDatabase _dungeonRoomDatabase;

        private void Awake()
        {
            instance = this;
        }

        // Use this for initialization
        void Start()
        {
            map = new DungeonMap();
            map.createMap(_dungeonRoomDatabase, 1, 10, _dungeonSeed);
            spawnRooms();
        }

        void spawnRooms()
        {
            List<Room> rooms = map.rooms;
            GameObject lastRoomsSpawn = _startZone;

            for (int i = 0; i < rooms.Count; i++)
            {
                GameObjectExtender extender = lastRoomsSpawn.GetComponent<GameObjectExtender>();
                
                GameObject prefab = rooms[i].prefab;
                Terrain terrain = prefab.GetComponent<Terrain>();

                if (extender == null || terrain == null)
                {
                    Debug.LogWarningFormat("{0} does not have a GameObjectExtender script attached or a Terrain", lastRoomsSpawn.name);
                    continue;
                }

                Vector3 size = terrain.terrainData.size;
                lastRoomsSpawn = extender.spawnNewObjectFromPrefab(Vector3.right, size, prefab);
            }
        }
    }
}

