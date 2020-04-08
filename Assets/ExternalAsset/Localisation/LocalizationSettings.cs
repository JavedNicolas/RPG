using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class LocalizationSettings
{
    // localization HUB attributs
    public const string LocalizationFileName = "Localization.json";
    private const string LocalizationFileFolder = "ExternalAsset/Localisation/";
    public static string LocalizationFilePath { get => Application.dataPath + "/" + LocalizationFileFolder + LocalizationFileName; }
}
