using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class LocalizationTextHubEditorWindow : EditorWindow
{
    static LocalizationTextHubEditorWindow window;

    // Tabs
    static LocalizationWindow[] localizationWindows;

    static int currentTab;
    static bool tabIsLock;

    [UnityEditor.MenuItem("Localization/Edit Text")]
    public static void showWindow()
    {
        window = new LocalizationTextHubEditorWindow();

        localizationWindows = new LocalizationWindow[3];

        localizationWindows[0] = new LocalizationWindow(eLocalizationWindowType.LangEditor, "Edit Languages", new LanguagesEditorWindow());
        localizationWindows[1] = new LocalizationWindow(eLocalizationWindowType.KeyEditor, "Add / Remove Keys", new LocalizationKeyEditorWindow());
        localizationWindows[2] = new LocalizationWindow(eLocalizationWindowType.AddLang, "Add language", new NewLanguageEditorWindow());

        window.minSize = new Vector2(700, 500);
        window.Show();
    }

    #region localization menu items
    [UnityEditor.MenuItem("Localization/Edit languages")]
    static void showAbilityDB()
    {
        showWindow();
        currentTab = 0;
    }

    [UnityEditor.MenuItem("Localization/Localizaion Keys")]
    static void showEnemyDB()
    {
        showWindow();
        currentTab = 1;
    }

    [UnityEditor.MenuItem("Localization/Add language")]
    static void showItemDB()
    {
        showWindow();
        currentTab = 2;
    }
    #endregion

    private void OnGUI()
    {
        // Il the localization file do not exist throw an error
        if (!File.Exists(Localization.instance.localizationFilePath))
        {
            if (this.displayError(eLocalizationError.FileNotFound))
                this.Close();
            return;
        }

        if (tabIsLock)
        {
            string[] tabNames = new string[localizationWindows.Length];

            for (int i = 0; i < localizationWindows.Length; i++)
                tabNames[i] = localizationWindows[i].name;

            int tabClicked = GUILayout.Toolbar(currentTab, tabNames);
            if(tabClicked != currentTab)
                EditorUtility.DisplayDialog("Tab Locked", "Please finish what you are doing before switching tab", "Ok");
        }
            
        else
            currentTab = GUILayout.Toolbar(currentTab, new string[] { "Edit Languages", "Add / Remove Keys", "Add languages" });

        EditorGUILayout.Space();

        localizationWindows[currentTab].window.init();
    }

    public static void updateOtherWindows(eLocalizationWindowType type)
    {
        int windowsToReset = localizationWindows.ToList().FindIndex(x => x.type == type);

        if (windowsToReset == -1)
            return;

        localizationWindows[windowsToReset].window.update();
    }

    public static void lockTab(bool state)
    {
        tabIsLock = state;
    }
}

