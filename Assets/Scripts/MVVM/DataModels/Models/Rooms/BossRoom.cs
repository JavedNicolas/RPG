using UnityEngine;
using UnityEditor;
using RPG.DataModule;
using System.Collections;

namespace RPG.DataModule
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