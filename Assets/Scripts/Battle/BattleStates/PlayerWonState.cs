


using UnityEngine;

namespace RPG.Battle.StateMachine
{
    public class PlayerWonState : BattleState
    {
        public override void start()
        {
            Debug.Log("PLayer Won");
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


