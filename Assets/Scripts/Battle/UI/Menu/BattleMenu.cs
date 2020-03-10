using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System;

namespace RPG.Battle.UI
{
    using RPG.UI;
    using RPG.Data;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class BattleMenu : MonoBehaviour
    {
        [SerializeField] BattleActorMenu _battleActorMenu;
        [SerializeField] BattleActionMenu _battleActionMenu;
        [SerializeField] BattleTargetSelector _battleTargetSelector;
        [SerializeField] BattleAPDisplayer _battleAPDisplayer;
        [SerializeField] Button _endTurnButton;
        [SerializeField] EventSystem _eventSystem;

        public Button endTurnButton => _endTurnButton;

        #region delegate 
        public delegate List<BattleTarget> RequestValidTarget(Action action);
        public RequestValidTarget requestValidTarget;

        public System.Action<Being> actorChoosed;
        public System.Action<Action> actionChoosed;
        public System.Action<BattleTarget> targetChoosen;
        #endregion

        /// <summary> init all the menus and their delegate/actions</summary>
        public void initMenus()
        {
            _battleActorMenu.setEventSystem(_eventSystem);
            _battleActorMenu.menuFinished = switchToActionMenu;
            _battleActorMenu.menuCanceled = delegate { switchToActorSelection(); };

            _battleActionMenu.setEventSystem(_eventSystem);
            _battleActionMenu.menuFinished = switchToTargetSelection;
            _battleActionMenu.menuCanceled = delegate { switchToActorSelection(); };

            _battleTargetSelector.setEventSystem(_eventSystem);
            _battleTargetSelector.menuFinished =  sendTargetChoosen;
            _battleTargetSelector.menuCanceled = delegate { switchToActionMenu(null); };

        }

        private void Update()
        {
            // when the button BattleEndTurn is used end the turn
            if (Input.GetButtonDown("BattleEndTurn"))
            {
                _endTurnButton.onClick.Invoke();
                hideMenu();
            }
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

        /// <summary> Send the delegate to send the choosen target</summary>
        /// <param name="target"></param>
        private void sendTargetChoosen(BattleTarget target)
        {
            targetChoosen(target);
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
            actorChoosed(actor);
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
            actionChoosed(action);
            List<BattleTarget> validBattleTarget = requestValidTarget(action);
            _battleActionMenu.unFocusMenu();
            _battleTargetSelector.initMenu(validBattleTarget);
            _battleTargetSelector.focusMenu();
        }
        #endregion

    }

}
