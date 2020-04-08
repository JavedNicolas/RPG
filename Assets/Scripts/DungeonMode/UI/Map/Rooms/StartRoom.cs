using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using RPG.Data;

namespace RPG.DungeonMode.Dungeon
{
    [CreateAssetMenu(fileName = "Room", menuName = AssetsPath.DUNGEON_ROOM_SO_MENU_NAME + "/StartRoom")]
    public class StartRoom : RoomScriptableObject
    {
        public override void effect()
        {
            DungeonManager.instance.dungeonModeUI.generateCharacterChoices();
        }
    }
}