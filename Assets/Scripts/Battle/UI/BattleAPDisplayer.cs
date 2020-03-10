using UnityEngine;
using System.Collections;
using TMPro;
using System.Collections.Generic;

public class BattleAPDisplayer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _actionPointTextField;
    [SerializeField] List<ActionPointItem> _actionPointsItemsPool;

    public void updateActionPointDisplay(int remainingActionPoint, int maxActionPoint)
    {
        // display remaining point as text
        _actionPointTextField.text = remainingActionPoint.ToString();

        // hide all action point
        _actionPointsItemsPool.ForEach(x => x.gameObject.SetActive(false));

        // set the action dots
        for (int i =0; i < maxActionPoint; i++)
        {
            _actionPointsItemsPool[i].gameObject.SetActive(true);
            _actionPointsItemsPool[i].setFilled(i < remainingActionPoint);
        }
    }
}
