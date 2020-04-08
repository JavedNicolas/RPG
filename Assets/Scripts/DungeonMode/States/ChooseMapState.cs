using UnityEngine;
using System.Collections;

namespace RPG.DungeonMode.States
{
    public class ChooseMapState : DungeonState
    {
        public override void start()
        {
            execute();
        }

        public override void execute()
        {
            _manager.dungeonModeUI.mapDisplayer.display(true, true);
        }

        public override void end()
        {
            
        }
    }
}