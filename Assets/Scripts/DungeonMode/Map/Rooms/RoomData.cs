using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RPG.DungeonMode.Dungeon
{
    public class RoomData
    {
        public string name { get; private set; }


        public bool cannotBranch {get; private set;}
        public GameObject prefab { get; private set; }
        public GameObject gameObject { get; private set; }
        public GameObject mapItem { get; private set; }
        public List<RoomData> linkedRoom { get; private set; }

        public RoomData(RoomScriptableObject roomSO)
        {
            name = roomSO.name;
            prefab = roomSO.prefab;
            cannotBranch = roomSO.cannotBranch;
            linkedRoom = new List<RoomData>();
        }

        public void setMapItem(GameObject mapItem)
        {
            this.mapItem = mapItem;
        }

        public void setGameObject(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }  
}
