using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;


namespace RPG.Battle
{
    using RPG.DataManagement;

    public class BattleMenuItemCreator
    {
        Character _character;

        GameObject _categoryPrefab;
        GameObject _actionPrefab;

        RectTransform _parent;

        public BattleMenuItems createMenu(Character character, GameObject categoryPrefab, GameObject actionPrefab, RectTransform parent)
        {
            _character = character;
            _categoryPrefab = categoryPrefab;
            _actionPrefab = actionPrefab;
            _parent = parent;

            BattleMenuItems menuGO = new BattleMenuItems(); ;
            menuGO.categoryItems = instantiateMenu();

            return menuGO;
        }

        /// <summary> Do not invoke this </summary>
        private List<ActionCategoryMenuItem> instantiateMenu()
        {
            List<ActionCategoryMenuItem> categoryMenuItems = new List<ActionCategoryMenuItem>();
            List<string> categories = Enum.GetNames(typeof(ActionCategory)).ToList();

            // for each category generate a menu Item and a list of menu item for all the action in this category
            for (int i = 0; i < categories.Count; i++)
            {
                List<Action> matchingActions = _character.actions.FindAll(x => x.getCategory().ToString() == categories[i]);

                if (matchingActions == null || matchingActions.Count == 0)
                    continue;

                GameObject gameObject = GameObject.Instantiate(_categoryPrefab, _parent);
                gameObject.GetComponentInChildren<LocalizeText>().key = "BattleMenu_" + categories[i];
                gameObject.SetActive(false);

                ActionCategoryMenuItem categoryMenuItem = new ActionCategoryMenuItem()
                {
                    name = categories[i],
                    isCategory = true,
                    gameObject = gameObject,
                    actionMenu = instantiateActionMenu(matchingActions)
                };

                categoryMenuItems.Add(categoryMenuItem);
            }

            return categoryMenuItems;
        }

        private List<ActionMenuItem> instantiateActionMenu(List<Action> matchingActions)
        {
            List<ActionMenuItem> actionMenuItem = new List<ActionMenuItem>();
            foreach (Action action in matchingActions)
            {
                GameObject gameObject = GameObject.Instantiate(_actionPrefab, _parent);
                gameObject.GetComponentInChildren<LocalizeText>().key = action.getLocalisationKey();
                gameObject.SetActive(false);

                ActionMenuItem menuItem = new ActionMenuItem()
                {
                    name = action.name,
                    gameObject = gameObject,
                    action = action
                };
                actionMenuItem.Add(menuItem);
            }

            return actionMenuItem;
        }

    }
}

