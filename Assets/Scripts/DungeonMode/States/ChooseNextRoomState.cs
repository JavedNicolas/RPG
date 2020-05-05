using UnityEngine;

namespace RPG.DungeonMode.States
{
    public class ChooseNextRoomState : DungeonState
    {
        public override void start()
        {
            execute();
        }

        public override void execute()
        {
            _manager.dungeonModeUI.displayNextRoomButton(true);
        }

        public override void end()
        {
            _manager.dungeonModeUI.mapDisplayer.display(true, false);
            _manager.dungeonModeUI.menu.display(false, false);
            _manager.dungeonModeUI.displayNextRoomButton(false);
            _manager.changeState(typeof(MoveToNextRoomState).ToString());
        }
    }
}