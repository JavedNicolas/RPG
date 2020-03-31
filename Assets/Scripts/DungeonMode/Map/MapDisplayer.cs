using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RPG.DungeonMode.Map
{
    public class MapDisplayer : MonoBehaviour
    {
        [SerializeField] Transform _mapParent;
        [SerializeField] GameObject _roomPrefab;
        [SerializeField] GameObject _choicePrefab;
        
        public void displayMap(List<List<Room>> rooms)
        {
            for(int i = 0; i < rooms.Count; i++)
            {
                List<Room> choice = rooms[i];
                GameObject currentChoice = Instantiate(_choicePrefab, _mapParent);
                for (int j = 0; j < choice.Count; j++)
                {
                    Instantiate(_roomPrefab, currentChoice.transform);
                }
            }
        }
    }

}
