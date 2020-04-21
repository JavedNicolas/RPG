using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RPG.DungeonMode.Dungeon
{
    public class Room
    {
        public string name { get; private set; }

        public bool cannotBranch {get; private set;}
        public GameObject prefab { get; private set; }
        public GameObject gameObject { get; private set; }
        public GameObject mapItem { get; private set; }
        public List<Room> linkedRooms { get; private set; }
        public string linkedRoomString { get; private set; }
        RoomScriptableObject _scriptableObject;

        public delegate void RoomCompleted();
        public RoomCompleted roomCompleted;

        public Room(RoomScriptableObject roomSO)
        {
            name = roomSO.name;
            prefab = roomSO.prefab;
            cannotBranch = roomSO.cannotBranch;
            linkedRooms = new List<Room>();
            _scriptableObject = roomSO;
            linkedRoomString = "";

        }

        public void setMapItem(GameObject mapItem)
        {
            this.mapItem = mapItem;
        }

        public void setGameObject(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        public void executeRoom()
        {
            _scriptableObject.effect();
        }

        public void addLinkedRoom(Room newlinkedRoom, RoomLinkDirection newRoomDirection)
        {
            this.linkedRooms.Add(newlinkedRoom);

            switch (newRoomDirection)
            {
                case RoomLinkDirection.Top: 
                    linkedRoomString += "T"; break;
                case RoomLinkDirection.Right: 
                    linkedRoomString += "R"; break;
                case RoomLinkDirection.Bottom: 
                    linkedRoomString += "B"; break;
                case RoomLinkDirection.Left: 
                    linkedRoomString += "L"; break;
            }
        }
    }  
}
