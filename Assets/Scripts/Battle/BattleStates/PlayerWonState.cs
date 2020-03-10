


using UnityEngine;

namespace RPG.Battle.StateMachine
{
    public class PlayerWonState : BattleState
    {
        public override void start()
        {
            Debug.Log("PLayer Won");
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


