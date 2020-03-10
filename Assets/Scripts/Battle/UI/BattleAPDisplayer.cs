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
        _actionPointTextField.text = remainingActionPoint.ToString();

        _actionPointsItemsPool.ForEach(x => x.gameObject.SetActive(false));

        for (int i =0; i < maxActionPoint; i++)
        {
            _actionPointsItemsPool[i].gameObject.SetActive(true);
            _actionPointsItemsPool[i].setFilled(i < remainingActionPoint);
        }
    }
}
