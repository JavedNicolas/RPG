using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.EventSystems;
using RPG.Data;
using System.Linq;
using MultipleMenus;

namespace RPG.DungeonMode.UI
{
    public class TabMenu : MonoBehaviour
    {
        [SerializeField] List<TabMenuButton> _menuButtons;
        [SerializeField] ChoiceMenus _choiceMenus;
        [SerializeField] TabMenuButton choiceMenuButton;
        [SerializeField] MMenu _mMenu;

        public ChoiceMenus choiceMenus => _choiceMenus;

        bool _isChoiceDisplayed = false;

        private void Start()
        {
            // activate first button
            _mMenu.setSelectionSettings();
            isTabChoice(false);
        }

        private void Update()
        {
            if(_mMenu.getCurrentSelectedElement() != null && _mMenu.getCurrentSelectedElement().Count != 0)
            {
                TabMenuButton tabMenuButtonSelected = _mMenu.getCurrentSelectedElement()[0].GetComponent<TabMenuButton>();
                displayTab(tabMenuButtonSelected.menuToDisplay, tabMenuButtonSelected);
            }
        }

        public void display(bool display, bool isChoice = false)
        {
            gameObject.SetActive(display);
            isTabChoice(isChoice);
        }

        private void isTabChoice(bool displayChoice)
        {
            _isChoiceDisplayed = displayChoice;
            choiceMenuButton.gameObject.SetActive(displayChoice);
            displayTab(choiceMenuButton.menuToDisplay, choiceMenuButton);
        }

        private void displayTab(GameObject menuToDisplay, TabMenuButton menuButton)
        {
            if (menuToDisplay.activeSelf)
                return;

            _menuButtons.ForEach(x => {
                if (x.menuToDisplay == menuToDisplay)
                    x.menuToDisplay?.SetActive(true);
                else
                    x.menuToDisplay?.SetActive(false);
            });
            
        }
        
    }
}