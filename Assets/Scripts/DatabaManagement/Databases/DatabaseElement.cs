using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace RPG.DataManagement
{
    [System.Serializable]
    public class DatabaseElement : ScriptableObject
    {
        [SerializeField] public new string name;
        private string assetPath;

        public void init(DatabaseElement databaseElement)
        {
            this.name = databaseElement.name;
        }

        [Button("Update File Name"), PropertyOrder(-1)]
        private void rename()
        {
            if (name != null || name != "")
            {
                assetPath = AssetDatabase.GetAssetPath(this.GetInstanceID());
                AssetDatabase.RenameAsset(assetPath, name);
            }
        }

        [Button("Delete"), PropertySpace(SpaceBefore = 50)]
        private void delete()
        {
            if (EditorUtility.DisplayDialog("Delete", "Are you sure you want to delete " + name, "Yes", "No"))
            {
                DestroyImmediate(this, true);
            }
        }
    }
}