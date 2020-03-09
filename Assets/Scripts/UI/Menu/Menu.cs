using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Linq;

namespace RPG.UI
{
    [RequireComponent(typeof(INavigationSetter))]
    public abstract class Menu<T> : MonoBehaviour
    {
        #region delegates
        public Action<T> menuFinished;
        public Action<BaseEventData> menuCanceled;
        #endregion

        protected INavigationSetter navigationSetter;
        protected EventSystem _eventSystem;
        protected List<MenuItem<T>> elements;

        public abstract void initMenu(List<T> setterList);
        public abstract void focusMenu();
        public abstract void unFocusMenu();

        protected void Awake()
        {
            navigationSetter = GetComponent<INavigationSetter>();
        }

        protected void Update()
        {
            updateSelectionWhenLost();
        }

        protected virtual void updateSelectionWhenLost()
        {
            if (elements != null && elements.Count != 0 && _eventSystem.currentSelectedGameObject == null)
                _eventSystem.SetSelectedGameObject(elements.First().gameObject);
        }


        /// <summary> set the menu event system used for navigation </summary>
        /// <param name="eventSystem"></param>
        public void setEventSystem(EventSystem eventSystem)
        {
            _eventSystem = eventSystem;
        }
    }
}

