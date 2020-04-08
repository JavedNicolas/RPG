

using UnityEngine;

namespace RPG.Battle.StateMachine
{
    public class PlayerLostState : BattleState
    {
        public override void start()
        {
            Debug.Log("PLayer Lost");
        }

        public override void execute()
        {
            throw new System.NotImplementedException();
        }

        public override void end()
        {
            throw new System.NotImplementedException();
        }
    }
}

