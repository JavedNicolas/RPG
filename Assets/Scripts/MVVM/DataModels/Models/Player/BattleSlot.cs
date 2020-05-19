using UnityEngine;
using System.Collections;

namespace RPG.DataModule
{
    public class BattleSlot<T> : TeamSlot<T>
    {
        public GameObject gameObject { get; private set; }

        public BattleSlot(TeamSlot<T> teamslot, GameObject gameObject) : base(teamslot.being, teamslot.frontPosition, teamslot.battlePosition)
        {
            this.gameObject = gameObject;
        }

        public BattleSlot(T being, GameObject gameObject, bool frontPosition, BattlePosition battlePosition) : base(being, frontPosition, battlePosition)
        {
            this.gameObject = gameObject;
        }

    }
}