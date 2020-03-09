using UnityEngine;
using UnityEditor;

namespace RPG.DataManagement
{
    [CreateAssetMenu(menuName = Path.ITEM_DB_SO_MENU_NAME)]
    public class ItemDatabase : Database<Item>
    {
        protected override string elementFolderPath => Path.DATABASE_ELEMENT_BASE_FOLDER + Path.ITEM_NAME;
    }
}