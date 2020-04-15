using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MultipleMenus
{
    public class MMenu : MonoBehaviour
    {
        [SerializeField] NavigationType _navigationType;
        [SerializeField] bool _unlimitedChoices;
        [SerializeField] int _maxNumberOfSelection;
        [SerializeField] List<MSelectable> _selectables = new List<MSelectable>();
        List<MSelectable> _currentSelectedElements = new List<MSelectable>();

        private void Start()
        {
            
        }

        public void setElements(List<MSelectable> selectables)
        {
            _selectables = selectables;
        }

        private void Update()
        {
            checkSelected();
        }

        private void checkSelected()
        {
            if (_unlimitedChoices)
            {
                _currentSelectedElements = _selectables.FindAll(x => x.isSelected);
                return;
            }

            // if there is no current selected element or one the element in the list is null then reset the current selected elements
            if (_currentSelectedElements != null && (_currentSelectedElements.Count == 0 || _currentSelectedElements.Exists(x => x == null)) && _selectables.Count > 0)
            {
                _currentSelectedElements = new List<MSelectable>();
                _currentSelectedElements.Add(_selectables.First());
                _currentSelectedElements.ForEach(x => x.select(true));
                return;
            }

            // find selected elements
            List<MSelectable> selectedElements = _selectables.FindAll(x => x.isSelected);

            // remove not selected elements which where selected
            if (selectedElements.Count < _currentSelectedElements.Count && _currentSelectedElements.Count > 0)
            {
                for (int i = 0; i < _currentSelectedElements.Count; i++)
                {
                    if (!selectedElements.Contains(_currentSelectedElements[i]))
                        _currentSelectedElements.Remove(_currentSelectedElements[i]);
                }

                return;
            }

            // find selected elements which are not in the current selected List
            selectedElements = selectedElements.FindAll(x => !_currentSelectedElements.Contains(x));

            int numberOfItemSelected = selectedElements.Count + _currentSelectedElements.Count;
            if (numberOfItemSelected > _maxNumberOfSelection && selectedElements.Count > 0)
            {
                Debug.Log("more than " + _maxNumberOfSelection + " elements !");
                int numberOfItemToRemove = numberOfItemSelected - _maxNumberOfSelection;
                Debug.Log(numberOfItemToRemove);
                for (int i = 0; i < numberOfItemToRemove; i++)
                {
                    _currentSelectedElements[0].select(false);
                    _currentSelectedElements.RemoveAt(0);
                    _currentSelectedElements.Add(selectedElements.First());
                    selectedElements.RemoveAt(0);
                }
            }
            else
            {
                _currentSelectedElements.AddRange(selectedElements);
            }
        }

        public List<MSelectable> getCurrentSelectedElement() 
        {
            if (_currentSelectedElements == null && _selectables != null && _selectables.Count != 0)
                return new List<MSelectable>() { _selectables.First() };

            return _currentSelectedElements;
        }

        public void setSelectionSettings(bool unlimitedChoices = false, int maxNumberOfSelection = 1)
        {
            _unlimitedChoices = unlimitedChoices;
            _maxNumberOfSelection = maxNumberOfSelection;
        }
    }
}