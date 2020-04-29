using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using RPG.Data;
using UnityEngine.EventSystems;

namespace RPG.DungeonMode.UI
{
    public abstract class Reward<T> : MonoBehaviour where T : DatabaseElement
    {
        public T element { get; private set; }
        [SerializeField] Image _iconImage;
        [SerializeField] TextMeshProUGUI _nameTMPro;
        [SerializeField] TextMeshProUGUI _descriptionTMPro;

        public bool isSelected { get; private set; }
        public bool isHovered { get; private set; }

        public virtual void init(T element)
        {
            this.element = element;
            _iconImage.sprite = element.icon;
            _nameTMPro.text = element.name;
            _descriptionTMPro.text = element.description;
        }
    }
}