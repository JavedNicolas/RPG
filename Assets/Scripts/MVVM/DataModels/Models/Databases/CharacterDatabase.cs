using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace RPG.DataModule
{
    [CreateAssetMenu(menuName = AssetsPath.CHARACTER_DB_SO_MENU_NAME)]
    public class CharacterDatabase : Database<Character>
    {
        protected override string elementFolderPath => AssetsPath.DATABASE_ELEMENT_BASE_FOLDER + AssetsPath.CHARACTER_NAME;
    }
}
