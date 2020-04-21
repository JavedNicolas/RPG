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

        Room[,] _rooms;
        public Room[,] rooms => _rooms;

        /// <summary>
        /// Generate a random dungeons
        /// </summary>
        /// <param name="roomDatabase">The database of rooms</param>
        /// <param name="minSize">Dungeon horizontal min size</pare">Dungeon horizontal max size</param>
        /// <param name="seed">am>
        /// <param name="maxSiz</param>
        /// <returns></returns>
        public Room createDungeon(DungeonRoomDatabase roomDatabase, int minSize, int maxSize, int seed)
        {
            int dungeonSize = Random.Range(minSize, maxSize);

            _rooms = new Room[5, dungeonSize];

            int dungeonMainLaneWidthStart = Random.Range(0 , _rooms.GetLength(1) / 2 );
            int dungeonMainLaneHeightStart = Random.Range(0, _rooms.GetLength(0));

            // fetch start and boss room
            Room startRoom = new Room(roomDatabase.getStaticRoom("Start Room"));
            Room bossRoom = new Room(roomDatabase.getStaticRoom("Boss Room"));

            // set the start and boss room
            _rooms[dungeonMainLaneHeightStart, dungeonMainLaneWidthStart] = startRoom;
            _rooms[dungeonMainLaneHeightStart, _rooms.GetLength(1) - 1] = bossRoom;

            // create the main lane
            for (int i = dungeonMainLaneWidthStart + 1; i < _rooms.GetLength(1) - 1; i++)
            {
                _rooms[dungeonMainLaneHeightStart, i] = new Room(getRandomRoom(roomDatabase));

                // ad main lane links
                _rooms[dungeonMainLaneHeightStart, i].addLinkedRoom(_rooms[dungeonMainLaneHeightStart, i - 1], RoomLinkDirection.Left);
                _rooms[dungeonMainLaneHeightStart, i - 1].addLinkedRoom(_rooms[dungeonMainLaneHeightStart, i], RoomLinkDirection.Right);
            }

            // link the boss room
            _rooms[dungeonMainLaneHeightStart, _rooms.GetLength(1) - 2].addLinkedRoom(_rooms[dungeonMainLaneHeightStart, _rooms.GetLength(1) - 1],RoomLinkDirection.Right);
            _rooms[dungeonMainLaneHeightStart, _rooms.GetLength(1) - 1].addLinkedRoom(_rooms[dungeonMainLaneHeightStart, _rooms.GetLength(1) - 2], RoomLinkDirection.Left);

            // create the room branches
            for (int i = 0; i < _rooms.GetLength(0); i++)
            {
                for (int j = 0; j < _rooms.GetLength(1); j++)
                {
                    if(_rooms[i, j] != null)
                        getNextRoom(i, j, roomDatabase);
                }
            }

            return startRoom;
        }

        /// <summary>
        /// Get a random room from the database
        /// </summary>
        /// <param name="roomDatabase"></param>
        /// <returns></returns>
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

            foreach (RoomLinkDirection direction in System.Enum.GetValues(typeof(RoomLinkDirection)))
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
                        Room newRoomData = new Room(room);

                        // assign links
                        _rooms[parentHeightIndex, parentWidthIndex].addLinkedRoom(newRoomData, direction);
                        newRoomData.addLinkedRoom(_rooms[parentHeightIndex, parentWidthIndex], getOppositeDirection(direction));
                        
                        _rooms[nextRoomIndex.heightIndex, nextRoomIndex.widthIndex] = newRoomData;
                        getNextRoom(nextRoomIndex.heightIndex, nextRoomIndex.widthIndex, roomDatabase);
                    }
                }
            }
        }

        /// <summary>
        /// Get the relevant grid array indexes based on the parent indexes and the direction of the new room
        /// </summary>
        /// <param name="parentRoomHeightIndex"></param>
        /// <param name="parentRoomWidthIndex"></param>
        /// <param name="newRoomDirection"></param>
        /// <returns></returns>
        (int heightIndex, int widthIndex) getNextRoonIndex(int parentRoomHeightIndex, int parentRoomWidthIndex, RoomLinkDirection newRoomDirection)
        {
            int heightIndex = -1;
            int widthIndex = -1;
            switch (newRoomDirection)
            {
                case RoomLinkDirection.Top: 
                    heightIndex = parentRoomHeightIndex + 1;
                    widthIndex = parentRoomWidthIndex;
                    break;
                case RoomLinkDirection.Right:
                    heightIndex = parentRoomHeightIndex;
                    widthIndex = parentRoomWidthIndex + 1;
                    break;
                case RoomLinkDirection.Bottom:
                    heightIndex = parentRoomHeightIndex - 1;
                    widthIndex = parentRoomWidthIndex;
                    break;
                case RoomLinkDirection.Left:
                    heightIndex = parentRoomHeightIndex;
                    widthIndex = parentRoomWidthIndex - 1;
                    break;
            }

            return (heightIndex: heightIndex, widthIndex: widthIndex);
        }

        RoomLinkDirection getOppositeDirection(RoomLinkDirection newRoomDirection)
        {
            switch (newRoomDirection)
            {
                case RoomLinkDirection.Top:
                    return RoomLinkDirection.Bottom;
                case RoomLinkDirection.Right:
                    return RoomLinkDirection.Left;
                case RoomLinkDirection.Bottom:
                    return RoomLinkDirection.Top;
                case RoomLinkDirection.Left:
                    return RoomLinkDirection.Right;
            }

            return RoomLinkDirection.Bottom;
        }

    }



}