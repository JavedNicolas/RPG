using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RPG.DungeonMode.Map
{
    public class RoomGameObject : MonoBehaviour
    {
        [SerializeField] RoomData _roomdata;
        [SerializeField] List<GameObject> _startPoints;
    }

}
