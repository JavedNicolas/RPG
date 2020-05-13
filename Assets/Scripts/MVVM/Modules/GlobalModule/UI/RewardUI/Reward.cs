using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

namespace RPG.GlobalModule.View
{
    public class Reward : MonoBehaviour
    {
        [SerializeField] Image _iconImage;
        [SerializeField] TextMeshProUGUI _nameTMPro;
        [SerializeField] TextMeshProUGUI _descriptionTMPro;

        public bool isSelected { get; private set; }
        public bool isHovered { get; private set; }

        public virtual void init(string name, string description, Sprite icon)
        {
            _nameTMPro.text = name;
            _descriptionTMPro.text = description;
            _iconImage.sprite = icon;
        }

        public bool isEmpty() { return name == "" || name == "Empty"; }
    }
}