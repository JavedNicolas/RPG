using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

namespace RPG.Battle.UI
{
    using RPG.Data;
    using RPG.UI;

    public class BattleActionMenu : Menu<Action>
    {
        #region editor variable
        [Header("Prefabs")]
        [SerializeField] GameObject _categoryPrefab;
        [SerializeField] GameObject _actionPrefab;

        [Header("Menu container")]
        [SerializeField] RectTransform _menuParent;

        [Header("Mandatory Reference")]
        [SerializeField] UpdateScrollViewBasedOnNavigation _scrollViewUpdater;
        #endregion

        BattleMenuSetter _menuSetter;

        BattleActionMenuItems _menuGO;

        // the last selected item : Allow to refocus to correct item on cancel input
        GameObject _selectedCategory;
        GameObject _selectedAction;

        #region object pooling
        public List<GameObject> _categoriesPool = new List<GameObject>();
        public List<GameObject> _actionsPool = new List<GameObject>();
        #endregion

        public override void initMenu(List<Action> settersList)
        {
            // reset the previously selected Go (set when switching from category to action...)
            _selectedCategory = null;
            _selectedAction = null;

            // create the menu
            _menuSetter = new BattleMenuSetter(_categoriesPool, _actionsPool);
            _menuGO = _menuSetter.getCurrentMenu(settersList);
            setOnClick();
            setNavigation();

            // set the scroll content
            _scrollViewUpdater.setContent(_menuParent);
        }


        #region menu setters
        private void setNavigation()
        {
            // set navigation for categories
            navigationSetter.setNavigation(_menuGO.categoryItems.Select(x => x.button as Button).ToList(), _eventSystem);
            // set navigation for each action in category
            _menuGO.categoryItems.ForEach(x => navigationSetter.setNavigation(x.element.Select(y => y.button as Button).ToList(), _eventSystem));
        }

        /// <summary>
        /// Set each on click delegate
        /// </summary>
        private void setOnClick()
        {
            // set onClick Actions
            _menuGO.categoryItems.ForEach(x =>
            {
                // add listen to onclick
               x.button?.onClick.RemoveAllListeners();
               x.button?.onClick.AddListener(delegate
                {
                    _selectedCategory = x.gameObject;
                    displayActions(x.element);
                });
                // add a delegate to the onCancel
                x.button.onCancel = null;
                x.button.onCancel = menuCanceled;

                // for each action menu set the delegate to be fired when clicked
                x.element.ForEach(a =>
                {
                    a.button?.onClick.RemoveAllListeners();
                    a.button?.onClick.AddListener(delegate
                    {
                        _selectedAction = a.gameObject;
                        fireDelegate(a.element);
                    });
                    a.button.onCancel = delegate { displayCategory(); };
                });
            });
        }

        /// <summary> sende the delegate </summary>
        /// <param name="action"></param>
        private void fireDelegate(Action action)
        {
            menuFinished(action);
        }
        #endregion

        #region menu displayer
        /// <summary> display the menu :
        /// Display the categories if there is no there was no lastActionSelected, display actions if there is</summary>
        public void displayMenu()
        {
            if (_selectedAction == null)
                displayCategory();
            else
                displayActions(_menuGO.categoryItems.Find(x => x.gameObject == _selectedCategory).element);

        }

        /// <summary> Display the action category menu </summary>
        private void displayCategory()
        {
            _menuGO.categoryItems.ForEach(x =>
            {
                x.button.gameObject.SetActive(true);
                x.element.ForEach(y => y.gameObject.SetActive(false));
            });

            _selectedAction = null;

            if (_selectedCategory != null)
                _eventSystem.SetSelectedGameObject(_selectedCategory);
            else if (_menuGO != null && _menuGO.categoryItems != null && _menuGO.categoryItems.Count != 0)
                _eventSystem.SetSelectedGameObject(_menuGO.categoryItems.First().gameObject);
            else
                _eventSystem.SetSelectedGameObject(null);

        }

        /// <summary> Display the action sub menu </summary>
        /// <param name="actionMenuItems"></param>
        private void displayActions(List<ActionMenuItem> actionMenuItems)
        {
            _menuGO.categoryItems.ForEach(x => x.button.gameObject.SetActive(false));
            actionMenuItems.ForEach(x => x.gameObject.SetActive(true));

            if (_selectedAction != null)
                _eventSystem.SetSelectedGameObject(_selectedAction);
            else
                _eventSystem.SetSelectedGameObject(actionMenuItems == null || actionMenuItems.Count == 0 ? null : actionMenuItems.First().gameObject);
        }
        #endregion

        /// <summary> override allowing to custom how the selection is updated when lost </summary>
        protected override void updateSelectionWhenLost()
        {
            if (_menuGO != null && _menuGO.categoryItems.Count != 0 && _eventSystem.currentSelectedGameObject == null)
            {
                if (_selectedCategory != null)
                    _eventSystem.SetSelectedGameObject(_menuGO.categoryItems.Find(x => x.gameObject == _selectedCategory).element.Find(x => x.button.interactable).gameObject);
                else
                    _eventSystem.SetSelectedGameObject(_menuGO.categoryItems.Find(x => x.button.interactable).gameObject);
            }
                
        }

        /// <summary> focus the menu. Used when the </summary>
        public override void focusMenu()
        {
            if (_eventSystem == null || _menuGO == null || _menuGO.categoryItems == null || _menuGO.categoryItems.Count == 0)
                return;

            gameObject.SetActive(true);
            displayMenu();
        }

        /// <summary> un focus the menu.Used when the menu is not needed anymore </summary>
        public override void unFocusMenu()
        {
            gameObject.SetActive(false);
        }
    }
}
