using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class NewLanguageEditorWindow : CustomEditorWindow
{
    bool addingLanguage = false;

    string newLang;
    LocalizationElement[] newElements;

    Vector2 scrollPos;

    float keyAndValueHeight = 50;
    float keyWidth = 300;

    public override void init()
    {
        if(newElements == null || newElements.Length != Localization.instance.getTextElementCount())
        {
            newElements = new LocalizationElement[Localization.instance.getTextElementCount()];
            for (int i = 0; i < newElements.Length; i++)
                newElements[i] = new LocalizationElement();
        }

        if(!addingLanguage)
            if (GUILayout.Button("Add a new Language"))
                addingLanguage = true;

        displayForm();
    }

    /// <summary>
    /// Display the language creation form
    /// </summary>
    private void displayForm()
    {
        if (!addingLanguage)
            return;

        LocalizationTextHubEditorWindow.lockTab(true);
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        newLang = EditorGUILayout.TextField("Language name : ", newLang);

        EditorGUILayout.LabelField("Please set those key's texts : ", new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter, fontStyle = FontStyle.Bold });
        EditorGUILayout.Space();

        Language baseLocalizationDatas = Localization.instance.getBaseLanguage();

        if(baseLocalizationDatas != null)
        {
            for (int i = 0; i < newElements.Length; i++)
            {
                string key = baseLocalizationDatas.elements[i].key;
                string originalLangText = baseLocalizationDatas.elements[i].value;

                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.BeginHorizontal("Box");
                // display the key with a tool showing the english value for this key
                EditorGUILayout.LabelField(
                    new GUIContent(key + " : \n (hover for the current english value)", "The English Value is : \n" + originalLangText),
                    new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.BoldAndItalic, alignment = TextAnchor.MiddleCenter },
                    GUILayout.Height(keyAndValueHeight),
                    GUILayout.Width(keyWidth));
                EditorGUILayout.EndHorizontal();

                // value
                newElements[i].value = EditorGUILayout.TextArea(newElements[i].value, GUILayout.Height(keyAndValueHeight));
                newElements[i].key = key;

                EditorGUILayout.EndHorizontal();
            }
        }

        EditorGUILayout.EndScrollView();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Validate"))
        {
            // check that the form is correctly filled
            if (newLang == null || newLang == "" || newElements.ToList().Exists(x => x.value == null) || newElements.ToList().Exists(x => x.value == ""))
            {
                string message = "";
                message += newLang == null || newLang == "" ? "Please fill the language name.\n" : "";
                message += newElements.ToList().Exists(x => x.value == null) || newElements.ToList().Exists(x => x.value == "") ? "Please fill every key field." : "";

                EditorUtility.DisplayDialog("Error", message, "Ok");
                return; 
            }

            createLang(newLang);
            addingLanguage = false;
            newLang = "";
            LocalizationTextHubEditorWindow.lockTab(false);
            LocalizationTextHubEditorWindow.updateOtherWindows(eLocalizationWindowType.KeyEditor);
            LocalizationTextHubEditorWindow.updateOtherWindows(eLocalizationWindowType.LangEditor);
        }

        if (GUILayout.Button("Cancel"))
        {
            addingLanguage = false;
            newElements = null;
            newLang = "";
            LocalizationTextHubEditorWindow.lockTab(false);
        }
        EditorGUILayout.EndHorizontal();
    }

    /// <summary>
    /// Create the language
    /// </summary>
    /// <param name="langName">The language name</param>
    void createLang(string langName)
    {
        Localization.instance.addLang(langName, newElements);

        newElements = new LocalizationElement[0];
        EditorGUI.FocusTextInControl("");
    }

    public override void update()
    {
        addingLanguage = false;
        newElements = null;
        newLang = "";
    }
}