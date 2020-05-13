using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RPG.GlobalModule.View
{
    public interface INavigationSetter
    {
        void setNavigation(List<Button> selectables, EventSystem eventSystem);
    }
}