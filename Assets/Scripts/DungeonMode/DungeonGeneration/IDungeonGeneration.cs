using UnityEngine;
using UnityEditor;

namespace RPG.DungeonMode.Dungeon
{
    using RPG.Data;
    using System.Collections.Generic;

    public interface IDungeonGeneration
    {
        Room[,] rooms { get; }

        Room createDungeon(DungeonRoomDatabase roomDatabase, int minSize, int maxSize, int seed);
    }
}
