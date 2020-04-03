using UnityEngine;
using System.Collections;
using RPG.DungeonMode.Dungeon;
using System.Collections.Generic;
using RPG.Data.Team;
using RPG.Data;
using RPG.DungeonMode.Map;

namespace RPG.DungeonMode
{
    public class DungeonManager : MonoBehaviour
    {

        public static DungeonManager instance;

        [Header("Databases")]
        [SerializeField] CharacterDatabase _characterDatabase;
        [SerializeField] EnemyDatabase _enemyDatabase;
        [SerializeField] DungeonRoomDatabase _dungeonRoomDatabase;

        // private var
        int _dungeonSeed = 0;
        Room _currentRoom;

        IDungeonGeneration _dungeonGenerator;
        IDungeonSpawner _dungeonSpawner;
        MapDisplayer _mapDisplayer;

        List<Team> team;

        private void Awake()
        {
            _dungeonGenerator = GetComponent<IDungeonGeneration>();
            _mapDisplayer = GetComponent<MapDisplayer>();
            _dungeonSpawner = GetComponent<IDungeonSpawner>();
            instance = this;
        }

        // Use this for initialization
        void Start()
        {

            // createDungeon
            _currentRoom = _dungeonGenerator.createDungeon(_dungeonRoomDatabase, 12, 15, _dungeonSeed);
            _dungeonSpawner.spawnRooms(_dungeonGenerator.rooms);

            // display map
            _mapDisplayer.roomChosed = moveToNextRoom;
            _mapDisplayer.displayMap(_dungeonGenerator.rooms, _currentRoom);

            _currentRoom.mapItem.GetComponent<RoomMapItem>().isCurrentRoom(true);
        }


        void moveToNextRoom(Room nextRoom)
        {
            if (!_currentRoom.linkedRoom.Contains(nextRoom))
            {
                
                Debug.LogWarning("INSERT MESSAGE TO THE PLAYER, OR ANY VISUAL INDICATION");
                return;
            }

            _currentRoom.mapItem.GetComponent<RoomMapItem>().haveBeenCleared(true);
            _currentRoom = nextRoom;
            _currentRoom.mapItem.GetComponent<RoomMapItem>().isCurrentRoom(true);
            

            // Moving to the next room code
            Debug.Log("Can move");
        }

        
    }
}

