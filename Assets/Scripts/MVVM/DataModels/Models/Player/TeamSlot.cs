using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace RPG.DataModule
{
    public struct TeamSlot
    {
        public Character character;
        public bool frontPosition;
        public BattlePosition battlePosition;

        public TeamSlot(Character character, bool frontPosition, BattlePosition battlePosition)
        {
            this.character = character;
            this.frontPosition = frontPosition;
            this.battlePosition = battlePosition;
        }
    }
}
