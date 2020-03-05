using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class BattleMenu : MonoBehaviour
{
    #region editor variable
    [Header("Prefabs")]
    [SerializeField] GameObject _actionCategoryPrefab;
    [SerializeField] GameObject _actionPrefab;

    [Header("Menu container")]
    [SerializeField] RectTransform _menuParent;

    [Header("Mandatory Reference")]
    [SerializeField] EventSystem _eventSystem;
    [SerializeField] UpdateScrollViewBasedOnNavigation _scrollViewUpdater;
    #endregion

    INavigationSetter _navigationSetter;
    BattleMenuItemCreator _menuItemCreator;

    BattleMenuItems _menuGO;
    
    // the last selected item : Allow to refocus to correct item on cancel input
    GameObject _selectedCategory;
    GameObject _selectedAction;

    bool _displayMenu = true;
    bool _lockUpdate = false;

    #region delegate
    public delegate void ActionHasBeenChoosen(Action action);
    public ActionHasBeenChoosen actionHasBeenChosen;
    #endregion

    private void Awake()
    {
        _navigationSetter = GetComponent<INavigationSetter>();
        _menuItemCreator = new BattleMenuItemCreator();
    }

    private void FixedUpdate()
    {
        _menuParent.gameObject.SetActive(_displayMenu);

        if (!_lockUpdate && Input.GetButtonDown("Cancel"))
        {
            displayCategory();
        }
    }

    public void updateBattleMenu(Character currentCharacter)
    {
        if (!_displayMenu)
            return;

        if (_menuGO == null || _menuGO.categoryItems.Count == 0)
        {
            _menuParent.clearChild();
            _menuGO = _menuItemCreator.createMenu(currentCharacter, _actionCategoryPrefab, _actionPrefab, _menuParent);
            setOnClick();
            setNavigation();
            displayMenu();
        }

        _scrollViewUpdater.setContent(_menuParent);
    }

    public void resetMenu()
    {
        _menuGO = null;
        _selectedCategory = null;
        _selectedAction = null;
        _displayMenu = true;
    }

    #region menu setters
    private void setNavigation()
    {
        // set navigation for categories
        _navigationSetter.setNavigation(_menuGO.categoryItems.Select(x => x.gameObject.GetComponent<Button>()).ToList(), _eventSystem);
        // set navigation for each action in category
        _menuGO.categoryItems.ForEach(x => _navigationSetter.setNavigation(x.actionMenu.Select(y => y.gameObject.GetComponent<Button>()).ToList(), _eventSystem));
    }

    /// <summary>
    /// Set each on click delegate
    /// </summary>
    private void setOnClick()
    {
        // set onClick Actions
        _menuGO.categoryItems.ForEach(x =>
        {
            x.gameObject.GetComponent<Button>()?.onClick.AddListener(delegate {
                displayActions(x.actionMenu);
                _selectedCategory = x.gameObject;
            });

            // for each action menu set the delegate to be fired when clicked
            x.actionMenu.ForEach(a =>
            {
                a.gameObject.GetComponent<Button>()?.onClick.AddListener(delegate
                {
                    _lockUpdate = true;
                    fireDelegate(a.action);
                    _selectedAction = a.gameObject;
                });
            });
        });
    }


    #endregion

    #region menu displayer
    public void displayMenu()
    {
        if (_selectedAction == null)
            displayCategory();
        else
            displayActions(_menuGO.categoryItems.Find(x => x.gameObject == _selectedCategory).actionMenu);

        _lockUpdate = false;
    }

    /// <summary> Display the action sub menu </summary>
    /// <param name="actionMenuItems"></param>
    private void displayActions(List<ActionMenuItem> actionMenuItems)
    {
        _menuGO.categoryItems.ForEach(x => x.gameObject.SetActive(false));
        actionMenuItems.ForEach(x => x.gameObject.SetActive(true));

        if (_selectedAction != null)
            _eventSystem.SetSelectedGameObject(_selectedAction);
        else
            _eventSystem.SetSelectedGameObject(actionMenuItems == null || actionMenuItems.Count == 0 ? null : actionMenuItems.First().gameObject);
    }

    /// <summary> Display the action category menu </summary>
    private void displayCategory()
    {
        _menuGO.categoryItems.ForEach(x =>
        {
            x.gameObject.SetActive(true);
            x.actionMenu.ForEach(y => y.gameObject.SetActive(false));
        });

        _selectedAction = null;

        if (_selectedCategory != null)
            _eventSystem.SetSelectedGameObject(_selectedCategory);
        else if (_menuGO != null || _menuGO.categoryItems != null || _menuGO.categoryItems.Count != 0)
            _eventSystem.SetSelectedGameObject(_menuGO.categoryItems.First().gameObject);
        else
            _eventSystem.SetSelectedGameObject(null);
    }
    #endregion

    public void displayMenu(bool display)
    {
        _displayMenu = display;
    }

    private void fireDelegate(Action action)
    {
        actionHasBeenChosen(action);
    }


}
