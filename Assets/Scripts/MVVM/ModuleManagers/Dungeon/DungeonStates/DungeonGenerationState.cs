using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RPG.DataModule;

namespace RPG.ModuleManager.Dungeon.States
{
    public class DungeonGenerationState : DungeonState
    {
        public override void start()
        {
            execute();
        }

        public override void execute()
        {
            // dungeon generation
            Room startRoom = _manager.dungeonGenerationController.generateDungeon();
            _manager.dungeonController.stateEnded = _manager.endCurentState;
            _manager.dungeonController.initDungeon(_manager.dungeonGenerationController.dungeon, startRoom);

            _manager.dungeonController.moveCameraToCurrentRoomInstant();

            // display map
            _manager.dungeonController.roomChoosed = _manager.changeCurrentRoom;
            _manager.dungeonController.generateMap();

            _manager.dungeonController.displayCharacterReward();
        }

        public override void end()
        {
            _manager.dungeonController.menu.display(false, false);
            _manager.dungeonController.spawnCharacter();
            _manager.changeState(typeof(RoomState).ToString());
        }
    }
}