using UnityEngine;
using UnityEditor;
using RPG.Data;
using System.Collections;

namespace RPG.DungeonMode.Dungeon
{
    [CreateAssetMenu(fileName = "Room", menuName = AssetsPath.DUNGEON_ROOM_SO_MENU_NAME + "/BossRoom")]
    public class BossRoom : RoomScriptableObject
    {
        public override IEnumerator effect()
        {
            yield return null;
        }
    }
}