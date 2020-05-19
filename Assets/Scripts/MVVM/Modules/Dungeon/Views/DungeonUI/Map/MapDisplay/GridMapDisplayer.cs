using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using System.Collections;


namespace RPG.DungeonModule.View
{

    public class GridMapDisplayer : MonoBehaviour
    {
        [SerializeField] GameObject _mapIconPrefab;
        [SerializeField] GridLayoutGroup _mapGridLayout;
        [SerializeField] Transform _mapParent;

        GameObject[,] mapItems;
        bool canChooseARoom = false;

        #region room click display
        public delegate bool RoomChosed(int heightIndex, int widthIndex);
        public RoomChosed roomChoosed;
        #endregion

        public void canChangeRoom(bool canChooseARoom)
        {
            this.canChooseARoom = canChooseARoom;
        }

        public void generateMap(Sprite[,] roomSprites)
        {
            _mapGridLayout.constraintCount = roomSprites.GetLength(1);
            mapItems = new GameObject[roomSprites.GetLength(0), roomSprites.GetLength(1)];
            for (int i = 0; i < roomSprites.GetLength(0); i++)
            {
                for (int j = 0; j < roomSprites.GetLength(1); j++)
                {
                    GameObject mapItem = Instantiate(_mapIconPrefab, _mapGridLayout.transform);
                    mapItems[i,j] = mapItem;

                    RoomMapItem roomMapItem = mapItem.GetComponent<RoomMapItem>();
                    if (roomSprites[i,j] != null)
                    {
                        Sprite roomSprite = roomSprites[i, j];

                        roomMapItem.setLayoutImage(roomSprite);

                        (int i, int j) indexes = (i, j); // avoid index corrumpt 
                        roomMapItem.setButtonAction(delegate { fireDelegate(indexes.i, indexes.j); });
                    }
                    else
                    {
                        // transparent code
                        roomMapItem.hide(true);
                    }
                }
            }
            StartCoroutine("updateParentSizeToFit");
        }

        public void fireDelegate(int heightIndex, int widthIndex)
        {
            if (canChooseARoom)
                if(roomChoosed(heightIndex, widthIndex))
                    mapItems[heightIndex, widthIndex].GetComponent<RoomMapItem>().isCurrentRoom(true);
        }

        /// <summary>
        /// Set a room as the current room
        /// </summary>
        /// <param name="heightIndex"></param>
        /// <param name="widthIndex"></param>
        public void selectedRoom(int heightIndex, int widthIndex)
        {
            mapItems[heightIndex, widthIndex].GetComponent<RoomMapItem>().isCurrentRoom(true);
        }

        /// <summary>
        /// Set a room as cleared
        /// </summary>
        /// <param name="heightIndex"></param>
        /// <param name="widthIndex"></param>
        public void roomCleared(int heightIndex, int widthIndex)
        {
            mapItems[heightIndex, widthIndex].GetComponent<RoomMapItem>().haveBeenCleared(true);
;        }

        /// <summary>
        /// Update the size of the grid layout to fit the map
        /// </summary>
        /// <returns></returns>
        IEnumerator updateParentSizeToFit()
        {
            yield return new WaitForEndOfFrame();

            _mapGridLayout.transform.parent.GetComponent<RectTransform>().sizeDelta = _mapGridLayout.GetComponent<RectTransform>().sizeDelta * -1;
        }


    }
}