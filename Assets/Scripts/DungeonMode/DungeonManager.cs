using UnityEngine;
using System.Collections;
using RPG.DungeonMode.Dungeon;
using System.Collections.Generic;
using RPG.Data.Team;
using RPG.Data;
using RPG.DungeonMode.UI;

namespace RPG.DungeonMode
{

    using RPG.DungeonMode.States;
    using RPG.settings;
    using System;
    using System.Linq;

    public class DungeonManager : MonoBehaviour
    {
        public static DungeonManager instance;

        #region databases
        [Header("Databases")]
        [SerializeField] CharacterDatabase _characterDatabase;
        [SerializeField] EnemyDatabase _enemyDatabase;
        [SerializeField] DungeonRoomDatabase _dungeonRoomDatabase;

        public CharacterDatabase characterDatabase { get => _characterDatabase; set => _characterDatabase = value; }
        public EnemyDatabase enemyDatabase { get => _enemyDatabase; set => _enemyDatabase = value; }
        public DungeonRoomDatabase dungeonRoomDatabase { get => _dungeonRoomDatabase; set => _dungeonRoomDatabase = value; }
        #endregion

        [SerializeField] public Camera mainCamera;

        [Header("UI")]
        [SerializeField] DungeonModeUI _dungeonModeUI;
        public DungeonModeUI dungeonModeUI  => _dungeonModeUI;

        // private var
        public int dungeonSeed { get; private set; }
        public Room currentRoom {get; private set; }
        public Team team { get; private set; }

        public IDungeonGeneration dungeonGenerator { get; private set; }
        public IDungeonSpawner dungeonSpawner { get; private set; }

        // states
        public List<DungeonState> states { get; private set; }
        public DungeonState _currentState { get; private set; }

        private void Awake()
        {
            dungeonGenerator = GetComponent<IDungeonGeneration>();
            dungeonSpawner = GetComponent<IDungeonSpawner>();

            instance = this;
        }

        // Use this for initialization
        void Start()
        {
            initTeam();
            DungeonState.init(this);
            initStates();
            changeState(typeof(DungeonGenerationState).ToString());
        }

        public void moveCameraToCurrentRoom()
        {
            mainCamera.transform.position = currentRoom.gameObject.GetComponent<RoomGameObject>().cameraPosition();
        }

        private void Update()
        {
            moveCameraToCurrentRoom();
        }

        public void setCurrentRoom(Room currentRoom)
        {
            this.currentRoom = currentRoom;
        }

        public void endCurentRoom()
        {
            _currentState.end();
        }

        void initTeam()
        {
            team = new Team();
            List<BattlePosition> position = Enum.GetValues(typeof(BattlePosition)).Cast<BattlePosition>().ToList();

            for (int i =0; i < GameSettings.DUNGEON_MODE_TEAM_MAX_SIZE; i++)
            {
                bool isInFront = i / position.Count > 1 ? false : true;
                int currentPositionIndex = i % (GameSettings.DUNGEON_MODE_TEAM_MAX_SIZE);
                Character character = new Character();
                character.initEmpty();
                team.addCharacterToTeam(character, isInFront, position[currentPositionIndex]);
            }
        }

        #region states Management
        void initStates()
        {
            states = new List<DungeonState>();
            states.Add(new DungeonGenerationState());
            states.Add(new ChooseMapState());
            states.Add(new MoveToRoomState());
            states.Add(new RoomState());

        }

        public void changeState(string typeName) 
        {
            DungeonState newState = states.Find(x => x.GetType().ToString() == typeName);
            _currentState = newState;
            _currentState.start();
        }

        #endregion


        public void moveToNextRoom(Room nextRoom)
        {
            if (!currentRoom.linkedRooms.Contains(nextRoom))
            {
                
                Debug.LogWarning("INSERT MESSAGE TO THE PLAYER, OR ANY VISUAL INDICATION");
                return;
            }

            currentRoom.mapItem.GetComponent<RoomMapItem>().haveBeenCleared(true);
            currentRoom = nextRoom;
            currentRoom.mapItem.GetComponent<RoomMapItem>().isCurrentRoom(true);
            

            // Moving to the next room code
            Debug.Log("Can move");
        }


    }
}

