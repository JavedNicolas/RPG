using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace RPG.UI
{
    public class MenuItem<T>
    {
        public T element;
        public MenuButton button;
        public Sprite icon;
        public GameObject gameObject;
    }
}

