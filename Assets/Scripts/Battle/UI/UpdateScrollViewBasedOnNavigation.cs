using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;

public class UpdateScrollViewBasedOnNavigation : MonoBehaviour
{
    [SerializeField] ScrollRect _scrollRect;
    [SerializeField] RectTransform _contentMaskTransfrom;
    [SerializeField] Scrollbar _scrollbar;
    [SerializeField] EventSystem _eventSystem;
    List<Button> _selectables;

    private void Start()
    {
        _selectables = _scrollRect.content.GetComponentsInChildren<Button>().ToList();
    }

    /// <summary>
    /// Set which rect transfrom the Scroll Rect script will use as content view
    /// </summary>
    /// <param name="transform"></param>
    public void setContent(RectTransform transform)
    {
        _scrollRect.content = transform;
        _selectables = _scrollRect.content.GetComponentsInChildren<Button>().ToList();

    }

    private void Update()
    {
        updateContentViewPosition();
    }

    private void updateContentViewPosition()
    {
        Button currentSelection = _eventSystem.currentSelectedGameObject?.GetComponent<Button>();
        Button match = _selectables.Find(x => x == currentSelection);
        if (match != null && currentSelection != null)
        {
            RectTransform matchTransfrom = match.GetComponent<RectTransform>();
            RectTransform contentTransfrom = _scrollRect.content.GetComponent<RectTransform>();

            float contentHeight = _scrollRect.content.GetComponent<RectTransform>().sizeDelta.y;
            float contentMaskHeight = _contentMaskTransfrom.rect.height;
            float contentPosY = contentTransfrom.localPosition.y;

            Vector3 matchPosition = -matchTransfrom.localPosition;
            float matchHeight = matchTransfrom.rect.height;

            // get the scroll bar down
            if (matchPosition.y + matchHeight > contentMaskHeight + contentPosY)
            {
                float scrollValue = -(matchPosition.y + matchHeight) / contentHeight + 1;
                _scrollbar.value = Mathf.Clamp(scrollValue, 0, 1);
            }
            // get the scroll bar up
            else if (matchPosition.y < contentPosY)
            {
                float scrollValue = -(matchPosition.y) / contentHeight + 1;
                _scrollbar.value = Mathf.Clamp(scrollValue, 0, 1);
            }
        }
    }
}
