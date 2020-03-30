using UnityEngine;
using System.Collections;
using RPG.DungeonMode.Map;

namespace RPG.Data
{
    [CreateAssetMenu(menuName = AssetsPath.DUNGEON_ROOM_DB_MENU_NAME)]
    public class DungeonRoomDatabase : Database<Room>
    {
        protected override string elementFolderPath => AssetsPath.DATABASE_ELEMENT_BASE_FOLDER + AssetsPath.DUNGEON_ROOM_NAME;
    }
}

