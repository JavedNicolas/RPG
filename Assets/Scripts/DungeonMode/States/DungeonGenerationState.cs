using UnityEngine;
using System.Collections;
using RPG.DungeonMode.UI;

namespace RPG.DungeonMode.States
{
    public class DungeonGenerationState : DungeonState
    {
        public override void start()
        {
            execute();
        }

        public override void execute()
        {
            _manager.setCurrentRoom(_manager.dungeonGenerator.createDungeon(_manager.dungeonRoomDatabase, 12, 15, _manager.dungeonSeed));
            _manager.dungeonSpawner.spawnRooms(_manager.dungeonGenerator.rooms);

            // display map
            _manager.dungeonModeUI.mapDisplayer.roomChosed = _manager.moveToNextRoom;
            _manager.dungeonModeUI.mapDisplayer.generateMap(_manager.dungeonGenerator.rooms, _manager.currentRoom);

            _manager.currentRoom.mapItem.GetComponent<RoomMapItem>().isCurrentRoom(true);
            end();
        }

        public override void end()
        {
            _manager.changeState(typeof(RoomState).ToString());
        }
    }
}