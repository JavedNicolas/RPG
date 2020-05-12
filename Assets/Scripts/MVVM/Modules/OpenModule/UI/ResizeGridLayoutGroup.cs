using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace RPG.OpenModule.View
{
    public class ResizeGridLayoutGroup : MonoBehaviour
    {
        [SerializeField, ShowInInspector] bool _adaptCellSizeToRectTransfromSize = false;
        [SerializeField, ShowInInspector] bool _keepItemSquare = true;
        [SerializeField, ShowInInspector] int _numberOfItemPerLine = 1;
        [SerializeField, ShowInInspector] int _numberOfLine = 1;
        [SerializeField] GridLayoutGroup _gridLayoutGroup;

        private void Start()
        {
            if (_adaptCellSizeToRectTransfromSize)
            {
                _gridLayoutGroup.updateCellSizeBasedOnSize(_numberOfItemPerLine, _numberOfLine, _keepItemSquare);
            }
        }

        public void updateSize(int numberOfItemPerLine, int numberOfLine)
        {
            _numberOfItemPerLine = numberOfItemPerLine;
            _numberOfLine = numberOfLine;
            updateSize();
        }

        [Button("Update size")]
        public void updateSize()
        {
            if (_adaptCellSizeToRectTransfromSize)
            {
                _gridLayoutGroup.updateCellSizeBasedOnSize(_numberOfItemPerLine, _numberOfLine, _keepItemSquare);
            }
        }
    }
}

