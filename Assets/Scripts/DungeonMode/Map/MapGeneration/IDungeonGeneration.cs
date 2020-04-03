using UnityEngine;
using UnityEditor;

namespace RPG.DungeonMode.Dungeon
{
    using RPG.Data;
    using System.Collections.Generic;

    public interface IDungeonGeneration
    {
        RoomData[,] rooms { get; }

        void createDungeon(DungeonRoomDatabase roomDatabase, int minSize, int maxSize, int seed);
    }
}
