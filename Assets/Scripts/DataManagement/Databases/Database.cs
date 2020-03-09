using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;


namespace RPG.DataManagement
{
    public abstract class Database<T> : ScriptableObject where T : ScriptableObject
    {
        [SerializeField] protected abstract string elementFolderPath { get; }
        protected int creationAssetNumber = 0;

        [SerializeField] protected List<T> _elements;

        private void OnEnable()
        {
            loadDatabase();
        }

        /// <summary> load the database from the json File</summary>
        /// <param name="databaseJsonFilePath"></param>
        public void loadDatabase()
        {

            _elements = Resources.LoadAll<T>(elementFolderPath).ToList();
        }

        #region getter

        public T getElement(int atIndex)
        {
            if (atIndex >= _elements.Count)
                return default;

            return _elements[atIndex];
        }

        public List<T> getRandomElements(int numberOfElementToGet, bool allowDuplicate)
        {
            List<T> elementMatching = new List<T>();
            List<T> elementsToReturn = new List<T>();
            Predicate<T> duplicateFilter = !allowDuplicate ? new Predicate<T>(x => !elementsToReturn.Contains(x)) : null;

            if (isListNullOrEmpty())
                return new List<T>();

            if (!allowDuplicate)
                elementMatching = _elements.FindAll(duplicateFilter);
            else
                elementMatching = _elements;

            for (int i = 0; i < numberOfElementToGet; i++)
            {
                int randomIndex = UnityEngine.Random.Range(0, elementMatching.Count);
                elementsToReturn.Add(elementMatching[randomIndex]);
            }

            return elementsToReturn;
        }
        #endregion

        #region ondin inspector 
        [Button("Reload Database")]
        [PropertyOrder(-2), PropertySpace(SpaceBefore = 10)]
        public void reloadElement()
        {
            loadDatabase();
        }

        [Button("Add"), PropertyOrder(-1), PropertySpace(SpaceAfter = 10)]
        public void addElement()
        {
            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<T>(), Path.RESOURCES_FOLDER_PATH + elementFolderPath + "/New " + typeof(T).ToString() + creationAssetNumber + ".asset");
            creationAssetNumber++;
        }

        #endregion


        public bool isListNullOrEmpty() { return _elements == null || _elements.Count == 0 ? true : false; }
    }
}