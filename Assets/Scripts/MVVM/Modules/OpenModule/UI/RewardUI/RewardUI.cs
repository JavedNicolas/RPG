using MultipleMenus;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.OpenModule.View
{
    public abstract class RewardUI : MonoBehaviour
    {
        [Tooltip("Display the top elements which are what thre player already have. \n" +
            "For exemple for a character reward if this toogle box if unchecked then the reward will not \n" +
            "display the player current team.\n" +
            "Be sure that the player elements have an empty slot before unchecking this !")]
        [SerializeField] bool _displayPlayerElements = true;
        [SerializeField] int _choicePerLine = 5;

        [Header("M Menus")]
        [SerializeField] bool _unlimitedChoices;
        [SerializeField] int _maxNumberOfChoices;
        [SerializeField] MMenu _playerElementMenu;
        [SerializeField] MMenu _rewardsElementMenu;

        [Header("Transforms")]
        [SerializeField] Transform _transformToHide;

        [Header("Prefabs")]
        [SerializeField] GameObject _choicePrefab;

        [Header("Button")]
        [SerializeField] Button _validateButton;

        #region delegate
        public delegate void ChoiceDone();
        public ChoiceDone isDone;
        #endregion

        private void Awake()
        {
            _validateButton.onClick.AddListener(choiceDone);

            // init the menus
            _playerElementMenu.setSelectionSettings(_unlimitedChoices, _maxNumberOfChoices, true);
            _rewardsElementMenu.setSelectionSettings(_unlimitedChoices, _maxNumberOfChoices, true);
        }

        public virtual void display(bool display)
        {
            _transformToHide.gameObject.SetActive(display);
        }

        /// <summary>
        /// init the choice menu
        /// </summary>
        /// <param name="rewards"></param>
        /// <param name="currentPlayerElements"></param>
        public void initRewards(List<(string name, string description, Sprite icon)> rewards, List<(string name, string description, Sprite icon)> currentPlayerElements)
        {
            initRewardParents();
            instantiateRewards(currentPlayerElements ,_playerElementMenu.transform);
            instantiateRewards(rewards, _rewardsElementMenu.transform);

            // set first selected player elements
            int emptySelectedElements = 0;
            _playerElementMenu.getElements().ForEach(x =>
            {
                if (emptySelectedElements >= _maxNumberOfChoices)
                {
                    return;
                }

                Reward reward =  x.GetComponent<Reward>();
                if (reward.isEmpty())
                {
                    x.select(true);
                    emptySelectedElements++;
                }
                    
            });
        }


        protected virtual void initRewardParents()
        {
            _playerElementMenu.transform.gameObject.SetActive(_displayPlayerElements);
            _playerElementMenu.transform.clearChild();
            _rewardsElementMenu.transform.clearChild();
        }

        /// <summary>
        /// Instantiate the choices prefabs
        /// </summary>
        /// <param name="choices">the list of choices available</param>
        /// <param name="choicesList">The list of choice to populate</param>
        /// <param name="parent">The parent where the instantiate elements will be put under</param>
        protected virtual void instantiateRewards(List<(string name, string description, Sprite icon)> choices, Transform parent)
        {
            resizeGridLayout(choices.Count, parent.GetComponent<ResizeGridLayoutGroup>());
            List<MSelectable> selectables = new List<MSelectable>();
            for (int i = 0; i < choices.Count; i++)
            {
                GameObject rewardGo = Instantiate(_choicePrefab, parent);
                Reward reward = rewardGo.GetComponent<Reward>();
                reward.init(choices[i].name, choices[i].description, choices[i].icon);
                selectables.Add(rewardGo.GetComponent<MSelectable>());
            }
        }

        protected virtual void resizeGridLayout(int numberOfElement, ResizeGridLayoutGroup gridLayout)
        {
            int choicePerLine = _choicePerLine > numberOfElement ? _choicePerLine : numberOfElement;
            gridLayout.updateSize(choicePerLine, (numberOfElement / choicePerLine) + 1);
        }

        /// <summary>
        /// Get the elements choosed by the player, which is a current player element which will be remplaced by by his reward, and his choosed reward
        /// </summary>
        /// <returns></returns>
        protected List<Choices> getSelectedRewards()
        {
            if (!_displayPlayerElements)
                _playerElementMenu.manualyCheckSelectedElement();

            List<Reward> choosedPlayerElements = getChoicesFromMenu(_playerElementMenu);
            List<Reward> choosedChoices = getChoicesFromMenu(_rewardsElementMenu);
            List<Choices> choosedElements = new List<Choices>();

            if(choosedPlayerElements.Count != choosedChoices.Count)
            {
                Debug.Log("Add Pop up warning");
                return null;
            }

            for (int i = 0; i < choosedPlayerElements.Count; i++)
            {
                choosedElements.Add(new Choices(choosedPlayerElements[i].name, choosedChoices[i].name));
            }

            return choosedElements;
        }

        /// <summary>
        /// Get selected choices elements from the MMenu
        /// </summary>
        /// <param name="menu">the menu to fetch the selected elements from</param>
        /// <returns></returns>
        private List<Reward> getChoicesFromMenu(MMenu menu)
        {
            List<Reward> choices = new List<Reward>();

            menu.getCurrentSelectedElement().ForEach(x =>
            {
                Reward choice = x.GetComponent<Reward>();
                
                if (choice != null)
                    choices.Add(choice);
            });

            return choices;
        }

        public abstract void choiceDone();

    }
}