using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = Path.ENEMY_DB_SO_MENU_NAME)]
public class EnemyDatabase : Database<Enemy>
{
    protected override string elementFolderPath => Path.DATABASE_ELEMENT_BASE_FOLDER + Path.ENEMY_NAME;

}