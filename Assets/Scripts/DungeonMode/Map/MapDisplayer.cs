using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RPG.DungeonMode.Map
{
    public class MapDisplayer : MonoBehaviour
    {
        [SerializeField] Transform _mapParent;
        [SerializeField] Transform _mapLinkParent;
        [SerializeField] GameObject _roomPrefab;
        [SerializeField] GameObject _choicePrefab;
        
        public void displayMap(List<List<RoomData>> rooms)
        {
            for(int i = 0; i < rooms.Count; i++)
            {
                List<RoomData> choice = rooms[i];
                GameObject currentChoice = Instantiate(_choicePrefab, _mapParent);
                for (int j = 0; j < choice.Count; j++)
                {
                    GameObject roomMapItem = Instantiate(_roomPrefab, currentChoice.transform);
                    rooms[i][j].setMapItem(roomMapItem);
                }
            }

            IEnumerator coroutine = drawLinks(rooms);
            StartCoroutine(coroutine);
        }

        private IEnumerator drawLinks(List<List<RoomData>> roomsData)
        {
            yield return new WaitForEndOfFrame();
            roomsData.ForEach(x =>
            {
                x.ForEach(r =>
                {
                    if (r.nextRoomData != null || r.nextRoomData.Count != 0)
                    {
                        LineRenderer lineRenderer;

                        if (r.mapItem.TryGetComponent<LineRenderer>(out lineRenderer))
                        {
                            lineRenderer.positionCount = r.nextRoomData.Count * 2;
                            for (int i = 0; i < r.nextRoomData.Count; i++)
                            {
                                lineRenderer.SetPosition(i*2, r.mapItem.transform.position);
                                lineRenderer.SetPosition(i*2 + 1, r.nextRoomData[i].mapItem.transform.position);
                            }
                        }
                    }
                });
            });
        }
    }

}
