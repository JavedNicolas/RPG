using UnityEngine;
using System.Collections.Generic;


namespace RPG.DungeonModule.View
{
    using MultipleMenus;
    using RPG.GlobalModule.View;

    public class TabMenu : MonoBehaviour
    {
        [SerializeField] List<TabMenuButton> _menuButtons;
        [SerializeField] RewardUI _rewardMenus;
        [SerializeField] TabMenuButton _rewardMenuButton;
        [SerializeField] MMenu _mMenu;

        public RewardUI rewardMenus => _rewardMenus;

        bool _isChoiceDisplayed = false;

        private void Start()
        {
            // activate first button
            _mMenu.setSelectionSettings();
            List<MSelectable> selectables = new List<MSelectable>();
            _menuButtons.ForEach(x => selectables.Add(x.gameObject.GetComponent<MSelectable>()));
            _mMenu.setElements(selectables);
        }

        private void Update()
        {
            if(_mMenu.getCurrentSelectedElement() != null && _mMenu.getCurrentSelectedElement().Count != 0)
            {
                TabMenuButton tabMenuButtonSelected = _mMenu.getCurrentSelectedElement()[0].GetComponent<TabMenuButton>();
                displayTab(tabMenuButtonSelected.menuToDisplay, tabMenuButtonSelected);
            }
        }

        public void display(bool display, bool hasReward = false)
        {
            displayTabReward(hasReward);
            gameObject.SetActive(display);
        }

        private void displayTabReward(bool displayReward)
        {
            _isChoiceDisplayed = displayReward;
            _rewardMenuButton.gameObject.SetActive(displayReward);
            _rewardMenuButton.GetComponent<MSelectable>().select(true);
        }

        private void displayTab(GameObject menuToDisplay, TabMenuButton menuButton)
        {
            _menuButtons.ForEach(x => {
                if (x.menuToDisplay == menuToDisplay)
                    x.menuToDisplay?.SetActive(true);
                else
                    x.menuToDisplay?.SetActive(false);
            });
            
        }
        
    }
}