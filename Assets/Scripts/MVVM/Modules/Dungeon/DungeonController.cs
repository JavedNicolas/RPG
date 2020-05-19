using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace RPG.DungeonModule
{
    using RPG.DataModule;
    using RPG.DataModule.ViewModel;
    using RPG.DungeonModule.View;
    using RPG.GlobalModule.View;
    using System;
    using System.Linq;

    public partial class DungeonController : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] TabMenu _menu;
        public TabMenu menu => _menu;

        [SerializeField] Button _nextRoomButton;
        public Button nextRoomButton => _nextRoomButton;

        [Header("Camera")]
        [SerializeField] public Camera mainCamera;
        [SerializeField] Vector3 _cameraOffset;

        [Header("Transfrom")]
        [SerializeField] Transform _characterHolder;

        [Header("Map Displayer")]
        [SerializeField] GridMapDisplayer _mapDisplayer;
        [SerializeField] List<Sprite> _layoutIcons;

        [Header("Databases")]
        [SerializeField] CharacterDatabase _characterDatabase;
        [SerializeField] EnemyDatabase _enemyDatabase;

        // dungeon
        GridDungeon _dungeon;
        public Room currentRoom { get; private set; }
        private Room _previousRoom;

        // player
        public Team team { get; private set; }
        public List<BattleSlot<Character>> spawnedCharacter = new List<BattleSlot<Character>>();

        // reward
        RewardType _currentRewardType;

        #region delegate
        public delegate bool RoomChoosed(int heightIndex, int widthIndex);
        public RoomChoosed roomChoosed;
        public delegate void EndCurrentState();
        public EndCurrentState stateEnded;
        #endregion

        private void Start()
        {
            team = new Team();
        }

        public void initDungeon(GridDungeon dungeon, Room startRoom)
        {
            this._dungeon = dungeon;
            currentRoom = startRoom;
            _nextRoomButton.onClick.AddListener(delegate { displayMenu(true, canChooseARoom: true); });
        }

        #region Room Management

        /// <summary>
        /// Change the current room
        /// </summary>
        /// <param name="heightIndex">new room height index</param>
        /// <param name="widthIndex">new room width index</param>
        /// <returns>Return true if the room has been changed, false if there was a problem</returns>
        public bool setCurrentRoom(int heightIndex, int widthIndex)
        {
            Room nextRoom = _dungeon.findRoom(heightIndex, widthIndex);

            if (!currentRoom.linkedRooms.Contains(nextRoom))
                return false;

            _previousRoom = this.currentRoom;
            this.currentRoom = nextRoom;

            (int height, int width) indexes = _dungeon.findRoomIndex(_previousRoom);
            _mapDisplayer.roomCleared(indexes.height, indexes.width);
            return true;
        }
        #endregion

        #region UI
        /// <summary>
        /// Generate the map
        /// </summary>
        public void generateMap()
        {
            RoomViewModel roomViewModel = new RoomViewModel();
            Sprite[,] mapLayoutSprites =  roomViewModel.getGridDungeonMapSprites(_dungeon.rooms, _layoutIcons);

            _mapDisplayer.generateMap(mapLayoutSprites);
            _mapDisplayer.roomChoosed = fireRoomChoosedDelegate;

            // set the current room (start room) as the frist room
            (int height, int width) indexes = _dungeon.findRoomIndex(currentRoom);
            _mapDisplayer.selectedRoom(indexes.height, indexes.width);
        }

        /// <summary>
        /// Fire the room choose delegate when the map display fired his own room choosed delegate
        /// </summary>
        /// <param name="heightIndex">The height index of the room choosed (in the grid)</param>
        /// <param name="widthIndex">The width index of the room choosed (in the grid)</param>
        /// <returns></returns>
        public bool fireRoomChoosedDelegate(int heightIndex, int widthIndex)
        {
            return roomChoosed(heightIndex, widthIndex);
        }

        /// <summary>
        /// Display the character reward UI
        /// </summary>
        public void displayCharacterReward()
        {
            List<Character> characters = _characterDatabase.getRandomElements(3, false);
            displayMenu(true, false, true);

            // generate view data
            CharacterViewModel characterViewModel = new CharacterViewModel();
            TeamViewModel teamViewModel = new TeamViewModel();

            List<(string name, string description, Sprite icon)> charactersInfos = new List<(string name, string description, Sprite icon)>();
            characters.ForEach(x => charactersInfos.Add(characterViewModel.getCharacterInfo(x)));

            _currentRewardType = RewardType.Character;
            _menu.rewardMenus.initRewards(charactersInfos, teamViewModel.getTeamInfos(team));
            _menu.rewardMenus.isDone = RewardChoiceDone;
        }

        /// <summary>
        /// Fire the delegate which end current state
        /// </summary>
        void  RewardChoiceDone()
        {
            _menu.rewardMenus.isDone = null;
            switch (_currentRewardType)
            {
                case RewardType.Character: addRewardCharacterToTeam(); break;
                case RewardType.Item: break;
                case RewardType.Skill: break;
            }
            stateEnded();
        }

        /// <summary>
        /// Add a character to the team from the choice made in the reward menu
        /// </summary>
        void addRewardCharacterToTeam()
        {
            List<Choices> choices = _menu.rewardMenus.getSelectedRewards();

            List<BattlePosition> battlePositions = Enum.GetValues(typeof(BattlePosition)).Cast<BattlePosition>().ToList();

            for (int i = 0; i < choices.Count; i++)
            {
                bool isInFront = choices[i].playerElementIndex / battlePositions.Count < 1;

                Character characterChoosed = _characterDatabase.getElements(x => x.name == choices[i].reward).First();
                
                int battlePositionIndex = choices[i].playerElementIndex % battlePositions.Count;
                BattlePosition battlePosition = battlePositions[battlePositionIndex];

                team.addCharacterToTeam(characterChoosed, isInFront, battlePosition);
            }
        }

        /// <summary>
        /// Display or hide the button allowing the player to switch room
        /// </summary>
        /// <param name="display"></param>
        public void displayNextRoomButton(bool display)
        {
            _nextRoomButton.gameObject.SetActive(display);
        }

        /// <summary>
        /// Display the menu with the map to allow the player to switch room
        /// </summary>
        public void displayMenu(bool display, bool canChooseARoom = false, bool hasReward = false)
        {
            _mapDisplayer.canChangeRoom(canChooseARoom);
            menu.display(display, hasReward);
            displayNextRoomButton(false);
        }
        #endregion

        #region movement
        /// <summary>
        /// Move the camera to the current room without animation or smooth transition
        /// </summary>
        public void moveCameraToCurrentRoomInstant()
        {
            mainCamera.transform.position = currentRoom.gameObject.transform.position + _cameraOffset;
        }

        /// <summary>
        /// Move the team from the previous room to the current room
        /// </summary>
        public void moveCurrentRoomToCurrentRoom()
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
        void moveTeam(List<GameObject> actorSpawningPoints)
        {
            spawnedCharacter.ForEach(x =>
            {
                TeamViewModel teamViewModel = new TeamViewModel();
                int positionIndex = teamViewModel.getIndexForTeamPosition(x);

                if (positionIndex > actorSpawningPoints.Count)
                    return;

                GameObject matchingPoint = actorSpawningPoints[positionIndex];
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
            if (currentRoom.gameObject.TryGetComponent<RoomGameObject>(out roomGO))
            {
                Vector3 direction = (_previousRoom.gameObject.transform.position - currentRoom.gameObject.transform.position).normalized;
                roomGO.rotateStartZone(direction);
            }
        }

        public bool movementFinished()
        {
            MoveToPosition moveToPosition;
            if (_characterHolder.TryGetComponent<MoveToPosition>(out moveToPosition))
            {
                return moveToPosition.hasFinishedHisMovement;
            }

            return true;
        }

        #endregion

        /// <summary>
        /// Spawn all character in the team that have not already been spawned
        /// </summary>
        public void spawnCharacter()
        {
            team.currentTeam.ForEach(x =>
            {
                // if the character has not been spawned
                if(!spawnedCharacter.Exists(s => s.being != x.being))
                {
                    // spawn character
                    List<GameObject> roomStartPoints = currentRoom.gameObject.GetComponent<RoomGameObject>().startPoints;
                    TeamViewModel teamViewModel = new TeamViewModel();
                    int startPointIndex = teamViewModel.getIndexForTeamPosition(x);
                    GameObject gameObject = Instantiate(x.being.dungeonModeModel, roomStartPoints[startPointIndex].transform.position, Quaternion.identity, _characterHolder);

                    // make the character rotation correct
                    gameObject.transform.LookAt(gameObject.transform.position + roomStartPoints[startPointIndex].transform.forward);

                    BattleSlot<Character> battleSlot = new BattleSlot<Character>(x, gameObject);
                    spawnedCharacter.Add(battleSlot);
                }
            });
        }

    }
}