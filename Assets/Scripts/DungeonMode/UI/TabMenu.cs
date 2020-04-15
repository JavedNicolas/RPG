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
            displayChoice(false);
        }

        private void Update()
        {
            TabMenuButton tabMenuButtonSelected = _mMenu.getCurrentSelectedElement().First().GetComponent<TabMenuButton>();
            display(tabMenuButtonSelected.menuToDisplay, tabMenuButtonSelected);
        }

        public void displayChoice(bool displayChoice)
        {
            _isChoiceDisplayed = displayChoice;
            choiceMenuButton.gameObject.SetActive(displayChoice);
            display(choiceMenuButton.menuToDisplay, choiceMenuButton);
        }

        public void display(GameObject menuToDisplay, TabMenuButton menuButton)
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