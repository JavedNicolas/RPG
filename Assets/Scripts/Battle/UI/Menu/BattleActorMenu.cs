using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace RPG.Battle.UI
{
    using RPG.UI;
    using RPG.Data;

    public class BattleActorMenu : Menu<Being>
    {
        // gameobject pool
        [SerializeField] List<BattleActorMenuItem> actorMenuItemGOs;
        GameObject _lastSelection;
        bool _isFocused = true;

        private void FixedUpdate()
        {
            updateActorLife();
        }

        private void updateActorLife()
        {
            elements?.ForEach(x => {
                BattleActorMenuItem menuItem = x.gameObject.GetComponent<BattleActorMenuItem>();
                menuItem.updateLife(x.element);
            });
        }

        public override void initMenu(List<Being> setterList)
        {
            elements = new List<MenuItem<Being>>();
            _lastSelection = null;

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

                // if the actor is dead disable his button
                if (elements[i].element.isDead())
                    elements[i].button.interactable = false;

                elements[i].gameObject.SetActive(true);
            }

            setNav();
        }

        private void setNav()
        {
            navigationSetter.setNavigation(elements.FindAll(x => x.button.interactable).Select(x => x.button as Button).ToList(), _eventSystem);

            elements.ForEach(x =>
            {
               x.button.onClick.AddListener(delegate
               {
                   _lastSelection = x.gameObject;
                   menuFinished(x.element);
               });
                x.button.onCancel = menuCanceled;
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
            _eventSystem.SetSelectedGameObject(_lastSelection);
        }

        public override void unFocusMenu()
        {
            _isFocused = false;
            _eventSystem.SetSelectedGameObject(null);
        }
    }
}

