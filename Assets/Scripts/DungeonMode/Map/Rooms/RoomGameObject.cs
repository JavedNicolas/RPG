using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RPG.DungeonMode.Dungeon
{
    public class RoomGameObject : MonoBehaviour
    {
        [SerializeField] public RoomData roomData { get; private set; }
        [SerializeField] List<GameObject> _startPoints;

        public void setRoomData(RoomData roomData)
        {
            this.roomData = roomData;
        }
    }

}
