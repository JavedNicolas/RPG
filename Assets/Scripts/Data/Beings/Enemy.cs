﻿using UnityEngine;
using System.Collections;

namespace RPG.Data
{
    [System.Serializable]
    [CreateAssetMenu(menuName = Path.ENEMY_SO_MENU_NAME)]
    public class Enemy : Being
    {
        public void init(Enemy enemy)
        {
            base.init(enemy);
        }
    }
}