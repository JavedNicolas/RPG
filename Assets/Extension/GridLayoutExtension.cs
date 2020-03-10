using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public static class GridLayoutExtension
{
    public static void updateCellSizeBasedOnSize(this GridLayoutGroup gridLayout, int numberPerLine, int numberOfLine, bool squareItem = true)
    {
        Vector2 size = gridLayout.gameObject.GetComponent<RectTransform>().rect.size;
        float valueX = (size.x - (gridLayout.padding.left + gridLayout.padding.right + gridLayout.spacing.x)) / numberPerLine;
        float valueY = (size.y - (gridLayout.padding.top + gridLayout.padding.bottom + gridLayout.spacing.y)) / numberOfLine;
        float minValue = Mathf.Min(valueX, valueY);

        if (squareItem)
            gridLayout.cellSize = new Vector2(minValue, minValue);
        else
            gridLayout.cellSize = new Vector2(valueX, valueY);
    }

}
