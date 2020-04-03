using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RPG.Data;

namespace RPG.DungeonMode.Dungeon
{
    public class GridDungeonGenerator : MonoBehaviour, IDungeonGeneration
    {
        [SerializeField] [Range(0,100)]int _chanceToBranch;
        [Tooltip("Is a factor which reduced the chance to branch based on number of branched room")]
        [SerializeField] [Range(0, 30)] int _chanceToBranchReductionFactor;

        RoomData[,] _rooms;
        public RoomData[,] rooms => _rooms;

        public void createDungeon(DungeonRoomDatabase roomDatabase, int minSize, int maxSize, int seed)
        {
            int dungeonSize = Random.Range(minSize, maxSize);

            _rooms = new RoomData[5, dungeonSize];

            int dungeonMainLaneWidthStart = Random.Range(0 , _rooms.GetLength(1) / 2 );
            int dungeonMainLaneHeightStart = Random.Range(0, _rooms.GetLength(0));

            // fetch start and boss room
            RoomData startRoom = new RoomData(roomDatabase.getStaticRoom("Start Room"));
            RoomData bossRoom = new RoomData(roomDatabase.getStaticRoom("Boss Room"));

            // set the start and boss room
            _rooms[dungeonMainLaneHeightStart, dungeonMainLaneWidthStart] = startRoom;
            _rooms[dungeonMainLaneHeightStart, _rooms.GetLength(1) - 1] = bossRoom;

            // create the main lane
            for (int i = dungeonMainLaneWidthStart + 1; i < _rooms.GetLength(1) - 1; i++)
            {
                _rooms[dungeonMainLaneHeightStart, i] = new RoomData(getRandomRoom(roomDatabase));

                // ad main lane links
                _rooms[dungeonMainLaneHeightStart, i].linkedRoom.Add(_rooms[dungeonMainLaneHeightStart, i - 1]);
                _rooms[dungeonMainLaneHeightStart, i - 1].linkedRoom.Add(_rooms[dungeonMainLaneHeightStart, i]);
            }

            // link the boss room
            _rooms[dungeonMainLaneHeightStart, _rooms.GetLength(1) - 2].linkedRoom.Add(_rooms[dungeonMainLaneHeightStart, _rooms.GetLength(1) - 1]);
            _rooms[dungeonMainLaneHeightStart, _rooms.GetLength(1) - 1].linkedRoom.Add(_rooms[dungeonMainLaneHeightStart, _rooms.GetLength(1) - 2]);

            // create the room branches
            for (int i = 0; i < _rooms.GetLength(0); i++)
            {
                for (int j = 0; j < _rooms.GetLength(1); j++)
                {
                    if(_rooms[i, j] != null)
                        getNextRoom(i, j, roomDatabase);
                }
            }
        }

        RoomScriptableObject getRandomRoom(DungeonRoomDatabase roomDatabase)
        {
            System.Predicate<RoomScriptableObject> filter = new System.Predicate<RoomScriptableObject>(x => !x.isStaticRoom);
            return roomDatabase.getRandomElement(filter);
        }

        /// <summary> 
        /// Generate the next room for this roomTemplate if it's null (which mean there is no room attributed).
        /// This function is recurcive and will create a succession of room will the random chance to get
        /// an other room is true
        /// </summary>
        /// <returns></returns>
        void getNextRoom(int parentHeightIndex, int parentWidthIndex, DungeonRoomDatabase roomDatabase, int numberOfBranchedRoom = 0)
        {
            int newChanceToBranch = 100 - (_chanceToBranch - (numberOfBranchedRoom * _chanceToBranchReductionFactor));

            if (_rooms[parentHeightIndex, parentWidthIndex].cannotBranch)
                return;

            foreach (RoomDirection direction in System.Enum.GetValues(typeof(RoomDirection)))
            {
                if (Random.Range(0, 100) > newChanceToBranch)
                {
                    // get the new room index and check if it's in the grid bound
                    (int heightIndex, int widthIndex) nextRoomIndex = getNextRoonIndex(parentHeightIndex, parentWidthIndex, direction);
                    if (_rooms.GetLength(0) <= nextRoomIndex.heightIndex || nextRoomIndex.heightIndex < 0 || _rooms.GetLength(1) <= nextRoomIndex.widthIndex || nextRoomIndex.widthIndex < 0)
                        return;

                    // if there is no other other already on this spot, attribut the new room
                    if(_rooms[nextRoomIndex.heightIndex, nextRoomIndex.widthIndex] == null)
                    {
                        numberOfBranchedRoom++;

                        RoomScriptableObject room = getRandomRoom(roomDatabase);
                        RoomData newRoomData = new RoomData(room);

                        // assign links
                        _rooms[parentHeightIndex, parentWidthIndex].linkedRoom.Add(newRoomData);
                        newRoomData.linkedRoom.Add(_rooms[parentHeightIndex, parentWidthIndex]);

                        _rooms[nextRoomIndex.heightIndex, nextRoomIndex.widthIndex] = newRoomData;
                        getNextRoom(nextRoomIndex.heightIndex, nextRoomIndex.widthIndex, roomDatabase);
                    }
                }
            }
        }

        (int heightIndex, int widthIndex) getNextRoonIndex(int parentRoomHeightIndex, int parentRoomWidthIndex, RoomDirection newRoomDirection)
        {
            int heightIndex = -1;
            int widthIndex = -1;
            switch (newRoomDirection)
            {
                case RoomDirection.Top: 
                    heightIndex = parentRoomHeightIndex - 1;
                    widthIndex = parentRoomWidthIndex;
                    break;
                case RoomDirection.Right:
                    heightIndex = parentRoomHeightIndex;
                    widthIndex = parentRoomWidthIndex + 1;
                    break;
                case RoomDirection.Bottom:
                    heightIndex = parentRoomHeightIndex + 1;
                    widthIndex = parentRoomWidthIndex;
                    break;
                case RoomDirection.Left:
                    heightIndex = parentRoomHeightIndex;
                    widthIndex = parentRoomWidthIndex - 1;
                    break;
            }

            return (heightIndex: heightIndex, widthIndex: widthIndex);
        }
    }



}