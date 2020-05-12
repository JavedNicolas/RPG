using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RPG.DungeonModule.View
{
    public class RoomMapItem : MonoBehaviour
    {
        [Header("Color")]
        [SerializeField] Color _visitedColor;
        [SerializeField] Color _selectedColor;

        [Header("Components")]
        [SerializeField] Button _button;
        [SerializeField] Image _layoutImage;
        [SerializeField] Image _iconImage;

        bool _isCurrent = false;
        bool _iscleared = false;
        bool _isHided = false;

        public void Update()
        {
            setColor();
        }

        void setColor()
        {
            Color color;
            if (_isHided)
                color = new Color(0, 0, 0, 0);
            else if (_isCurrent)
                color = _selectedColor;
            else if (_iscleared)
                color = _visitedColor;
            else
                color = Color.white;

           _layoutImage.color = color;
           _iconImage.color = color;
        }

        public void isCurrentRoom(bool isCurrent)
        {
            this._isCurrent = isCurrent;
        }

        public void haveBeenCleared(bool cleared)
        {
            this._iscleared = cleared;
            this._isCurrent = false;
        }

        public void hide(bool display)
        {
            _isHided = display;
        }

        public void setLayoutImage(Sprite sprite)
        {
            _layoutImage.sprite = sprite;
        }

        public void setRoomImage(Sprite sprite)
        {
            _iconImage.sprite = sprite;
        }

        public void setButtonAction(UnityAction onclick)
        {
            _button.onClick.AddListener(onclick);
        }

    }
}

