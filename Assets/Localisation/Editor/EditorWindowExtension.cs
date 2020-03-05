using UnityEngine;
using UnityEditor;

public enum eLocalizationError { FileNotFound, LangNotFound }

public static class EditorWindowExtension
{
    public static bool displayError(this EditorWindow window, eLocalizationError error)
    {
        string title = "";
        string message = "";
        string buttonText = "";
        switch (error)
        {
            case eLocalizationError.FileNotFound:
                title = "Localization Hub file not found";
                message = "The Localization Hub file could not be found.\n" +
                    "Please check that your file is in the correct folder. \n\n" +
                    "You can check or edit this path in the LocalizationFilesAttributs file.";
                buttonText = "Ok";
                break;
            case eLocalizationError.LangNotFound:
                break;
        }

        return EditorUtility.DisplayDialog(title, message, buttonText);
    }

}

public class CustomEditorWindow : EditorWindow
{
    public virtual void init(){}
    public virtual void update() { }
}
