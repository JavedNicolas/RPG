using UnityEngine;
using System.Collections;

namespace RPG.DungeonMode.States
{
    using RPG.State;

    public abstract class DungeonState : State
    {
        protected static DungeonManager _manager;
        
        /// <summary>
        /// init the state manager
        /// </summary>
        /// <param name="manager"></param>
        public static void init(DungeonManager manager) 
        {
            _manager = manager;
        }

    }
}