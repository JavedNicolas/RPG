using UnityEngine;
using System.Collections;

namespace RPG.DungeonMode.States
{
    public class RoomState : DungeonState
    {
        public override void start()
        {
            _manager.currentRoom.roomCompleted = end;
            execute();
        }

        public override void execute()
        {
            _manager.currentRoom.executeRoom();
        }

        public override void end()
        {
            _manager.changeState(typeof(ChooseMapState).ToString());
        }
    }
}