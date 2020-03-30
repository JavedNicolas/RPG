using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Localization
{ 
    public string localizationFilePath = LocalizationSettings.LocalizationFilePath;

    [SerializeField] LocalizationData _localizationDatas;
    public LocalizationData localizationDatas => _localizationDatas;

    [SerializeField] string _currentLang;
    public string currentLang { get => _currentLang; }

    #region Singleton
    private static Localization _instance = new Localization();
    public static Localization instance => _instance;
    #endregion

    #region Constructors
    private Localization()
    {
        _localizationDatas = new LocalizationData();
        loadLocalizedText();
        _currentLang = hasLanguageDataLoaded() ? _localizationDatas.languages[0].name : ""; 
    }

    #endregion

    #region language handling
    public bool setLang(string lang)
    {
        if (!hasLanguageDataLoaded() && !_localizationDatas.languages.Exists(x => x.name == lang))
            return false;
        
        _currentLang = lang;
        return true;
   
    }

    /// <summary>
    /// Add a new language to the localization table
    /// </summary>
    /// <param name="lang">The lang to add</param>
    /// <param name="elements">The localization keys and localized texts</param>
    public void addLang(string lang, LocalizationElement[] elements)
    {
        Language language = new Language(lang, elements.ToList());
        _localizationDatas.languages.Add(language);
        saveLocalizedText();
    }

    /// <summary>
    /// Update the text for a language
    /// </summary>
    /// <param name="lang">the lang</param>
    /// <param name="elements"> the elements to update</param>
    /// <returns></returns>
    public bool updateLang(string lang, List<LocalizationElement> elements)
    {
        int langIndex = _localizationDatas.languages.FindIndex(x => x.name == lang);
        if (langIndex == -1)
            return false;

        foreach(LocalizationElement element in elements)
        {
            if (!updateValue(element, _localizationDatas.languages[langIndex].name))
                return false;
        }

        saveLocalizedText();
        return true;
    }

    public void removeLang(string lang)
    {
        int langIndex = _localizationDatas.languages.FindIndex(x => x.name == lang);
        _localizationDatas.languages.RemoveAt(langIndex);
        saveLocalizedText();
    }
    #endregion

    #region Handling Values
    /// <summary>
    /// get the text for a key
    /// </summary>
    /// <param name="key">The key to look for</param>
    /// <param name="lang">The lang</param>
    /// <returns></returns>
    public string getTextForKey(string key, string lang = "")
    {
        LocalizationElement localizationItem = getLangElements(lang == "" ? _currentLang : lang).Find(x => x.key == key && x.targetType == KeyTargetType.Text);
        return localizationItem == null ? "" : localizationItem.value;
    }

    /// <summary>
    /// get the Object for a key
    /// </summary>
    /// <param name="key">The key to look for</param>
    /// <param name="targetType">The target Type to look for</param>
    /// <param name="lang">The lang</param>
    /// <returns></returns>
    public UnityEngine.Object getLocalizationForKey(string key, KeyTargetType targetType, string lang = "")
    {
        if (targetType == KeyTargetType.Text)
            return null;

        LocalizationElement localizationItem = getLangElements(lang == "" ? _currentLang : lang).Find(x => x.key == key && x.targetType == targetType);
        return localizationItem == null ? null : localizationItem.obj;
    }

    public bool updateValue(LocalizationElement element, string lang)
    {
        int langIndex = _localizationDatas.languages.FindIndex(x => x.name == lang);
        if (langIndex >= _localizationDatas.languages.Count)
            return false;

        int keyIndex = _localizationDatas.languages[langIndex].elements.FindIndex(x => x.key == element.key);

        if (keyIndex >= _localizationDatas.languages[langIndex].elements.Count)
            return false;

        _localizationDatas.languages[langIndex].elements[keyIndex].value = element.value;
        return true;
    }

    /// <summary>
    /// return the number of element of the english Language
    /// </summary>
    public int getTextElementCount()
    {
        return _localizationDatas == null || _localizationDatas.languages.Count == 0 ? 0 : _localizationDatas.languages[0].elements.Count;
    }

    #endregion

    #region handling keys
    /// <summary>
    /// Get a list of all the keys
    /// </summary>
    /// <returns></returns>
    public string[] getKeys()
    {
        if (_localizationDatas.languages.Count == 0)
            return default;

        List<string> keys = new List<string>();
        List<LocalizationElement> localisationElements = _localizationDatas.languages[0].elements;

        foreach (LocalizationElement elements in localisationElements)
        {
            keys.Add(elements.key);
        }

        return keys.ToArray();
    }

    /// <summary>
    /// Get a list of all the keys
    /// </summary>
    /// <param name="targetType">the target type to look for </param>
    /// <returns></returns>
    public string[] getKeys(KeyTargetType targetType)
    {
        if (_localizationDatas.languages.Count == 0)
            return default;

        List<string> keys = new List<string>();
        List<LocalizationElement> localisationElements = _localizationDatas.languages[0].elements.FindAll(x => x.targetType == targetType);

        foreach (LocalizationElement elements in localisationElements)
        {
            keys.Add(elements.key);
        }

        return keys.ToArray();
    }

    /// <summary>
    /// Add a new key to all the language
    /// </summary>
    /// <param name="key">The key to add</param>
    /// <param name="newText">The text for each language</param>
    /// <param name="langs">The language in the same order as the text array</param>
    public void addKeyToLanguages(string key, KeyTargetType targetType, string[] newText, string[] langs)
    {
        for (int i = 0; i < langs.Length; i++)
        {
            int langIndex = _localizationDatas.languages.FindIndex(x => x.name == langs[i]);
            _localizationDatas.languages[langIndex].elements.Add(new LocalizationElement(key, targetType, newText[i]));
        }
        saveLocalizedText();
    }

    /// <summary>
    /// remove a key from all languages
    /// </summary>
    /// <param name="key"></param>
    public void removeKey(string key)
    {
        for (int i = 0; i < _localizationDatas.languages.Count; i++)
        {
            int index = _localizationDatas.languages[i].elements.FindIndex(x => x.key == key);
            if (index != -1)
                _localizationDatas.languages[i].elements.RemoveAt(index);
        }
        saveLocalizedText();
    }

    public void updateKeysName(List<string> keysNameModifed, string[] oldKeys)
    {
        for (int i = 0; i < _localizationDatas.languages.Count; i++)
        {
            for (int j = 0; j < keysNameModifed.Count; j++)
            {
                int elementIndex = _localizationDatas.languages[i].elements.FindIndex(x => x.key == oldKeys[j]);
                if (elementIndex < _localizationDatas.languages[i].elements.Count)
                    _localizationDatas.languages[i].elements[elementIndex].key = keysNameModifed[j];
            }
        }
        saveLocalizedText();
    }

    #endregion

    #region save and load languages
    /// <summary>
    /// Save the localized text currently opened
    /// </summary>
    public void saveLocalizedText()
    {
        string dataAsJson = JsonUtility.ToJson(_localizationDatas, true);
        File.WriteAllText(localizationFilePath, dataAsJson);
    }

    /// <summary>
    /// Load the localized text from the json
    /// </summary>
    /// <param name="lang">The language to load</param>
    public void loadLocalizedText()
    {
        if (localizationFilePath != "" && File.Exists(localizationFilePath))
        {
            string dataAsJson = File.ReadAllText(localizationFilePath);

            if (dataAsJson == "{}" || dataAsJson == "")
            {
                _localizationDatas.languages.Add(new Language("English", new List<LocalizationElement>() { new LocalizationElement("initKey", KeyTargetType.Text, "initText") }));
                saveLocalizedText();

                dataAsJson = File.ReadAllText(localizationFilePath);
            }

            _localizationDatas = JsonUtility.FromJson<LocalizationData>(dataAsJson);

        }
    }

    /// <summary>
    /// Unload localizatation
    /// </summary>
    public void unloadLocalizationData()
    {
        _localizationDatas = null;
    }
    #endregion

    #region getters Langs elements
    /// <summary>
    /// Get all the keys and text for a lang
    /// </summary>
    /// <param name="lang">The lang in which the text will be be return. \n Leave blank to use the base lang</param>
    private List<LocalizationElement> getLangElements(string lang = "")
    {
        string langToSearch = lang == "" ? _localizationDatas.languages[0].name : lang;

        List<LocalizationElement> localizationElements = _localizationDatas.languages.Find(x => x.name == langToSearch)?.elements;

        if (localizationElements == null)
            return new List<LocalizationElement>();

        return localizationElements;
    }

    /// <summary>
    /// get all the localization element organized by type
    /// </summary>
    /// <returns></returns>
    public List<List<LocalizationElement>> getElementsByType(string lang)
    {
        List<List<LocalizationElement>> elementByType = new List<List<LocalizationElement>>();

        List<string> types = getTypes();

        List<LocalizationElement> elements = getLangElements(lang);

        foreach (string type in types)
        {
            List<LocalizationElement> localizationElements = new List<LocalizationElement>();
            localizationElements.AddRange(elements.FindAll(x => x.targetType.ToString() == type));
            elementByType.Add(localizationElements);
        }

        return elementByType;
    }

    #endregion


    /// <summary>
    /// check if there is a language loaded
    /// </summary>
    /// <returns></returns>
    public bool hasLanguageDataLoaded()
    {
        return _localizationDatas != null && _localizationDatas.languages.Count != 0 ? true : false;
    }


    /// <summary>
    /// Return a list of all the key types
    /// </summary>
    /// <returns></returns>
    public List<String> getTypes()
    {
        List<string> types = new List<string>();
        List<KeyTargetType> targetTypes = Enum.GetValues(typeof(KeyTargetType)).Cast<KeyTargetType>().ToList();

        foreach (KeyTargetType type in targetTypes)
            types.Add(type.ToString());

        return types;
    }

    public Language getBaseLanguage() { return _localizationDatas.languages.Count > 0 ? _localizationDatas.languages[0] : null; }

}
