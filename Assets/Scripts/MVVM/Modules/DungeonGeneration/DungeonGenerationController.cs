using UnityEngine;
using System.Collections;
using RPG.DataModule;
using RPG.DungeonGenerationModule.View;
using RPG.DataModule.ViewModel;
using System.Collections.Generic;

namespace RPG.DungeonGenerationModule 
{
    public class DungeonGenerationController : MonoBehaviour
    {
        public GridDungeon dungeon { get; private set; }
        public GridDungeonSpawner dungeonSpawner { get; private set; }

        [Header("Dungeon Generation")]
        [SerializeField] DungeonRoomDatabase _dungeonRoomDatabase;
        [SerializeField] int _minSize, _maxSize, _seed;

        [Header("Dungeon prefabs")]
        [SerializeField] List<GameObject> _roomPrefabs = new List<GameObject>();
        [SerializeField] List<GameObject> _emptyRoomPrefabs = new List<GameObject>();

        private void Awake()
        {
            dungeon = new GridDungeon();
            dungeonSpawner = GetComponent<GridDungeonSpawner>();
        }

        public void generateDungeon()
        {
            dungeon.generate(_dungeonRoomDatabase, _minSize, _maxSize, _seed);

            RoomViewModel roomViewModel = new RoomViewModel();
            GameObject[,] roomPrefabs = roomViewModel.getGridDungeonRoomsPrefabs(dungeon.rooms, _roomPrefabs, _emptyRoomPrefabs);
            dungeonSpawner.spawnRooms(roomPrefabs);
        }

    }

}
