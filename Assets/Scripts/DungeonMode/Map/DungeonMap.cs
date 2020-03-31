using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RPG.Data;

namespace RPG.DungeonMode.Map
{
    public class DungeonMap : MonoBehaviour
    {
        const int MAX_ROOM_PER_CHOICE = 4;

        List<List<Room>> _map = new List<List<Room>>();
        public List<List<Room>> rooms => _map;

        public Room currentMap { get; private set; }
        
        public void createMap(DungeonRoomDatabase roomDatabase, int minSize, int maxSize, int seed)
        {
            int dungeonSize = Random.Range(minSize, maxSize);
            Debug.Log("Dungeon size :" + dungeonSize);
            for(int i = 0; i < dungeonSize; i++)
            {
                int roomInThiSChoice = Random.Range(1, MAX_ROOM_PER_CHOICE + 1);
                _map.Add(roomDatabase.getRandomElements(roomInThiSChoice, true));
                Debug.Log("Choice size :" + roomInThiSChoice);
            } 
            
        }
    }
}
