using UnityEngine;

namespace RPG.ModuleManager.Dungeon.States
{
    public class ChooseNextRoomState : DungeonState
    {
        public override void start()
        {
            execute();
        }

        public override void execute()
        {
            _manager.dungeonController.displayNextRoomButton(true);
        }

        public override void end()
        {
            _manager.dungeonController.displayMenu(false);
            _manager.dungeonController.displayNextRoomButton(false);
            _manager.changeState(typeof(MoveToNextRoomState).ToString());
        }
    }
}