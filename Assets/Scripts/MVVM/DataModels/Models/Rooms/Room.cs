using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.DataModule
{
    public class Room
    {
        public string name { get; private set; }

        public bool cannotBranch {get; private set;}
        public GameObject gameObject { get; private set; }
        public List<Room> linkedRooms { get; private set; }
        public string linkedRoomString { get; private set; }
        public RoomScriptableObject scriptableObject { get; private set; }

        public delegate void RoomCompleted();
        public RoomCompleted roomCompleted;

        public Room(RoomScriptableObject roomSO)
        {
            name = roomSO.name;
            cannotBranch = roomSO.isSpecialRoom ? true : roomSO.cannotBranch;
            linkedRooms = new List<Room>();
            scriptableObject = roomSO;
            linkedRoomString = "";
        }

        public void setGameObject(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        public IEnumerator executeRoom()
        {
            IEnumerator coroutine = scriptableObject.effect();
            scriptableObject.effectDone = fireDelegate;
            return coroutine;
        }

        void fireDelegate()
        {
            roomCompleted();
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
