using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RPG.Battle
{
    using RPG.UI;

    public class BattleTargetSelector : Menu<BattleTarget>
    {
        List<BattleTarget> _targets;

        public override void initMenu(List<BattleTarget> setterList)
        {
            _targets = setterList;
           
            setterList.ForEach(x => {
                x.button.onClick.RemoveAllListeners();
                x.button.onClick.AddListener(delegate
                {
                    menuFinished(x);
                });
                x.button.onCancel = menuCanceled;
            });

            navigationSetter.setNavigation(setterList.Select(x => x.button as Button).ToList(), _eventSystem);
        }

        protected override void updateSelectionWhenLost()
        {
            if (_eventSystem != null && _targets != null && _targets.Count != 0 && _eventSystem.currentSelectedGameObject == null)
                _eventSystem.SetSelectedGameObject(_targets.First().button.gameObject);
        }

        public override void focusMenu()
        {
            if (_eventSystem == null || _targets == null || _targets.Count == 0)
                return;

            gameObject.SetActive(true);
            _eventSystem.SetSelectedGameObject(_targets.First().model);
        }

        public override void unFocusMenu()
        {
            gameObject.SetActive(false);
        }
    }

}
