using UnityEngine;
using UnityEngine.UI;

public class ActionPointItem : MonoBehaviour
{
    [SerializeField] Image _filledContent;
    public bool isFilled => _filledContent.fillAmount == 1 ? true : false;

    public void setFilled(bool filled)
    {
        _filledContent.fillAmount = filled ? 1f : 0f;
    }
}