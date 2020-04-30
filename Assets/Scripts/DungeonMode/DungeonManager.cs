using UnityEngine;
using System.Collections;
using RPG.DungeonMode.Dungeon;
using System.Collections.Generic;
using RPG.Data.Team;
using RPG.Data;
using RPG.DungeonMode.UI;

namespace RPG.DungeonMode
{
    using RPG.Battle;
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

        [Header("Transfrom")]
        [SerializeField] Transform _characterHolder;

        // private var
        public int dungeonSeed { get; private set; }
        public Room currentRoom {get; private set; }
        public Team team { get; private set; }

        public IDungeonGeneration dungeonGenerator { get; private set; }
        public IDungeonSpawner dungeonSpawner { get; private set; }

        // states
        public List<DungeonState> states { get; private set; }
        public DungeonState _currentState { get; private set; }

        // other
        [Header("Spawners")]
        [SerializeField] CharacterSpawner _characterSpawner;
        public CharacterSpawner characterSpawner => _characterSpawner;

        private void Awake()
        {
            dungeonGenerator = GetComponent<IDungeonGeneration>();
            dungeonSpawner = GetComponent<IDungeonSpawner>();

            instance = this;
        }

        // Use this for initialization
        void Start()
        {
            team = new Team();
            DungeonState.init(this);
            initStates();
            changeState(typeof(DungeonGenerationState).ToString());
        }

        public void setCurrentRoom(Room currentRoom)
        {
            this.currentRoom = currentRoom;
        }

        public void moveCurrentRoomToCurrentRoom()
        {
            Debug.Log(currentRoom.gameObject != null);
            if(currentRoom != null && currentRoom.gameObject != null)
                _characterHolder.position = currentRoom.gameObject.transform.position;
        }

        public void spawnPlayer()
        {
            RoomGameObject roomGO = currentRoom.gameObject.GetComponent<RoomGameObject>();

            team.currentTeam.ForEach(x =>
            {
                ActorSpawningPoint actorSpawningPoint = roomGO.startPoints.First();
                Debug.LogFormat("{0} character, {1}", x.character.name, actorSpawningPoint);
                if (actorSpawningPoint != null && !x.character.isEmpty())
                {
                    Debug.Log("Spawing : " + x.character.model);
                    GameObject actorGO = _characterSpawner.spawnCharacter(x.character.model, actorSpawningPoint.gameObject.transform.position);
                    actorSpawningPoint.actor = x.character;
                    actorSpawningPoint.actorGameObject = actorGO;
                }
            });
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

        public void endCurentState()
        {
            _currentState.end();
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

