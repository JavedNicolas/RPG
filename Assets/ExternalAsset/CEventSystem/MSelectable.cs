using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace MultipleMenus
{
    public class MSelectable : Selectable
    {
        public bool isSelected { get; private set; }
        private bool isHovered;

        private void Update()
        {
            setColor();
        }

        private void setColor()
        {
            if (isSelected)
                targetGraphic.color = colors.pressedColor;
            else if (isHovered)
                targetGraphic.color = colors.highlightedColor;
            else
                targetGraphic.color = colors.normalColor;
        }

        public void select(bool select)
        {
            isSelected = select;
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            isHovered = true;
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            isHovered = false;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            select(!isSelected);
        }
    }
}