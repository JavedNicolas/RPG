using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using System.Collections;


namespace RPG.DungeonMode.Map
{
    using RPG.DungeonMode.Dungeon;

    public class GridMapDisplayer : MapDisplayer
    {
        [SerializeField] Sprite[] _layoutIcons;
        [SerializeField] GameObject _mapIconPrefab;
        [SerializeField] GridLayoutGroup _mapGridLayout;
        
        public override void displayMap(Room[,] rooms, Room startRoom)
        {
            _mapGridLayout.constraintCount = rooms.GetLength(1);

            for (int i = 0; i < rooms.GetLength(0); i++)
            {
                for (int j = 0; j < rooms.GetLength(1); j++)
                {
                    GameObject mapItem = Instantiate(_mapIconPrefab, _mapGridLayout.transform);
                    RoomMapItem roomMapItem = mapItem.GetComponent<RoomMapItem>();
                    if (rooms[i,j] != null)
                    {
                        Room room = rooms[i, j];
                        Sprite sprite = getSprite(room);

                        roomMapItem.setLayoutImage(sprite);
                        roomMapItem.setButtonAction(delegate { roomChosed(room); });

                        room.setMapItem(mapItem);
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


        IEnumerator updateParentSizeToFit()
        {
            yield return new WaitForEndOfFrame();

            _mapGridLayout.transform.parent.GetComponent<RectTransform>().sizeDelta = _mapGridLayout.GetComponent<RectTransform>().sizeDelta * -1;
        }


        Sprite getSprite(Room roomData)
        {
            string spriteName = ""; 

            // create a sprite name using a letter for each linked room direction
            foreach(Room linkedRoom in roomData.linkedRoom)
            {
                Vector3 direction = linkedRoom.gameObject.transform.position - roomData.gameObject.transform.position;
                direction = direction.normalized;
                if(direction == Vector3.forward)
                    spriteName += "T";
                else if (direction == Vector3.right)
                    spriteName += "R";
                else if (direction == Vector3.back)
                    spriteName += "B";
                else if (direction == Vector3.left)
                    spriteName += "L";
            }

            // search for a sprite with the name containing all the sprite name letters
            Sprite fittingSprite = null;
            List<Sprite> searchingArray = new List<Sprite>(_layoutIcons);
            for (int i = 0; i < spriteName.Length; i++)
                searchingArray = searchingArray.FindAll(x => x.name.Contains(spriteName[i]));

            fittingSprite = searchingArray.Find(x => x.name.Length == spriteName.Length);

            return fittingSprite;
            
        }

    }
}