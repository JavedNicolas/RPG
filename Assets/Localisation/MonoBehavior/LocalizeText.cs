using UnityEngine;
using TMPro;

public enum UITextType { TMPro, TextMesh }

public class LocalizeText: LocalizeComponent
{
    [SerializeField] UITextType textMeshType;
    public override KeyTargetType targetType => KeyTargetType.Text;

    // TexwtMesh
    TextMeshProUGUI _textMeshProUGUI;
    TextMesh _textMesh;

    /// <summary>
    /// Get the text mesh object
    /// </summary>
    public override void getComponentToModify()
    {
        switch (textMeshType)
        {
            case UITextType.TMPro: _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
                break;
            case UITextType.TextMesh: _textMesh = GetComponent<TextMesh>();
                break;
        }
    }

    public override void setBasedOnLocation()
    {
        string text = Localization.instance.getTextForKey(key);

        switch (textMeshType)
        {
            case UITextType.TMPro:
                _textMeshProUGUI.text = text;
                break;
            case UITextType.TextMesh:
                _textMesh.text = text;
                break;
        }
    }
}
