using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace RPG.Battle.UI
{
    using RPG.UI;
    using RPG.DataManagement;

    public class BattleActorMenu : Menu<Being>
    {
        // gameobject pool
        [SerializeField] List<BattleActorMenuItem> actorMenuItemGOs;
        bool _isFocused = true;

        private void FixedUpdate()
        {
            updateActorLife();
        }

        private void updateActorLife()
        {
            if (elements != null)
                elements.ForEach(x => {
                    if (x.element != null)
                    {
                        x.gameObject.GetComponent<BattleActorMenuItem>().lifeValue.text = x.element.currentLife.ToString();
                    }
                });
        }

        public override void initMenu(List<Being> setterList)
        {
            elements = new List<MenuItem<Being>>();

            for (int i = 0; i < actorMenuItemGOs.Count; i++)
            {
                if(i >= setterList.Count())
                {
                    actorMenuItemGOs[i].gameObject.SetActive(false);
                    continue;
                }

                MenuItem<Being> menuItem = new MenuItem<Being>()
                {
                    element = setterList[i],
                    gameObject = actorMenuItemGOs[i].gameObject,
                    button = actorMenuItemGOs[i].GetComponent<MenuButton>()
                };

                menuItem.gameObject.GetComponent<BattleActorMenuItem>().actorName.text = menuItem.element.name;

                elements.Add(menuItem);
                elements[i].gameObject.SetActive(true);
            }

            setNav();
        }

        private void setNav()
        {
            navigationSetter.setNavigation(elements.Select(x => x.button as Button).ToList(), _eventSystem);

            elements.ForEach(x =>
            {
               x.button.onClick.AddListener(delegate
               {
                   menuFinished(x.element);
               });
            });
        }

        protected override void updateSelectionWhenLost()
        {
            if(_isFocused)
                base.updateSelectionWhenLost();
        }


        public override void focusMenu()
        {
            if (_eventSystem == null || elements == null || elements.Count == 0)
                return;

            _isFocused = true;
            gameObject.SetActive(true);
            _eventSystem.SetSelectedGameObject(elements.First().gameObject);
        }

        public override void unFocusMenu()
        {
            _isFocused = false;
        }
    }
}

