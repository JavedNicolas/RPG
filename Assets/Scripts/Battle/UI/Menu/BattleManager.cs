using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System;

namespace RPG.Battle
{
    using RPG.UI;
    using RPG.Battle.StateMachine;
    using RPG.Data;
    using RPG.Battle.UI;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class BattleManager : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] BattleActorMenu _battleActorMenu;
        [SerializeField] BattleActionMenu _battleActionMenu;
        [SerializeField] BattleTargetSelector _battleTargetSelector;
        [SerializeField] BattleAPDisplayer _battleAPDisplayer;
        [SerializeField] Button _endTurnButton;
        [SerializeField] EventSystem _eventSystem;

        public Button endTurnButton => _endTurnButton;

        [Header("States")]
        [SerializeField] BattleStateManager _battleStateManager;

        [Header("Spawner")]
        [SerializeField] BattleSpawners _battleSpawners;

        public void Start()
        {
            initMenus();
            initBattleStateDelegate();

            // spawn actors
            _battleSpawners.spawnActors();

            // start the battle
            _battleStateManager.startBattle();
        }

        private void initBattleStateDelegate()
        {
            _battleStateManager.displayMenu = displayMenu;
            _battleStateManager.hideMenu = hideMenu;
            _battleStateManager.actionPointUpdated = updateActionPointDisplay;
            _battleStateManager.requestCharacters = _battleSpawners.getCharacters;
            _battleStateManager.requestEnemies = _battleSpawners.getEnemies;
            _battleStateManager.requestValidTarget = getValidTarget;
            _battleStateManager.getBattleSpawn = _battleSpawners.getSpawningPoint;
        }

        /// <summary> init all the menus and their delegate/actions</summary>
        private void initMenus()
        {
            _battleActorMenu.setEventSystem(_eventSystem);
            _battleActorMenu.menuFinished = switchToActionMenu;
            _battleActorMenu.menuCanceled = delegate { switchToActorSelection(); };

            _battleActionMenu.setEventSystem(_eventSystem);
            _battleActionMenu.menuFinished = switchToTargetSelection;
            _battleActionMenu.menuCanceled = delegate { switchToActorSelection(); };

            _battleTargetSelector.setEventSystem(_eventSystem);
            _battleTargetSelector.menuFinished = _battleStateManager.setTarget;
            _battleTargetSelector.menuCanceled = delegate { switchToActionMenu(null); };

        }

        private void Update()
        {
            // when the button BattleEndTurn is used end the turn
            if (Input.GetButtonDown("BattleEndTurn") && _battleStateManager.isPlayerTurn())
                _endTurnButton.onClick.Invoke();

            if (_battleStateManager.isPlayerTurn())
                _endTurnButton.gameObject.SetActive(true);
        }

        /// <summary> Display the first menu </summary>
        /// <param name="characters">The characters in battle</param>
        public void displayMenu(List<Being> characters)
        {
            _battleActionMenu.unFocusMenu();
            _battleTargetSelector.unFocusMenu();
            _battleActorMenu.initMenu(characters);
            _battleActorMenu.focusMenu();
        }

        /// <summary> update the Action points displayer </summary>
        public void updateActionPointDisplay(int remainingActionPoint, int maxActionPoint)
        {
            _battleAPDisplayer.updateActionPointDisplay(remainingActionPoint, maxActionPoint);
        }

        /// <summary> Unfocus battleActor menu and Hide all other menu   </summary>
        public void hideMenu()
        {
            _battleActionMenu.unFocusMenu();
            _battleTargetSelector.unFocusMenu();
            _battleActorMenu.unFocusMenu();
        }

        public void endTurn()
        {
            _battleStateManager.currentBattleState.end();
            hideMenu();
            endTurnButton.gameObject.SetActive(false);
        }

        #region switch menus
        /// <summary> switch back to the Actor menu </summary>
        private void switchToActorSelection()
        {
            _battleTargetSelector.unFocusMenu();
            _battleActionMenu.unFocusMenu();
            _battleActorMenu.focusMenu();
        }

        /// <summary> Switch to action menu </summary>
        /// <param name="actor">the actor to init the menu with. If null just display the menu (used to come back from target selection)</param>
        private void switchToActionMenu(Being actor)
        {
            _battleStateManager.setActor(actor);
            _battleActorMenu.unFocusMenu();
            _battleTargetSelector.unFocusMenu();
            if(actor != null)
                _battleActionMenu.initMenu(actor.actions);
            _battleActionMenu.focusMenu();
        }

        /// <summary> Switch to the target selection interface</summary>
        /// <param name="action"></param>
        private void switchToTargetSelection(Action action)
        {
            _battleStateManager.setAction(action);
            List<BattleTarget> validBattleTarget = getValidTarget(ActorType.Enemy, action);
            _battleActionMenu.unFocusMenu();
            _battleTargetSelector.initMenu(validBattleTarget);
            _battleTargetSelector.focusMenu();
        }
        #endregion


        /// <summary> get a valid target from the enemy list. Used by the BattleTargetSelector to know which target can be selected</summary>
        /// <param name="action">Valid target for this action</param>
        /// <returns>A list a valid battle target</returns>
        public List<BattleTarget> getValidTarget(ActorType actorType, Action action) { return _battleSpawners.getValidTargets(actorType, action) ; }

    }

}
