using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace RPG.Data
{
    [System.Serializable]
    public class DatabaseElement : ScriptableObject
    {
        [SerializeField] public new string name;
        [SerializeField] public string description;
        [SerializeField] public Sprite icon;
        [SerializeField] public Sprite smallImage;
        [SerializeField] public Sprite mediumImage;
        [SerializeField] public Sprite fullImage;

        private string assetPath;

        public virtual void initEmpty()
        {
            this.name = "Empty";
            this.description = "Empty";
            this.icon = null;
            this.smallImage = null;
            this.mediumImage = null;
            this.fullImage = null;
        }

        public void init(DatabaseElement databaseElement)
        {
            this.name = databaseElement.name;
            this.description = databaseElement.description;
            this.icon = databaseElement.icon;
            this.smallImage = databaseElement.smallImage;
            this.mediumImage = databaseElement.mediumImage;
            this.fullImage = databaseElement.fullImage;
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