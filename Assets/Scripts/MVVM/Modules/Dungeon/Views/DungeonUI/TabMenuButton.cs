using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace RPG.DungeonModule.View
{
    public class TabMenuButton : MonoBehaviour
    {
        [SerializeField] private GameObject _menuToDisplay;

        public GameObject menuToDisplay => _menuToDisplay;

    }
}