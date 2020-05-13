using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace RPG.DataModule
{
    using RPG.GlobalModule;

    [System.Serializable]
    [CreateAssetMenu(menuName = AssetsPath.CHARACTER_SO_MENU_NAME)]
    public class Character : Being
    {
        [SerializeField] int _actionPoint;
        public int actionPoint => _actionPoint;

        public void init(Character character)
        {
            base.init(character);
            _actionPoint = character.actionPoint;
        }
    }
}
