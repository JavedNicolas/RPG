using UnityEngine;
using System.Collections;

namespace RPG.ModuleManager.Dungeon.States
{
    public abstract class DungeonState : State
    {
        protected static DungeonStateManager _manager;
        
        /// <summary>
        /// init the state manager
        /// </summary>
        /// <param name="manager"></param>
        public static void init(DungeonStateManager manager) 
        {
            _manager = manager;
        }

    }
}