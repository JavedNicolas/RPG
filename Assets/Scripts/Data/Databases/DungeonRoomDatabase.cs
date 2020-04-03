using UnityEngine;
using System.Collections;
using RPG.DungeonMode.Dungeon;
using System.Collections.Generic;

namespace RPG.Data
{
    [CreateAssetMenu(menuName = AssetsPath.DUNGEON_ROOM_DB_MENU_NAME)]
    public class DungeonRoomDatabase : Database<RoomScriptableObject>
    {
        protected override string elementFolderPath => AssetsPath.DATABASE_ELEMENT_BASE_FOLDER + AssetsPath.DUNGEON_ROOM_NAME;

        public RoomScriptableObject getStaticRoom(string named)
        {
            RoomScriptableObject staticRoom = getStaticRooms().Find(x => x.name == named);
            Debug.Log(staticRoom);
            return staticRoom;
        }

        public List<RoomScriptableObject> getStaticRooms()
        {
            return _elements.FindAll(x => x.isStaticRoom);
        }
    }
}

