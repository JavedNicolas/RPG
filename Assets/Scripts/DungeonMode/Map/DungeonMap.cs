using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RPG.Data;

namespace RPG.DungeonMode.Map
{
    public class DungeonMap : MonoBehaviour
    {
        List<Room> _map = new List<Room>();
        public List<Room> rooms => _map;

        public Room currentMap { get; private set; }
        
        public void createMap(DungeonRoomDatabase roomDatabase, int minSize, int maxSize, int seed)
        {
            int dungeonSize = Random.Range(minSize, maxSize);

            _map = roomDatabase.getRandomElements(dungeonSize, true);
        }
    }
}
