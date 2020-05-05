using UnityEngine;
using System.Collections;
using RPG.DungeonMode.Dungeon;

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
            Room currentRoom = _manager.currentRoom;
            currentRoom.executeRoom();
        }

        public override void end()
        {
            _manager.changeState(typeof(ChooseNextRoomState).ToString());
        }
    }
}