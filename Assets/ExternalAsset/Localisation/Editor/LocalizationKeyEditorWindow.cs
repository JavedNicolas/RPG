using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class LocalizationKeyEditorWindow : CustomEditorWindow
{
    // new key attributs
    string _newKey;
    KeyTargetType _targetType;
    string[] _newTexts;
    string[] _langs;

    // scroll view positions
    Vector2 _scrollPos;
    Vector2 _scrollPosForKeys;
    Vector2 _buttonScrollView;

    // displayer boolean
    bool _displayList = false;
    bool _displayNewKeyForm = false;
    public bool displayNewKeyForm
    {
        get => _displayNewKeyForm;
        set { _displayNewKeyForm = value; LocalizationTextHubEditorWindow.lockTab(_displayNewKeyForm); }
    }

    // size 
    float _leftSizeWidth = 200;
    float _keyAndTextHeight = 50;
    float _keyWidth = 150;

    // key editor
    bool _keyEditorMode = false;
    List<string> _newKeys;
    string[] _keys;

    public override void init()
    {
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical("Box", GUILayout.Width(_leftSizeWidth));
        _buttonScrollView = EditorGUILayout.BeginScrollView(_buttonScrollView, GUILayout.Width(_leftSizeWidth + 130));

        if (GUILayout.Button("List of the keys"))
        {
            _displayList = true;
            displayNewKeyForm = false;
        }
            
        if (GUILayout.Button("Add a key"))
        {
            _newKey = "";
            _newTexts = new string[Localization.instance.localizationDatas.languages.Count];
            _langs = new string[Localization.instance.localizationDatas.languages.Count];
            displayNewKeyForm = true;
            _displayList = false;
        }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();


        if (_displayList)
            displayKey();

        if (displayNewKeyForm)
            addKey();

        EditorGUILayout.EndHorizontal();
    }

    #region Edit Key
    void displayKey()
    {
        EditorGUILayout.BeginVertical();

        if (GUILayout.Button(_keyEditorMode ? "Validate Change" : "Edit Key Names", GUILayout.Width(150)))
        {
            if (_keyEditorMode)
                updateKeysName();
            else
                _newKeys = Localization.instance.getKeys().ToList();
            _keyEditorMode = !_keyEditorMode;
        }

        EditorGUILayout.BeginHorizontal();
        _keys = Localization.instance.getKeys();
        _scrollPosForKeys = EditorGUILayout.BeginScrollView(_scrollPosForKeys);

        // Editor mode
        if (_keyEditorMode)
        {
            _keys = Localization.instance.getKeys();
            for (int i = 0; i < _keys.Length; i++)
            {
                EditorGUILayout.BeginHorizontal("Box");
                GUILayout.FlexibleSpace();
                _newKeys[i] = EditorGUILayout.TextField(_newKeys[i], GUILayout.Width(350));
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
            }
        }
        // classic mode
        else
        {
            for (int i = 0; i < _keys.Length; i++)
            {
                EditorGUILayout.BeginHorizontal("Box");

                EditorGUILayout.LabelField(_keys[i], new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter });
                if (GUILayout.Button("Remove", GUILayout.Width(150)) && EditorUtility.DisplayDialog("Are you sure ?", "Do you want to delete the key : " + _keys[i] + " ?", "Yes", "No"))
                {
                    removeKey(_keys[i]);
                }
                EditorGUILayout.EndHorizontal();
            }
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndHorizontal();

        if (GUILayout.Button("Close"))
            _displayList = false;

        EditorGUILayout.EndVertical();
    }

    /// <summary>
    /// Update a key name
    /// </summary>
    void updateKeysName()
    {
        Localization.instance.updateKeysName(_newKeys, _keys);
        LocalizationTextHubEditorWindow.updateOtherWindows(eLocalizationWindowType.LangEditor);
    }

    void removeKey(string key)
    {
        Localization.instance.removeKey(key);
        LocalizationTextHubEditorWindow.updateOtherWindows(eLocalizationWindowType.LangEditor);
    }

    #endregion

    #region add Key
    void addKey()
    {
        EditorGUILayout.BeginVertical();

        for(int i = 0; i < _langs.Length; i++)
            _langs[i] = Localization.instance.localizationDatas.languages[i].name;

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal("Box");
        GUILayout.FlexibleSpace();
        _newKey = EditorGUILayout.TextField("New Key : ", _newKey, GUILayout.Width(350));
        _targetType = (KeyTargetType)EditorGUILayout.EnumPopup(_targetType);
        GUILayout.FlexibleSpace();
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Set a value for each language", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter });

        EditorGUILayout.Space();

        _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
        for (int i = 0; i < _newTexts.Length; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Value for " + _langs[i] + ": ", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter }, GUILayout.Height(_keyAndTextHeight), GUILayout.Width(_keyWidth));

            //Display text Area if category is text, display object field if not
            if (_targetType == KeyTargetType.Text)
                _newTexts[i] = EditorGUILayout.TextArea(_newTexts[i], GUILayout.Height(_keyAndTextHeight));
            else
            {
                Object obj = null;
                if (_newTexts[i] != "")
                    obj = AssetDatabase.LoadAssetAtPath<Object>(_newTexts[i]);
                EditorGUILayout.BeginVertical();
                _newTexts[i] = AssetDatabase.GetAssetPath(EditorGUILayout.ObjectField(obj, typeof(Object), true));
                EditorGUILayout.LabelField("Path : " + _newTexts[i]);
                EditorGUILayout.EndVertical();
            }
            
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Validate"))
        {
            if (!keyCreationCheck())
                return;

            saveNewKey();
            LocalizationTextHubEditorWindow.updateOtherWindows(eLocalizationWindowType.LangEditor);
        }
                

        if (GUILayout.Button("Cancel"))
        {
            displayNewKeyForm = false;
        }
            
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.EndVertical();
    }

    bool keyCreationCheck()
    {
        // If the key already exist throw an error
        if (Localization.instance.getBaseLanguage().elements.Exists(x => x.key == _newKey) && EditorUtility.DisplayDialog("Error", "The key already exist !", "Ok"))
        {
            EditorGUI.FocusTextInControl("");
            return false;
        }

        // If the form is not filled correctly send a error message
        if (_newKey == "" || _newKey == null || _newTexts.ToList().Exists(x => x == "") || _newTexts.ToList().Exists(x => x == null))
        {
            string message = "";
            message += _newKey == null || _newKey == "" ? "Please fill the key name.\n" : "";
            message += _newTexts.ToList().Exists(x => x == null) || _newTexts.ToList().Exists(x => x == "") ? "Please fill every key field." : "";
            EditorUtility.DisplayDialog("Error", message, "Ok");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Save a new Key for every language
    /// </summary>
    void saveNewKey()
    {
        Localization.instance.addKeyToLanguages(_newKey, _targetType, _newTexts, _langs);
        displayNewKeyForm = false;
        _newKey = "";
        EditorGUI.FocusTextInControl("");
    }
    #endregion

    /// <summary>
    /// Update the view to follow other tab change
    /// </summary>
    public override void update()
    {
        _displayList = false;

        _newTexts = new string[0];
        _langs = new string[0];
        _displayNewKeyForm = false;
     }
}