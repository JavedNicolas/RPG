using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class LanguagesEditorWindow : CustomEditorWindow {

    Vector2 _leftScrollPos;
    Vector2 _rightScrollPos;

    float _leftSizeWidth = 200;
    float _keyAndTextHeight = 50;

    // keys and categories
    bool[] _displayTargetTypeCategory;
    List<string> _targetTypes = new List<string>();
    List<List<LocalizationElement>> _localizationTextOrdererByType;

    // lang
    string _langBeingEdited = "";

    //search bar
    string _searchText = "";

    public override void init()
    {
        EditorGUILayout.BeginHorizontal();
        displayLanguage();
        displayLanguageFields();
        EditorGUILayout.EndHorizontal();
    }

    #region Left Panel
    /// <summary>
    /// display the list of the current available languages
    /// </summary>
    void displayLanguage()
    {
        EditorGUILayout.BeginVertical("Box", GUILayout.Width(_leftSizeWidth));

        EditorGUILayout.LabelField("Languages : ", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter });
        EditorGUILayout.Space();

        _leftScrollPos = EditorGUILayout.BeginScrollView(_leftScrollPos, GUILayout.Width(_leftSizeWidth + 130));

        for (int i = 0; i < Localization.instance.localizationDatas.languages.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(Localization.instance.localizationDatas.languages[i].name);
            if (GUILayout.Button("Edit"))
            {
                _langBeingEdited = Localization.instance.localizationDatas.languages[i].name;
                loadLang();
            }

            if (GUILayout.Button("Remove"))
            {
                removeLang(i);
            }
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();

    }

    #endregion

    #region Right Panel
    /// <summary>
    /// Display the form to edit a language
    /// </summary>
    void displayLanguageFields()
    {
        EditorGUILayout.BeginVertical();
        _rightScrollPos = EditorGUILayout.BeginScrollView(_rightScrollPos);

        if (!Localization.instance.hasLanguageDataLoaded() || _langBeingEdited == "" || _targetTypes.Count == 0)
        {
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndScrollView();
            return;
        }

        if (!File.Exists(LocalizationSettings.LocalizationFilePath))
        {
            EditorGUILayout.LabelField("No JSON file in the Localized Text  ", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter });
        }

        
        EditorGUILayout.LabelField("List of the " + _langBeingEdited  + " language text : ", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter });
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        if (GUILayout.Button(_displayTargetTypeCategory.Contains(false) ? "Display All" : "Hide All", GUILayout.Width(150)))
        {
            showCategories(_displayTargetTypeCategory.Contains(false));
        }

        _searchText = EditorGUILayout.TextField("Search : ", _searchText, GUILayout.Width(350));
        EditorGUILayout.Space();
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        if(_searchText != "")
        {
            List<LocalizationElement> elementsFromSearch = _localizationTextOrdererByType.SelectMany(x => x).ToList().FindAll(x => x.value.Contains(_searchText) || x.key.Contains(_searchText));

            for(int i = 0; i < elementsFromSearch.Count; i++)
            {
                displayElement(i, elementsFromSearch);
            }
        }
        else
        {
            // display text order by key type
            for (int j = 0; j < _targetTypes.Count; j++)
            {
                List<LocalizationElement> itemsForTargetType = _localizationTextOrdererByType[j];

                EditorGUILayout.BeginHorizontal("Box");
                EditorGUILayout.LabelField(_targetTypes[j] + " : ", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter });

                // hide or display the category
                if (GUILayout.Button(_displayTargetTypeCategory[j] ? "Hide" : "Display"))
                    _displayTargetTypeCategory[j] = !_displayTargetTypeCategory[j];
                EditorGUILayout.EndHorizontal();

                if (_displayTargetTypeCategory[j])
                    for (int i = 0; i < itemsForTargetType.Count; i++)
                    {
                        displayElement(i, itemsForTargetType);
                    }
            }
        }

        EditorGUILayout.EndScrollView();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Update"))
        {
            saveLang(_localizationTextOrdererByType);
            loadLang();
            _searchText = "";
            EditorGUI.FocusTextInControl("");
        }
        if (GUILayout.Button("Close"))
        {
            _langBeingEdited = "";
            _searchText = "";
            EditorGUI.FocusTextInControl("");
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        EditorGUILayout.EndVertical();
    }

    void showCategories(bool show)
    {
        for (int i = 0; i < _displayTargetTypeCategory.Length; i++)
        {
            _displayTargetTypeCategory[i] = show;
        }
    }

    void displayElement(int index, List<LocalizationElement> elements)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(elements[index].key, GUILayout.Height(_keyAndTextHeight), GUILayout.Width(200));

        //Display text Area if category is text, display object field if not
        if(elements[index].targetType == KeyTargetType.Text)
            elements[index].value = EditorGUILayout.TextArea(elements[index].value, GUILayout.Height(_keyAndTextHeight));
        else
        {
            displayObjectField(index, elements);
        }

        elements[index].targetType = (KeyTargetType)EditorGUILayout.EnumPopup(elements[index].targetType, GUILayout.Width(150));
        EditorGUILayout.EndHorizontal();
    }

    /// <summary>
    /// Display the object field based on the key target type
    /// </summary>
    /// <param name="index">elements index</param>
    /// <param name="elements">the elements</param>
    void displayObjectField(int index, List<LocalizationElement> elements)
    {
        switch (elements[index].targetType)
        {
            case KeyTargetType.AudioClip:
                AudioClip audioClip = null;
                if (elements[index].value != "")
                    audioClip = AssetDatabase.LoadAssetAtPath<AudioClip>(elements[index].value);
                elements[index].value = AssetDatabase.GetAssetPath(EditorGUILayout.ObjectField(audioClip, typeof(AudioClip), false));
                break;
            case KeyTargetType.Sprite:
                Sprite sprite = null;
                if (elements[index].value != "")
                    sprite = AssetDatabase.LoadAssetAtPath<Sprite>(elements[index].value);
                elements[index].value = AssetDatabase.GetAssetPath(EditorGUILayout.ObjectField(sprite, typeof(Sprite), false));
                break;
        }

        
    }
    #endregion

    #region support function
    public override void update()
    {
        _langBeingEdited = "";
        _searchText = "";
    }

    void loadLang()
    {
        _localizationTextOrdererByType = Localization.instance.getElementsByType(_langBeingEdited);
        _targetTypes = Localization.instance.getTypes();
        _displayTargetTypeCategory = new bool[_targetTypes.Count];
    }

    void saveLang(List<List<LocalizationElement>> localizationElements)
    {
        List<LocalizationElement> elements = localizationElements.SelectMany(x => x).ToList();
        Localization.instance.updateLang(_langBeingEdited, elements);
    }

    /// <summary>
    /// remove a language from the localization
    /// </summary>
    /// <param name="index">the language index</param>
    void removeLang(int index)
    {
        if (Localization.instance.localizationDatas.languages.Count == 1)
            if (EditorUtility.DisplayDialog("Error", "You can't delete this language for it's the only one", "Ok"))
                return;

        if (EditorUtility.DisplayDialog("Are you sure ?", "Do you want to delete " + Localization.instance.localizationDatas.languages[index].name + " language ?", "Yes", "No"))
        {
            Localization.instance.removeLang(Localization.instance.localizationDatas.languages[index].name);
        }
    }
    #endregion

}