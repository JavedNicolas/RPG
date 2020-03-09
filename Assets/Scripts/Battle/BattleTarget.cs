using UnityEngine;
using UnityEngine.UI;
using RPG.DataManagement;

namespace RPG.Battle
{
    using RPG.UI;

    public class BattleTarget
    {
        public MenuButton button;
        public GameObject model;
        public Being being;

        /// <summary> Generate a List of target  </summary>
        /// <param name="spawningPoint"></param>
        /// <returns></returns>
        public BattleTarget(BattleSpawningPoint spawningPoint)
        {
            button = spawningPoint.GetComponentInChildren<MenuButton>();
            model = spawningPoint.actorGameObject;
            being = spawningPoint.actor;
        }
    }
}
