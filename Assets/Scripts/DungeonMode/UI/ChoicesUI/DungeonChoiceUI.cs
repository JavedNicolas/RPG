using MultipleMenus;
using RPG.Data;
using RPG.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.DungeonMode.UI
{
    public abstract class DungeonChoiceUI<T> : MonoBehaviour where T : DatabaseElement
    {
        [SerializeField] int _choicePerLine = 5;

        [Header("M Menus")]
        [SerializeField] bool _unlimitedChoices;
        [SerializeField] int _maxNumberOfChoices;
        [SerializeField] MMenu _playerElementMenu;
        [SerializeField] MMenu _choiceElementMenu;

        [Header("Transforms")]
        [SerializeField] Transform _transformToHide;
        [SerializeField] Transform _playerElementHolder;
        [SerializeField] Transform _choiceHolder;

        [Header("Prefabs")]
        [SerializeField] GameObject _choicePrefab;

        [Header("Button")]
        [SerializeField] Button _validateButton;

        public delegate void ChoiceDone();
        public ChoiceDone choosed;

        private void Start()
        {
            _validateButton.onClick.AddListener(choiceDone);

            // init the menus
            _playerElementMenu.setSelectionSettings(unlimitedChoices: _unlimitedChoices, maxNumberOfSelection: _maxNumberOfChoices);
            _choiceElementMenu.setSelectionSettings(unlimitedChoices: _unlimitedChoices, maxNumberOfSelection: _maxNumberOfChoices);
        }

        public virtual void display(bool display)
        {
            _transformToHide.gameObject.SetActive(display);
        }

        /// <summary>
        /// init the choice menu
        /// </summary>
        /// <param name="choices"></param>
        /// <param name="currentPlayerElements"></param>
        public void initChoices(List<T> choices, List<T> currentPlayerElements)
        {
            InstantiateChoices(currentPlayerElements ,_playerElementHolder, _playerElementMenu);
            InstantiateChoices(choices, _choiceHolder, _choiceElementMenu);
        }

        /// <summary>
        /// Instantiate the choices prefabs
        /// </summary>
        /// <param name="choices">the list of choices available</param>
        /// <param name="choicesList">The list of choice to populate</param>
        /// <param name="parent">The parent where the instantiate elements will be put under</param>
        protected virtual void InstantiateChoices(List<T> choices, Transform parent, MMenu menu)
        {
            resizeGridLayout(choices.Count, parent.GetComponent<ResizeGridLayoutGroup>());
            List<MSelectable> selectables = new List<MSelectable>();
            for (int i = 0; i < choices.Count; i++)
            {
                GameObject choiceGO = Instantiate(_choicePrefab, parent);
                Choice<T> choice = choiceGO.GetComponent<Choice<T>>();
                choice.init(choices[i]);
                selectables.Add(choiceGO.GetComponent<MSelectable>());
            }

            menu.setElements(selectables);
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
        protected List<ChoiceElements<T>> getSelectedChoices()
        {
            List<Choice<T>> choosedPlayerElements = getChoicesFromMenu(_playerElementMenu);
            List<Choice<T>> choosedChoices = getChoicesFromMenu(_choiceElementMenu);
            List<ChoiceElements<T>> choosedElements = new List<ChoiceElements<T>>();

            if(choosedPlayerElements.Count != choosedChoices.Count)
            {
                Debug.Log("Add Pop up warning");
                return null;
            }

            for (int i = 0; i < choosedPlayerElements.Count; i++)
            {
                choosedElements.Add(new ChoiceElements<T>(choosedPlayerElements[i], choosedChoices[i]));
            }

            return choosedElements;
        }

        /// <summary>
        /// Get selected choices elements from the MMenu
        /// </summary>
        /// <param name="menu">the menu to fetch the selected elements from</param>
        /// <returns></returns>
        private List<Choice<T>> getChoicesFromMenu(MMenu menu)
        {
            List<Choice<T>> choices = new List<Choice<T>>();

            menu.getCurrentSelectedElement().ForEach(x =>
            {
                Choice<T> choice = x.GetComponent<Choice<T>>();
                if (choice != null)
                    choices.Add(choice);
            });

            return choices;
        }

        public abstract void choiceDone();

    }
}