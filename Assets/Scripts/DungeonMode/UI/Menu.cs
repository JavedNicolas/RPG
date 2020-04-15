using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.EventSystems;
using RPG.Data;
using System.Linq;

namespace RPG.DungeonMode.UI
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] List<MenuButton> _menuButtons;
        [SerializeField] ChoiceMenus _choiceMenus;
        [SerializeField] MenuButton choiceMenuButton;

        public ChoiceMenus choiceMenus => _choiceMenus;

        bool _isChoiceDisplayed = false;

        private void Start()
        {
            // activate first button
        }

        public void displayChoice(bool displayChoice)
        {
            _isChoiceDisplayed = displayChoice;
            choiceMenuButton.gameObject.SetActive(displayChoice);
            display(choiceMenuButton.menuToDisplay, choiceMenuButton);
        }

        public void display(GameObject menuToDisplay, MenuButton menuButton)
        {
            _menuButtons.ForEach(x => x.menuToDisplay?.SetActive(false));
            menuToDisplay?.SetActive(true);
        }
        
    }
}