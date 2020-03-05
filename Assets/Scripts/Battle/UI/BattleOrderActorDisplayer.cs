using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class BattleOrderActorDisplayer : MonoBehaviour
{
    [SerializeField] Image _icon;
    [SerializeField] TextMeshProUGUI _text;

    public void set(Sprite icon, string name)
    {
        _icon.sprite = icon;
        _text.text = name;
    }
}
