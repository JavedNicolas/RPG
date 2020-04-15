using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace MultipleMenus
{
    public class MEventSystem : MonoBehaviour
    {
        [SerializeField] EventSystem _eventSystem;

        [SerializeField] bool _loop;
        [SerializeField] List<MMenu> _menus;
        MMenu _currentFocusedMenu;
        MMenuInput _currentMenuInput;

        private void Update()
        {
            if (Input.GetKeyDown(_currentMenuInput.nextMenu))
                switchMenu(true);
            if (Input.GetKeyDown(_currentMenuInput.previousMenu))
                switchMenu(false);
        }

        void switchMenu(bool nextMenu)
        {
            int currentMenuIndex = _menus.FindIndex(x => x == _currentFocusedMenu);
            int newMenuIndex = nextMenu ? currentMenuIndex + 1 : currentMenuIndex - 1;

            if (newMenuIndex >= _menus.Count)

                newMenuIndex = _loop ? 0 : currentMenuIndex;
       
            if (newMenuIndex < 0)
                newMenuIndex = _loop ? _menus.Count - 1 : 0;

            assigneCurrentMenu(_menus[newMenuIndex]);
        }

        public void assigneCurrentMenu(MMenu menu)
        {
            _currentFocusedMenu = menu;
            //_eventSystem.SetSelectedGameObject(_currentFocusedMenu.getCurrentSelectedElement().gameObject);
        }
    }
}