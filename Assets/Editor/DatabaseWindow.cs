using UnityEditor;
using Sirenix.Utilities.Editor;
using Sirenix.OdinInspector.Editor;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Editor {

    using RPG.DataManagement;

    public class DatabaseWindow: OdinMenuEditorWindow
    {
        CharacterDatabase _characterDatabase;
        EnemyDatabase _enemyDatabase;
        ItemDatabase _itemDatabase;
        ActionDatabase _actionDatabase;

        public OdinMenuTree tree;

       [UnityEditor.MenuItem("Tools/Database")]
        static void ShowWindow()
        {
            GetWindow<DatabaseWindow>().Show();
        
        }

        #region functions
        protected override OdinMenuTree BuildMenuTree()
        {
            tree = new OdinMenuTree(false);
            tree.Config.DrawSearchToolbar = true;

            // load the databases
            setAndLoadDatabases();

            return tree;
        }

        /// <summary>
        /// Set the tree element
        /// </summary>
        public void setTreeElements()
        {
            tree.Add("Characters", _characterDatabase);
            tree.Add("Enemy", _enemyDatabase);
            tree.Add("Items", _itemDatabase);
            tree.Add("Actions", _actionDatabase);

        }
        #endregion


        #region database region
        /// <summary>
        /// init databases and databases Wrappers
        /// </summary>
        public void setAndLoadDatabases()
        {
            _characterDatabase = Resources.LoadAll<CharacterDatabase>("")[0];
            _enemyDatabase = Resources.LoadAll<EnemyDatabase>("")[0];
            _itemDatabase = Resources.LoadAll<ItemDatabase>("")[0];
            _actionDatabase = Resources.LoadAll<ActionDatabase>("")[0];

            reloadDatabases();
            setTreeElements();
        }

        /// <summary>
        /// reload database content
        /// </summary>
        private void reloadDatabases()
        {
            _characterDatabase?.reloadElement();
            _enemyDatabase?.reloadElement();
            _itemDatabase?.reloadElement();
            _actionDatabase?.reloadElement();
        }
        #endregion
    }
}
