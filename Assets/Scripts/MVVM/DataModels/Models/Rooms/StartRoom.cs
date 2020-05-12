using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using RPG.DataModule;
using System.Collections;

namespace RPG.DataModule
{
    [CreateAssetMenu(fileName = "Room", menuName = AssetsPath.DUNGEON_ROOM_SO_MENU_NAME + "/StartRoom")]
    public class StartRoom : RoomScriptableObject
    {
        public override IEnumerator effect()
        {
            effectDone();
            yield return null;
        }
    }
}