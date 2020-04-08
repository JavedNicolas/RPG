using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using RPG.Data;
using UnityEngine.EventSystems;

namespace RPG.DungeonMode.UI
{
    public abstract class Choice<T> : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler where T : DatabaseElement
    {
        public T element { get; private set; }
        [SerializeField] Image backgroundImage;
        [SerializeField] Image _iconImage;
        [SerializeField] TextMeshProUGUI _nameTMPro;
        [SerializeField] TextMeshProUGUI _descriptionTMPro;
        [SerializeField] Color selectedColor;
        [SerializeField] Color hoverColor;
        [SerializeField] Color baseColor;

        public bool isSelected { get; private set; }
        public bool isHovered { get; private set; }

        public virtual void init(T element)
        {
            this.element = element;
            _iconImage.sprite = element.icon;
            _nameTMPro.text = element.name;
            _descriptionTMPro.text = element.description;
        }

        private void Update()
        {
            setColor();
        }

        private void setColor()
        {
            if (isSelected)
                backgroundImage.color = selectedColor;
            else if(isHovered)
                backgroundImage.color = hoverColor;
            else
                backgroundImage.color = baseColor;
        }

        public void select(bool select)
        {
            isSelected = select;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isHovered = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isHovered = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            select(!isSelected);
        }
    }
}