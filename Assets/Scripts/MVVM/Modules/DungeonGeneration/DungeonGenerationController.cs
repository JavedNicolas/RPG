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
        [SerializeField] [Range(0, 100)] int _chanceToBranch;
        [Tooltip("Is a factor which reduced the chance to branch based on number of branched room")]
        [SerializeField] [Range(0, 30)] int _chanceToBranchReductionFactor;

        [Header("Dungeon prefabs")]
        [SerializeField] List<GameObject> _roomPrefabs = new List<GameObject>();
        [SerializeField] List<GameObject> _emptyRoomPrefabs = new List<GameObject>();

        private void Awake()
        {
            dungeon = new GridDungeon();
            dungeonSpawner = GetComponent<GridDungeonSpawner>();
        }


        /// <summary>
        /// Generate a new dungeon data
        /// </summary>
        /// <returns>The start room</returns>
        public Room generateDungeon()
        {
            Room startRoom = dungeon.generate(_dungeonRoomDatabase, _minSize, _maxSize,_chanceToBranch ,_chanceToBranchReductionFactor , _seed);

            RoomViewModel roomViewModel = new RoomViewModel();
            GameObject[,] roomPrefabs = roomViewModel.getGridDungeonRoomsPrefabs(dungeon.rooms, _roomPrefabs, _emptyRoomPrefabs);
            GameObject[,] rooomSpawned = dungeonSpawner.spawnRooms(roomPrefabs);

            // set the room gameobject attribut with the spawned room object 
            for (int i = 0; i < dungeon.rooms.GetLength(0); i++)
                for (int j = 0; j < dungeon.rooms.GetLength(1); j++)
                    if(dungeon.rooms[i,j] != null)
                        dungeon.rooms[i, j].setGameObject(rooomSpawned[i, j]);

            return startRoom;
        }

    }

}
