using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RPG.ModuleManager.Dungeon
{
    using RPG.ModuleManager.Dungeon.States;
    using RPG.DungeonGenerationModule;
    using RPG.DungeonModule;
    using RPG.DataModule;

    public class DungeonStateManager : MonoBehaviour
    {
        public static DungeonStateManager instance;

        //Modules
        DungeonGenerationController _dungeonGenerationController;
        public DungeonGenerationController dungeonGenerationController => _dungeonGenerationController;

        DungeonController _dungeonController;
        public DungeonController dungeonController => _dungeonController;

        // states
        public List<DungeonState> states { get; private set; }
        public DungeonState _currentState { get; private set; }

        private void Awake()
        {
            instance = this;
        }

        // Use this for initialization
        void Start()
        {
            DungeonState.init(this);
            initStates();
            changeState(typeof(DungeonGenerationState).ToString());
        }

        void initStates()
        {
            states = new List<DungeonState>();
            states.Add(new DungeonGenerationState());
            states.Add(new MoveToNextRoomState());
            states.Add(new ChooseNextRoomState());
            states.Add(new RoomState());
        }

        public void changeState(string typeName) 
        {
            DungeonState newState = states.Find(x => x.GetType().ToString() == typeName);
            _currentState = newState;
            _currentState.start();
        }

        public void endCurentState()
        {
            _currentState.end();
        }

        public bool changeCurrentRoom(int heightIndex, int widthIndex)
        {
            if(!dungeonController.setCurrentRoom(heightIndex, widthIndex))
            {
                Debug.LogWarning("INSERT MESSAGE TO THE PLAYER, OR ANY VISUAL INDICATION");
                return false;
            }
                
            endCurentState();
            return true;
        }
    }
}

