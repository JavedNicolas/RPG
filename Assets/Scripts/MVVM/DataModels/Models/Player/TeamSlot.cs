using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace RPG.DataModule
{
    public class TeamSlot<T>
    {
        public T being;
        public bool frontPosition;
        public BattlePosition battlePosition;

        public TeamSlot() { }

        public TeamSlot(T being, bool frontPosition, BattlePosition battlePosition)
        {
            this.being = being;
            this.frontPosition = frontPosition;
            this.battlePosition = battlePosition;
        }
    }
}
