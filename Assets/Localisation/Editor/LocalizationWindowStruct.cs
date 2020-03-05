using UnityEditor;

public enum eLocalizationWindowType { LangEditor, KeyEditor, AddLang }

public struct LocalizationWindow
{
    public eLocalizationWindowType type;
    public string name;
    public CustomEditorWindow window;

    public LocalizationWindow(eLocalizationWindowType type, string name,  CustomEditorWindow window)
    {
        this.type = type;
        this.name = name;
        this.window = window;
    }
}