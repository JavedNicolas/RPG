using UnityEngine;
using System.Collections;

namespace RPG.DataModule
{
    [System.Serializable]
    [CreateAssetMenu(menuName = AssetsPath.ENEMY_SO_MENU_NAME)]
    public class Enemy : Being
    {
        public void init(Enemy enemy)
        {
            base.init(enemy);
        }
    }
}
