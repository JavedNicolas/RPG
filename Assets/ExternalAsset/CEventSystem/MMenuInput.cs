using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace MultipleMenus
{
    public struct MMenuInput 
    {
        public string nextMenu;
        public string previousMenu;
        public string horizontalAxis;
        public string verticalAxis;
        public string submit;
        public string cancel;
    }
}