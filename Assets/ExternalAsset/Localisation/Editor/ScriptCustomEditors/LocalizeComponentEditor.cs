using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(LocalizeComponent), true)]
public class LocalizeComponentEditor : Editor
{
    LocalizeComponent localizeComponent;
    string key;

    private void OnEnable()
    {
        localizeComponent = (LocalizeComponent)target;
        key = localizeComponent.key;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical("Box");
        EditorGUILayout.LabelField("Settings", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter });
        localizeComponent.executeOnUpdate = EditorGUILayout.Toggle("Execute On Update", localizeComponent.executeOnUpdate);
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Localization :", new GUIStyle(GUI.skin.label) { fontStyle = FontStyle.Bold, alignment = TextAnchor.MiddleCenter, border = new RectOffset(0, 0, 0, 1) });
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        key = EditorGUILayout.TextField("Key :", key);
        if (GUILayout.Button("Re-assign", GUILayout.Width(100)))
        {
            localizeComponent.setBasedOnLocation();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        if (key == "" || localizeComponent.key == key)
            return;

        string[] keys = Localization.instance.getKeys(localizeComponent.targetType);
        string[] matchingKey = keys.ToList().FindAll(x => x.ToLower().Contains(key.ToLower())).ToArray();

        for (int i = 0; i < matchingKey.Length; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(matchingKey[i]);
            if (GUILayout.Button("Choose"))
            {
                localizeComponent.key = matchingKey[i];
                key = matchingKey[i];
                localizeComponent.getComponentToModify();
                localizeComponent.setBasedOnLocation();
                EditorGUI.FocusTextInControl("");
            }
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space();
    }
}