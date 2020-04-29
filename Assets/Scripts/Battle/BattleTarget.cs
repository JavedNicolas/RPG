using UnityEngine;
using UnityEngine.UI;
using RPG.Data;

namespace RPG.Battle
{
    using RPG.UI;

    public class BattleTarget
    {
        public MenuButton button;
        public GameObject model;
        public Being actor;

        /// <summary> Generate a List of target  </summary>
        /// <param name="spawningPoint"></param>
        /// <returns></returns>
        public BattleTarget(ActorSpawningPoint spawningPoint)
        {
            button = spawningPoint.GetComponentInChildren<MenuButton>();
            model = spawningPoint.actorGameObject;
            actor = spawningPoint.actor;
        }
    }
}
