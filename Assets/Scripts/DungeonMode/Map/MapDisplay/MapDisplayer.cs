using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RPG.DungeonMode.Dungeon
{
    public abstract class MapDisplayer : MonoBehaviour
    {
        #region room click display
        public delegate void RoomChosed(RoomData roomChosed);
        public RoomChosed roomChosed;
        #endregion

        public abstract void displayMap(RoomData[,] rooms);
    }
}

