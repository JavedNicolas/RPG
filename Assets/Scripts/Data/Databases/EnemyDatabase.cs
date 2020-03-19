using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace RPG.Data
{
    [CreateAssetMenu(menuName = AssetsPath.ENEMY_DB_SO_MENU_NAME)]
    public class EnemyDatabase : Database<Enemy>
    {
        protected override string elementFolderPath => AssetsPath.DATABASE_ELEMENT_BASE_FOLDER + AssetsPath.ENEMY_NAME;

    }
}