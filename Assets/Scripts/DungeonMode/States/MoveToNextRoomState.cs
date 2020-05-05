using System.Collections;
using UnityEngine;

namespace RPG.DungeonMode.States
{
    public class MoveToNextRoomState : DungeonState
    {
        public override void start()
        {
            execute();
        }

        public override void execute()
        {
            IEnumerator coroutine = move();
            _manager.StartCoroutine(coroutine);
        }

        /// <summary>
        /// move to the next room and wait for this movement to be over
        /// </summary>
        /// <returns></returns>
        IEnumerator move()
        {
            _manager.rotateCurrentRoomStartZone();
            _manager.moveCurrentRoomToCurrentRoom();

            yield return new WaitUntil(() => _manager.movementFinished());

            _manager.endCurentState();
        }

        public override void end()
        {
            _manager.changeState(typeof(RoomState).ToString());
        }
    }
}