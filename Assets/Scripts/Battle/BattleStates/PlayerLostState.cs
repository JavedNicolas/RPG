

using UnityEngine;

namespace RPG.Battle.StateMachine
{
    public class PlayerLostState : BattleState
    {
        public override void start()
        {
            Debug.Log("PLayer Lost");
        }

        public override void executeState()
        {
            throw new System.NotImplementedException();
        }

        public override void endTurn()
        {
            throw new System.NotImplementedException();
        }
    }
}

