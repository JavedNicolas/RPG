﻿using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;


namespace RPG.Data
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

        public virtual T getElement(int atIndex)
        {
            if (atIndex >= _elements.Count)
                return default;

            return _elements[atIndex];
        }

        public virtual List<T> getElements(Predicate<T> filter = null)
        {
            List<T> elementsToReturn = _elements;

            if (filter != null)
                elementsToReturn = elementsToReturn.FindAll(filter);

            return elementsToReturn;
        }

        public virtual T getRandomElement(Predicate<T> filter = null)
        {
            List<T> randomElements = getRandomElements(1, true, filter);

            if (randomElements.Count == 0)
                return default;

            return randomElements[0];
        }

        public virtual List<T> getRandomElements(int numberOfElementToGet, bool allowDuplicate, Predicate<T> filter = null)
        {
            List<T> elementMatching = new List<T>();
            List<T> elementsToReturn = new List<T>();
            Predicate<T> duplicateFilter = !allowDuplicate ? new Predicate<T>(x => !elementsToReturn.Contains(x)) : null;

            if (isListNullOrEmpty())
                return new List<T>();

            elementMatching = _elements;

            if (!allowDuplicate)
                elementMatching = elementMatching.FindAll(duplicateFilter);

            if (filter != null)
                elementMatching = elementMatching.FindAll(filter);

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
        public virtual void reloadElement()
        {
            loadDatabase();
        }

        [Button("Add"), PropertyOrder(-1), PropertySpace(SpaceAfter = 10)]
        public virtual void addElement()
        {
            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<T>(), AssetsPath.RESOURCES_FOLDER_PATH + elementFolderPath + "/New " + typeof(T).ToString() + creationAssetNumber + ".asset");
            creationAssetNumber++;
        }

        #endregion


        public bool isListNullOrEmpty() { return _elements == null || _elements.Count == 0 ? true : false; }
    }
}