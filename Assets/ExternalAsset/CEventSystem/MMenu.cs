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
        [SerializeField] bool _useChilds;
        [SerializeField] NavigationType _navigationType;
        [SerializeField] bool _unlimitedChoices;
        [SerializeField] int _maxNumberOfSelection;
        [SerializeField] List<MSelectable> _elements = new List<MSelectable>();
        List<MSelectable> _currentSelectedElements = new List<MSelectable>();

        private void Start()
        {
          
        }

        public void setElements(List<MSelectable> selectables)
        {
            _elements = new List<MSelectable>();
            selectables.ForEach(x => _elements.Add(x));
        }

        private void Update()
        {
            manualyCheckSelectedElement();
        }

        public void manualyCheckSelectedElement()
        {
            getChildrenElements();
            checkSelected();
        }

        private void getChildrenElements() {

            if (_useChilds)
            {
                _elements = transform.GetComponentsInChildren<MSelectable>().ToList();
            }
        }

        private void checkSelected()
        {
            if (_maxNumberOfSelection <= 0)
                return;

            if (_unlimitedChoices)
            {
                _currentSelectedElements = _elements.FindAll(x => x.isSelected);
                return;
            }

            // remove hidden elements from selected list
            _currentSelectedElements.RemoveAll(x => !x.gameObject.activeSelf);

            // if there is no current selected element or one the element in the list is null then reset the current selected elements
            if (_currentSelectedElements != null && (_currentSelectedElements.Count == 0 || _currentSelectedElements.Exists(x => x == null)) && _elements.Count > 0)
            {
                _currentSelectedElements = new List<MSelectable>();

                int numberOfElementToSelect = _elements.Count > _maxNumberOfSelection ? _maxNumberOfSelection : _elements.Count;
                List<MSelectable> elementToSelect = _elements.FindAll(x => x.gameObject.activeSelf).GetRange(0, numberOfElementToSelect);

                _currentSelectedElements.AddRange(elementToSelect);
                _currentSelectedElements.ForEach(x => x.select(true));
                return;
            }

            // find selected elements
            List<MSelectable> updatedcurrentSelectedElements = _elements.FindAll(x => x.isSelected && x.gameObject.activeSelf);

            // remove not selected elements which where selected
            if (updatedcurrentSelectedElements.Count < _currentSelectedElements.Count && _currentSelectedElements.Count > 0)
            {
                for (int i = 0; i < _currentSelectedElements.Count; i++)
                {
                    if (!updatedcurrentSelectedElements.Contains(_currentSelectedElements[i]))
                        _currentSelectedElements.Remove(_currentSelectedElements[i]);
                }
                
                return;
            }

            // find selected elements which are not in the current selected List
            updatedcurrentSelectedElements = updatedcurrentSelectedElements.FindAll(x => !_currentSelectedElements.Contains(x));

            int numberOfItemSelected = updatedcurrentSelectedElements.Count + _currentSelectedElements.Count;
            if (numberOfItemSelected > _maxNumberOfSelection && updatedcurrentSelectedElements.Count > 0)
            {
                int numberOfItemToRemove = numberOfItemSelected - _maxNumberOfSelection;
                for (int i = 0; i < numberOfItemToRemove; i++)
                {
                    _currentSelectedElements[0].select(false);
                    _currentSelectedElements.RemoveAt(0);
                    _currentSelectedElements.Add(updatedcurrentSelectedElements.First());
                    updatedcurrentSelectedElements.RemoveAt(0);
                }
            }
            else
            {
                _currentSelectedElements.AddRange(updatedcurrentSelectedElements);
            }
        }

        public List<MSelectable> getCurrentSelectedElement() 
        {
            if (_currentSelectedElements == null && _elements != null && _elements.Count != 0)
                return new List<MSelectable>() { _elements.First() };

            return _currentSelectedElements;
        }

        public void setSelectionSettings(bool unlimitedChoices = false, int maxNumberOfSelection = 1, bool useChilds = false)
        {
            _unlimitedChoices = unlimitedChoices;
            _maxNumberOfSelection = maxNumberOfSelection;
            _useChilds = useChilds;
        }

        public List<MSelectable> getElements() { return _elements; }
    }
}