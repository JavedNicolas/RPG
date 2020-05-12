using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace RPG.OpenModule.View
{
    public class MenuButton : Button, ICancelHandler
    {
        public Action<BaseEventData> onCancel;

        public void OnCancel(BaseEventData eventData)
        {
            onCancel(eventData);
        }
    }
}

