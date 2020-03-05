using UnityEngine;
using RPG.DataManagement;

namespace RPG.Battle
{
    public class ActionMenuItem
    {
        public string name;
        public Sprite icon;
        public string apCost;
        public GameObject gameObject;
        public Action action;
    }
}