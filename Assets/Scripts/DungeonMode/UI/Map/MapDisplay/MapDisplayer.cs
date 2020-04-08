using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RPG.DungeonMode.UI
{
    using RPG.DungeonMode.Dungeon;

    public abstract class MapDisplayer : MonoBehaviour
    {
        #region room click display
        public delegate void RoomChosed(Room roomChosed);
        public RoomChosed roomChosed;
        #endregion

        public abstract void generateMap(Room[,] rooms, Room startRoom);
        public abstract void display(bool display, bool canChooseARoom);
    }
}

