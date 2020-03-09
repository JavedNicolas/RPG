using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System;

namespace RPG.Battle.UI
{
    using RPG.UI;
    using RPG.DataManagement;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class BattleMenu : MonoBehaviour
    {
        [SerializeField] BattleActorMenu _battleActorMenu;
        [SerializeField] BattleActionMenu _battleActionMenu;
        [SerializeField] BattleTargetSelector _battleTargetSelector;
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

        public void initMenus()
        {
            _battleActorMenu.setEventSystem(_eventSystem);
            _battleActorMenu.menuFinished = switchToActionMenu;
            _battleActorMenu.menuCanceled = delegate { switchToActorSelection(); };

            _battleActionMenu.initPooling();
            _battleActionMenu.setEventSystem(_eventSystem);
            _battleActionMenu.menuFinished = switchToTargetSelection;
            _battleActionMenu.menuCanceled = delegate { switchToActorSelection(); };

            _battleTargetSelector.setEventSystem(_eventSystem);
            _battleTargetSelector.menuFinished =  sendTargetChoosen;
            _battleTargetSelector.menuCanceled = delegate { switchToActionMenu(null); };

        }

        public void displayMenu(List<Being> actors)
        {
            _battleActionMenu.unFocusMenu();
            _battleTargetSelector.unFocusMenu();
            _battleActorMenu.initMenu(actors);
            _battleActorMenu.focusMenu();
        }

        private void sendTargetChoosen(BattleTarget target)
        {
            targetChoosen(target);
        }

        #region switch menus
        private void switchToActorSelection()
        {
            _battleActionMenu.unFocusMenu();
            _battleActorMenu.focusMenu();
        }

        /// <summary> Switch to action menu </summary>
        /// <param name="actor">the actor to init the menu with. If null just display the menu (used to come back from target selection)</param>
        private void switchToActionMenu(Being actor)
        {
            actorChoosed(actor);
            _battleActorMenu.unFocusMenu();
            if(actor != null)
                _battleActionMenu.initMenu(actor.actions);
            _battleActionMenu.focusMenu();
        }

        private void switchToTargetSelection(Action action)
        {
            actionChoosed(action);
            List<BattleTarget> validBattleTarget = requestValidTarget(action);
            Debug.Log(validBattleTarget.Count);
            _battleActionMenu.unFocusMenu();
            _battleTargetSelector.initMenu(validBattleTarget);
            _battleTargetSelector.focusMenu();
        }
        #endregion

    }

}
