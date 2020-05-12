using UnityEngine;
using System.Collections;
using RPG.DataModule;

namespace RPG.ModuleManager.Dungeon.States
{
    public class RoomState : DungeonState
    {
        public override void start()
        {
            _manager.dungeonController.currentRoom.roomCompleted = end;
            execute();
        }

        public override void execute()
        {
            Room currentRoom = _manager.dungeonController.currentRoom;
            IEnumerator roomEffect = currentRoom.executeRoom();
            _manager.StartCoroutine(roomEffect);
        }

        public override void end()
        {
            _manager.changeState(typeof(ChooseNextRoomState).ToString());
        }
    }
}