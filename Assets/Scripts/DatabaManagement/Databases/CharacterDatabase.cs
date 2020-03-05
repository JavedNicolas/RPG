using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace RPG.DataManagement
{
    [CreateAssetMenu(menuName = Path.CHARACTER_DB_SO_MENU_NAME)]
    public class CharacterDatabase : Database<Character>
    {
        protected override string elementFolderPath => Path.DATABASE_ELEMENT_BASE_FOLDER + Path.CHARACTER_NAME;
    }
}
