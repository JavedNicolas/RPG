using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class LocalizationData
{
    [SerializeField] public List<Language> languages = new List<Language>();
}

[System.Serializable]
public class Language
{
    [SerializeField] public string name;
    [SerializeField] public List<LocalizationElement> elements = new List<LocalizationElement>();

    public Language() { }

    public Language(string lang, List<LocalizationElement> elements)
    {
        this.name = lang;
        this.elements = elements;
    }
}

[System.Serializable]
public class LocalizationElement
{
    [SerializeField] public string key;
    [SerializeField] public string value;
    public Object obj;
    [SerializeField] public KeyTargetType targetType;

    public LocalizationElement()
    {
    }

    public LocalizationElement(string key, KeyTargetType targetType,string value)
    {
        this.key = key;
        this.value = value;
        if (targetType != KeyTargetType.Text)
            obj = AssetDatabase.LoadAssetAtPath<Object>(value);
        this.targetType = targetType;
    }
}

[System.Serializable]
public enum KeyTargetType { Text, AudioClip, Sprite }
