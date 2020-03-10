using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;


namespace RPG.Battle.UI
{
    using RPG.Data;
    using RPG.UI;
    using UnityEngine.UI;

    public class BattleMenuSetter
    {
        List<Action> _actions;
        List<GameObject> _categoryPool;
        List<GameObject> _actionPool;

        public BattleMenuSetter(List<GameObject> categoryPool, List<GameObject> actionPool)
        {
            _categoryPool = categoryPool;
            _actionPool = actionPool;
        }

        public BattleActionMenuItems getCurrentMenu(List<Action> actions)
        {
            _actions = actions;

            _categoryPool.ForEach(x => x.gameObject.SetActive(false));
            _actionPool.ForEach(x => x.gameObject.SetActive(false));

            BattleActionMenuItems menuGO = new BattleActionMenuItems(); ;
            menuGO.categoryItems = instantiateMenu();

            return menuGO;
        }

        /// <summary> set the menu items from the pool and the available actions</summary>
        private List<ActionCategoryMenuItem> instantiateMenu()
        {
            // get the categories
            List<ActionCategoryMenuItem> categoryMenuItems = new List<ActionCategoryMenuItem>();
            List<string> categories = Enum.GetNames(typeof(ActionCategory)).ToList();
            // for each category generate a menu Item and a list of menu item for all the action in this category
            for (int i = 0; i < categories.Count; i++)
            {
                List<Action> matchingActions = _actions.FindAll(x => x.getCategory().ToString() == categories[i]);

                if (matchingActions == null || matchingActions.Count == 0)
                    continue;

                // get an unused gameobject from the pool
                GameObject gameObject = _categoryPool.Find(x => !x.gameObject.activeSelf);
                gameObject.GetComponentInChildren<LocalizeText>().key = "BattleMenu_" + categories[i];
                gameObject.SetActive(true);

                // generate a category Menu item
                ActionCategoryMenuItem categoryMenuItem = new ActionCategoryMenuItem()
                {
                    name = categories[i],
                    gameObject = gameObject,
                    button = gameObject.GetComponent<MenuButton>(),
                    element = instantiateActionMenu(matchingActions)
                };

                categoryMenuItems.Add(categoryMenuItem);
            }

            return categoryMenuItems;
        }

        /// <summary> set the sub menu with the actions</summary>
        /// <param name="matchingActions"></param>
        /// <returns></returns>
        private List<ActionMenuItem> instantiateActionMenu(List<Action> matchingActions)
        {

            List<ActionMenuItem> actionMenuItem = new List<ActionMenuItem>();
            foreach (Action action in matchingActions)
            {
                // get an unused object from the pool
                GameObject gameObject = _actionPool.Find(x => !x.gameObject.activeSelf);
                gameObject.GetComponentInChildren<LocalizeText>().key = action.getLocalisationKey();
                gameObject.SetActive(true);

                // generate a menu item
                ActionMenuItem menuItem = new ActionMenuItem()
                {
                    gameObject = gameObject,
                    element = action,
                    button = gameObject.GetComponent<MenuButton>()
                };

                // set the action menu gameobject (name and ap cost)
                menuItem.set();
                actionMenuItem.Add(menuItem);
            }

            return actionMenuItem;
        }

    }
}

