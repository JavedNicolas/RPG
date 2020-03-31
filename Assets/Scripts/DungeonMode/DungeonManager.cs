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

        [Header("UI")]
        [SerializeField] MapDisplayer _mapDisplayer;

        [Header("Misc")]
        [SerializeField] DungeonRoomSpawner _roomSpawner;

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
            map.createMap(_dungeonRoomDatabase, 1, 4, _dungeonSeed);
            _roomSpawner.spawnRooms(map.rooms, _startZone);
            _mapDisplayer.displayMap(map.rooms);
        }

        
    }
}

