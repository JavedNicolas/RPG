using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.DataManagement;

namespace RPG.Battle
{
    public class ActionCategoryMenuItem
    {
        public string name;
        public bool isCategory;
        public Sprite icon;
        public GameObject gameObject;
        public List<ActionMenuItem> actionMenu;

    }
}