using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEngine.EventSystems;

namespace RPG.Battle
{
    public class SelectorSetter : MonoBehaviour
    {
        [SerializeField] GameObject _arrow;
        [SerializeField] GameObject _modelGameObject;
        [SerializeField] float yOffset;
        EventSystem eventSystem;

        // Use this for initialization
        void Start()
        {
            setPosition();
            eventSystem = FindObjectOfType<EventSystem>();
            _arrow.SetActive(false);
        }

        [Button("Set")]
        public void setPosition()
        {
            float yMaxValue = _modelGameObject.GetComponent<CapsuleCollider>().bounds.max.y;

            _arrow.transform.localPosition = new Vector3(0, yMaxValue + yOffset, 0);
        }

        void Update()
        {
            if (eventSystem.currentSelectedGameObject == _modelGameObject)
                _arrow.gameObject.SetActive(true);
            else
                _arrow.gameObject.SetActive(false);
        }
    }
}

