using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RPG.DungeonMode.Dungeon
{
    public interface IDungeonSpawner
    {
        void spawnRooms(Room[,] rooms);
    }
}