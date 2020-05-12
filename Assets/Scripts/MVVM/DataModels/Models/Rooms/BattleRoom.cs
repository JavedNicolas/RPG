using UnityEngine;
using System.Collections;
using RPG.DataModule;

namespace RPG.DataModule
{
    [CreateAssetMenu(fileName = "Room", menuName = AssetsPath.DUNGEON_ROOM_SO_MENU_NAME + "/BattleRoom")]
    public class BattleRoom : RoomScriptableObject
    {
        public override IEnumerator effect()
        {
            effectDone();
            yield return null;
        }
    }

}
