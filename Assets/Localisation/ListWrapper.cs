using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ListWrapper<T>
{
    [HideInInspector]
    public string name;
    [SerializeField] List<T> _databaseElements;
     
    public ListWrapper(List<T> elements)
    {
        this._databaseElements = elements;
    }

    public List<T> getElements() { return _databaseElements;  }
}