using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

internal interface INavigationSetter
{
    void setNavigation(List<Button> selectables, EventSystem eventSystem);
}