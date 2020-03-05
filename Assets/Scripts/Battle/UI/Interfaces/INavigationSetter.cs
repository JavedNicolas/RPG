using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RPG.Battle
{
    internal interface INavigationSetter
    {
        void setNavigation(List<Button> selectables, EventSystem eventSystem);
    }
}