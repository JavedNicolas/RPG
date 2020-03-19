using UnityEngine;
using System.Collections;

namespace RPG.Data
{
    [CreateAssetMenu(menuName = AssetsPath.ACTION_DB_SO_MENU_NAME)]
    public class ActionDatabase : Database<Action>
    {
        protected override string elementFolderPath => AssetsPath.DATABASE_ELEMENT_BASE_FOLDER + AssetsPath.ACTION_NAME;
    }
}

