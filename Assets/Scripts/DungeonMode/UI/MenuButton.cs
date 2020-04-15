using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace RPG.DungeonMode.UI
{
    public class MenuButton : MonoBehaviour
    {
        [SerializeField] private GameObject _menuToDisplay;
        [SerializeField] Menu _menu;
        [SerializeField] Button _button;

        public GameObject menuToDisplay => _menuToDisplay;

        private void Start()
        {
            _button.onClick.AddListener(display);
        }

        public void display()
        {
            _menu.display(_menuToDisplay, this);
        }
    }
}