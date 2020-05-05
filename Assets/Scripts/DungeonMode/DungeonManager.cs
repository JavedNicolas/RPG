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

        [Header("Camera")]
        [SerializeField] public Camera mainCamera;
        [SerializeField] Vector3 _cameraOffset;

        [Header("UI")]
        [SerializeField] DungeonModeUI _dungeonModeUI;
        public DungeonModeUI dungeonModeUI  => _dungeonModeUI;

        [Header("Transfrom")]
        [SerializeField] Transform _characterHolder;

        [Header("Spawners")]
        [SerializeField] CharacterSpawner _characterSpawner;
        public CharacterSpawner characterSpawner => _characterSpawner;

        public int dungeonSeed { get; private set; }
        public Room currentRoom { get; private set; }
        private Room _previousRoom;
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
            team = new Team();
            DungeonState.init(this);
            initStates();
            changeState(typeof(DungeonGenerationState).ToString());
        }

        public void setCurrentRoom(Room currentRoom)
        {
            this.currentRoom = currentRoom;
        }

        #region movement

        public void moveCameraToCurrentRoomInstant()
        {
            mainCamera.transform.position = currentRoom.gameObject.transform.position + _cameraOffset;
        }

        /// <summary>
        /// Move the team from the previous room to the current room
        /// </summary>
        public void moveCurrentRoomToCurrentRoom(bool instantMovement = false)
        {
            if (currentRoom != null && currentRoom.gameObject != null)
            {
                moveTeam(currentRoom.gameObject.GetComponent<RoomGameObject>().startPoints);
                mainCamera.GetComponent<MoveToPosition>().startMovement(currentRoom.gameObject.transform.position + _cameraOffset);
            }

                    
        }

        /// <summary>
        /// Move the team to a list a spawning points
        /// </summary>
        /// <param name="actorSpawningPoints"></param>
        void moveTeam(List<ActorSpawningPoint> actorSpawningPoints)
        {
            _characterSpawner.spawnedCharacter.ForEach(x =>
            {
                TeamSlot teamSlot = team.currentTeam.Find(t => t.character == x);
                ActorSpawningPoint matchingPoint = actorSpawningPoints.First();
                MoveToPosition moveToPosition = x.gameObject.GetComponent<MoveToPosition>();
                moveToPosition.startMovement(matchingPoint.gameObject.transform.position, 0.2f);
            });
        }

        /// <summary>
        /// Rotate the spawning point to match the movement direction (direction from previous room to current room)
        /// </summary>
        public void rotateCurrentRoomStartZone()
        {
            RoomGameObject roomGO;
            if(currentRoom.gameObject.TryGetComponent<RoomGameObject>(out roomGO))
            {
                Vector3 direction = (_previousRoom.gameObject.transform.position - currentRoom.gameObject.transform.position).normalized;
                Debug.Log(direction);
                roomGO.rotateStartZone(direction);
            }
        }

        public bool movementFinished()
        {
            MoveToPosition moveToPosition;
            if(_characterHolder.TryGetComponent<MoveToPosition>(out moveToPosition))
            {
                return moveToPosition.hasFinishedHisMovement;
            }

            return true;
        }

        #endregion
        public void spawnPlayer()
        {
            RoomGameObject roomGO = currentRoom.gameObject.GetComponent<RoomGameObject>();

            team.currentTeam.ForEach(x =>
            {
                ActorSpawningPoint actorSpawningPoint = roomGO.startPoints.First();

                if (actorSpawningPoint != null && !x.character.isEmpty() && !_characterSpawner.spawnedCharacter.Find(s => s.actor != x.character))
                    _characterSpawner.spawnCharacter(x.character, actorSpawningPoint.gameObject.transform.position);
            });
        }

        #region states Management
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

        #endregion


        public void changeCurrentRoom(Room nextRoom)
        {
            Debug.Log("Change currentRoom");
            if (!currentRoom.linkedRooms.Contains(nextRoom))
            {
                
                Debug.LogWarning("INSERT MESSAGE TO THE PLAYER, OR ANY VISUAL INDICATION");
                return;
            }
            _previousRoom = currentRoom;
            currentRoom.mapItem.GetComponent<RoomMapItem>().haveBeenCleared(true);
            nextRoom.mapItem.GetComponent<RoomMapItem>().isCurrentRoom(true);
            setCurrentRoom(nextRoom);
            endCurentState();
        }


    }
}

