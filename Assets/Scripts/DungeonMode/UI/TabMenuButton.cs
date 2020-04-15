using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace RPG.DungeonMode.UI
{
    public class TabMenuButton : MonoBehaviour
    {
        [SerializeField] private GameObject _menuToDisplay;

        public GameObject menuToDisplay => _menuToDisplay;

    }
}