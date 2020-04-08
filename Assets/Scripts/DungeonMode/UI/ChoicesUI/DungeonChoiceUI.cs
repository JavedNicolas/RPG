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

        [Header("Transforms")]
        [SerializeField] Transform _transformToHide;
        [SerializeField] Transform _playerElementHolder;
        [SerializeField] Transform _choiceHolder;

        [Header("Prefabs")]
        [SerializeField] GameObject _choicePrefab;

        [Header("Button")]
        [SerializeField] Button _validateButton;

        [SerializeField] protected List<Choice<T>> currentPlayerElements;
        [SerializeField] protected List<Choice<T>> choices;

        protected Choice<T> currentSelectedPlayerElement = null;
        protected Choice<T> currentSelectedChoice = null;

        public delegate void ChoiceDone();
        public ChoiceDone choosed;

        private void Start()
        {
            _validateButton.onClick.AddListener(choiceDone);
            currentPlayerElements = new List<Choice<T>>();
            choices = new List<Choice<T>>();
        }

        private void Update()
        {
            checkSelected(currentPlayerElements, ref currentSelectedPlayerElement);
            checkSelected(choices, ref currentSelectedChoice);
        }

        private void checkSelected(List<Choice<T>> elements, ref Choice<T> refCurrentSelectedElement)
        {
            if (refCurrentSelectedElement == null && elements.Count > 0)
            {
                refCurrentSelectedElement = elements.First();
                refCurrentSelectedElement.select(true);
                return;
            }

            Choice<T> currentSelectedElement = refCurrentSelectedElement;
            List<Choice<T>> selectedElements = elements.FindAll(x => x.isSelected && x != currentSelectedElement);

            if (selectedElements.Count >= 1)
            {
                Choice<T> newSelection = selectedElements.First();
                refCurrentSelectedElement.select(false);
                refCurrentSelectedElement = newSelection;
            }
        }

        public virtual void display(bool display)
        {
            _transformToHide.gameObject.SetActive(display);
        }

        public void initChoices(List<T> choices, List<T> currentPlayerElements)
        {
            setChoices(currentPlayerElements, this.currentPlayerElements ,_playerElementHolder);
            setChoices(choices, this.choices, _choiceHolder);
        }

        protected virtual void setChoices(List<T> choices, List<Choice<T>> choicesList, Transform parent)
        {
            resizeGridLayout(choices.Count, parent.GetComponent<ResizeGridLayoutGroup>());

            for (int i = 0; i < choices.Count; i++)
            {
                GameObject choiceGO = Instantiate(_choicePrefab, parent);
                Choice<T> choice = choiceGO.GetComponent<Choice<T>>();
                choice.init(choices[i]);
                choicesList.Add(choice);
            }
        }

        protected virtual void resizeGridLayout(int numberOfElement, ResizeGridLayoutGroup gridLayout)
        {
            int choicePerLine = _choicePerLine > numberOfElement ? _choicePerLine : numberOfElement;
            gridLayout.updateSize(choicePerLine, (numberOfElement / choicePerLine) + 1);
        }

        public abstract void choiceDone();

    }
}