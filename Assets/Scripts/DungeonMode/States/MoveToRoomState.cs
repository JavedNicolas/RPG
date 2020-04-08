using UnityEngine;
using System.Collections;

namespace RPG.DungeonMode.States
{
    public class MoveToRoomState : DungeonState
    {
        public override void start()
        {
            
        }

        public override void execute()
        {
            
        }

        public override void end()
        {
            _manager.changeState(typeof(RoomState).ToString());
        }
    }
}