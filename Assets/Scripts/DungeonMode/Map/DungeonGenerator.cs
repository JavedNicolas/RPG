using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RPG.Data;

namespace RPG.DungeonMode.Map
{
    public class DungeonGenerator : MonoBehaviour
    {
        const int MAX_ROOM_PER_CHOICE = 4;

        List<List<RoomData>> _map = new List<List<RoomData>>();
        public List<List<RoomData>> rooms => _map;
        public RoomScriptableObject currentMap { get; private set; }
        
        /// <summary> Create the map from the database of available rooms</summary>
        public void createMap(DungeonRoomDatabase roomDatabase, int minSize, int maxSize, int seed)
        {
            int dungeonSize = Random.Range(minSize, maxSize);

            for(int i = 0; i < dungeonSize; i++)
            {
                int roomInThisChoice = Random.Range(1, MAX_ROOM_PER_CHOICE + 1);

                List<RoomScriptableObject> rooms = roomDatabase.getRandomElements(roomInThisChoice, true);
                List<RoomData> roomDatas = new List<RoomData>();
                rooms.ForEach(x => roomDatas.Add(new RoomData(x)));
                _map.Add(roomDatas);

                if(i >= 1)
                    setLink(_map[i - 1], _map[i]);
            }        
        }

        /// <summary> Set the link between the previous choice rooms and the current ones </summary>
        /// <param name="previousChoice"> previous choice rooms </param>
        /// <param name="currentChoice"> current choice rooms </param>
        void setLink(List<RoomData> previousChoice, List<RoomData> currentChoice)
        {
            List<RoomData> roomNotLinked = new List<RoomData>(currentChoice);
            for (int i = 0; i < previousChoice.Count; i++)
            {
                // get the number of link for the previous Room
                int numberOfLink = (previousChoice.Count == 1) ? currentChoice.Count : Random.Range(1, currentChoice.Count/2);

                for (int j = 0; j < numberOfLink; j++)
                {
                    setNextRoom(i, j, previousChoice, currentChoice);

                    // remove the choosed room from the not linker list
                    int nextRoomCount = previousChoice[i].nextRoomData.Count - 1;
                    RoomData lastNextRoom = previousChoice[i].nextRoomData[nextRoomCount];
                    roomNotLinked.Remove(lastNextRoom);
                }
            }
            // add all the current choice room not linked to any previous room to the last previous room
            roomNotLinked.ForEach(x => previousChoice[previousChoice.Count - 1].nextRoomData.Add(x));
        }

        /// <summary> Get the next room to create a smooth layer with no dead end and road crossing </summary>
        /// <returns></returns>
        void setNextRoom(int previousChoiceIndex, int currentChoiceIndex, List<RoomData> previousChoice, List<RoomData> currentChoice)
        {
            RoomData room = previousChoice[previousChoiceIndex];


            // if there is only one previous then set his next Room to all the current one
            if (previousChoice.Count == 1)
            {
                room.nextRoomData.Add(currentChoice[currentChoiceIndex]);
                return;
            }
                

            // When there is only one current room set the next room to the first current room
            if (currentChoice.Count == 1)
            {
                room.nextRoomData.Add(currentChoice[0]);
                return;
            }
                

            // When we are setting the first previous room and this is his first link
            if (previousChoiceIndex == 0 && previousChoice[previousChoiceIndex].nextRoomData.Count < 1)
            {
                room.nextRoomData.Add(currentChoice[0]);
                return;
            }
            // When we are setting the first previous room and this is not his first link
            // Then we check the last link index and add a link to the next currentChoice room
            else if (previousChoiceIndex == 0 && previousChoice[previousChoiceIndex].nextRoomData.Count >= 1)
            {
                int nextRoomCount = previousChoice[previousChoiceIndex].nextRoomData.Count;
                RoomData lastNextRoomData = previousChoice[previousChoiceIndex].nextRoomData[nextRoomCount - 1];
                // the last next room index in the current choice list
                // /!\ THIS CANNOT BE OUT OF BOUND BECAUSE THE NUMBER OF LINK DOES NOT GOES TO THE COUNT OF CURRENTCHOICE SIZE /!\
                int lastNextRoomDataIndex = currentChoice.FindIndex(x => x == lastNextRoomData);
                room.nextRoomData.Add(currentChoice[lastNextRoomDataIndex + 1]);
                return;
            }

            // if we are choosing for the last previous room, set the next room to the last
            // current room
            /*if (previousChoiceIndex == previousChoice.Count - 1)
            {
                room.nextRoomData.Add(currentChoice[currentChoice.Count - 1]);
                return;
            }*/
  
            if(previousChoiceIndex > 0)
            {
                // We get the last link made to ensure the new link we make does not cross 
                int previousRoomNextRoomCount = previousChoice[previousChoiceIndex - 1].nextRoomData.Count;
                RoomData lastPreviousRoomNextRoomData = previousChoice[previousChoiceIndex - 1].nextRoomData[previousRoomNextRoomCount - 1];
                int lastNextRoomDataIndex = currentChoice.FindIndex(x => x == lastPreviousRoomNextRoomData);

                int nextRoomIndex;
                if (lastNextRoomDataIndex == currentChoice.Count - 1)
                {
                    if (previousChoice[previousChoiceIndex].nextRoomData.Count > 0)
                        return;
                    else
                        nextRoomIndex = currentChoice.Count - 1;
                }
                else
                {
                    nextRoomIndex = Random.Range(lastNextRoomDataIndex, lastNextRoomDataIndex + 2);
                }

                if (!room.nextRoomData.Exists(x => x == currentChoice[nextRoomIndex]))
                    room.nextRoomData.Add(currentChoice[nextRoomIndex]);
            }
        } 
    }
}
