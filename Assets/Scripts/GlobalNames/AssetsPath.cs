using UnityEngine;
using System.Collections;

namespace RPG.Data
{
    public static class AssetsPath
    {
        #region databases
        public const string DATABASE_SO_MENU_NAME = "Databases/";

        public const string CHARACTER_DATABASE_SO_NAME = "Character_Database";
        public const string ENEMY_DATABASE_SO_NAME = "Enemy_Database";
        public const string ITEM_DATABASE_SO_NAME = "Item_Database";
        public const string ACTION_DATABASE_SO_NAME = "Action_Database";

        public const string CHARACTER_DB_SO_MENU_NAME = DATABASE_SO_MENU_NAME + CHARACTER_DATABASE_SO_NAME;
        public const string ENEMY_DB_SO_MENU_NAME = DATABASE_SO_MENU_NAME + ENEMY_DATABASE_SO_NAME;
        public const string ITEM_DB_SO_MENU_NAME = DATABASE_SO_MENU_NAME + ITEM_DATABASE_SO_NAME;
        public const string ACTION_DB_SO_MENU_NAME = DATABASE_SO_MENU_NAME + ACTION_DATABASE_SO_NAME;
        #endregion

        #region databasesElements
        public const string DATABASE_ELEMENTS_SO_MENU_NAME = "DatabaseElements/";
        public const string DATABASE_ELEMENT_BASE_FOLDER = "DatabaseElements/";

        public const string CHARACTER_NAME = "Characters";
        public const string ENEMY_NAME = "Enemies";
        public const string ITEM_NAME = "Items";
        public const string ACTION_NAME = "Actions";
        public const string ACTION_EFFECT_NAME = "Action Effects/";

        public const string CHARACTER_SO_MENU_NAME = DATABASE_ELEMENTS_SO_MENU_NAME + CHARACTER_NAME;
        public const string ENEMY_SO_MENU_NAME = DATABASE_ELEMENTS_SO_MENU_NAME + ENEMY_NAME;
        public const string ITEM_SO_MENU_NAME = DATABASE_ELEMENTS_SO_MENU_NAME + ITEM_NAME;
        public const string ACTION_SO_MENU_NAME = DATABASE_ELEMENTS_SO_MENU_NAME + ACTION_NAME;
        public const string ACTION_EFFECT_SO_MENU_NAME = DATABASE_ELEMENTS_SO_MENU_NAME + ACTION_EFFECT_NAME;
        #endregion

        public const string RESOURCES_FOLDER_PATH = "Assets/Resources/";

    }
}
