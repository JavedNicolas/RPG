using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace RPG.UI
{
    public class VerticalNavigation : MonoBehaviour, INavigationSetter
    {
        [SerializeField] bool loop;

        public void setNavigation(List<Button> selectables, EventSystem eventSystem)
        {
            Button firstSelectable = null;
            Button lastCategory = null;
            
            if (selectables == null | selectables.Count == 0 || eventSystem == null)
                return;

            foreach (Button selectable in selectables)
            {
                if (firstSelectable == null)
                {
                    
                    if (eventSystem.firstSelectedGameObject == null)
                        eventSystem.firstSelectedGameObject = selectable.gameObject;
                    firstSelectable = selectable;
                    lastCategory = selectable;
                }
                else
                {
                    Button actionSelectable = selectable;

                    Navigation navigation = lastCategory.navigation;
                    navigation.selectOnDown = actionSelectable;
                    lastCategory.navigation = navigation;

                    navigation = actionSelectable.navigation;
                    navigation.selectOnUp = lastCategory;
                    actionSelectable.navigation = navigation;

                    lastCategory = actionSelectable;
                }
            }

            if (loop)
            {
                Navigation nav = firstSelectable.navigation;
                nav.selectOnUp = lastCategory;
                firstSelectable.navigation = nav;

                nav = lastCategory.navigation;
                nav.selectOnDown = firstSelectable;
                lastCategory.navigation = nav;
            }

            eventSystem.SetSelectedGameObject(firstSelectable.gameObject);
        }
    }
}
