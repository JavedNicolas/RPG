using UnityEngine;
using UnityEditor;

namespace RPG.DataModule
{
    using RPG.GlobalModule;

    [CreateAssetMenu(menuName = AssetsPath.ITEM_DB_SO_MENU_NAME)]
    public class ItemDatabase : Database<Item>
    {
        protected override string elementFolderPath => AssetsPath.DATABASE_ELEMENT_BASE_FOLDER + AssetsPath.ITEM_NAME;
    }
}