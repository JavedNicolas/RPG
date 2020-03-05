using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

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
        if(atIndex >= _elements.Count)
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




/*
public abstract class Database<T> where T : DatabaseElement
{
    public List<T> elements { get; private set; }

    protected string _jsonFilePath;

    protected Database(string jsonFilePath)
    { 
        _jsonFilePath = jsonFilePath;
        loadDatabase(_jsonFilePath);
    }

    #region abstract functions
    
    #endregion

    #region load and save
    /// <summary>
    /// load the database from the json File
    /// </summary>
    /// <param name="databaseJsonFilePath"></param>
    public void loadDatabase(string databaseJsonFilePath)
    {
        _jsonFilePath = databaseJsonFilePath;

        if (!File.Exists(_jsonFilePath))
            File.WriteAllText(_jsonFilePath, "");

        string jsonContent = File.ReadAllText(databaseJsonFilePath);
        JsonWrappingClass<T> wrapper = JsonUtility.FromJson<JsonWrappingClass<T>>(jsonContent);

        if (wrapper == null)
            elements = new List<T>();
        else
            elements = wrapper.elements;
    }

    /// <summary>
    /// update the database with the current data
    /// </summary>
    public void updateFromCurrentData()
    {
        JsonWrappingClass<T> wrapingClass = new JsonWrappingClass<T>(elements);
        string dataToSave = JsonUtility.ToJson(wrapingClass, true);
        File.WriteAllText(_jsonFilePath, dataToSave);
    }

    /// <summary>
    /// Update the databaseWith fresh elements
    /// /!\ THIS WILL REMPLACE ALL DATA WITH THE ONE IN PARAMETER /!\
    /// </summary>
    /// <param name="elements">The list of all the elements the datbase will save </param>
    public void updateDatabase(List<T> elements)
    {
        JsonWrappingClass<T> wrapingClass = new JsonWrappingClass<T>(elements);
        string dataToSave = JsonUtility.ToJson(wrapingClass, true);
        File.WriteAllText(_jsonFilePath, dataToSave);
    }
    #endregion

    #region editor database function
    public DatabaseWrapper<T> getDatabaseWrapper()
    {
        loadDatabase(_jsonFilePath);
        return new DatabaseWrapper<T>(this);
    }

    public void addElement(T element)
    {
        if (element != null && element.name != null && element.name != "")
            elements.Add(element);
        else
            EditorUtility.DisplayDialog("Error", "The name field cannot be empty !", "Ok");
    }

    public void removeElement(T element)
    {

    }

    #endregion

    #region getter
    public List<T> getRandomElements(int numberOfElementToGet, bool allowDuplicate)
    {
        List<T> elementMatching = new List<T>();
        List<T> elementsToReturn = new List<T>();
        Predicate<T> duplicateFilter = !allowDuplicate ? new Predicate<T>(x => !elementsToReturn.Contains(x)) : null;

        if (!allowDuplicate)
            elementMatching = elements.FindAll(duplicateFilter);
        else
            elementMatching = elements;

        for (int i = 0; i < numberOfElementToGet; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, elementMatching.Count);
            elementsToReturn.Add(elementMatching[randomIndex]);
        }

        return elementsToReturn;
    }
    #endregion
}
*/
