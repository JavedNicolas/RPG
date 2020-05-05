using RPG.Data;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Collections;
using System;

namespace RPG.DungeonMode.Dungeon
{
    public abstract class RoomScriptableObject : DatabaseElement
    {
        [Tooltip("If true this room is a static room, this mean that it will always be placed at a specific position in the dungeon. \n" +
            "This room will not be pulled randomly .\n" +
            "Ex: Starting Room, Boss etc.")]
        [SerializeField] bool _isStaticRoom = false;
        public bool isStaticRoom => _isStaticRoom;

        [SerializeField] bool _isSpecialRoom = false;
        public bool isSpecialRoom => _isSpecialRoom;

        [HideIf("_isSpecialRoom", animate: true)]
        [SerializeField] bool _cannotBranch = false;
        public bool cannotBranch => _cannotBranch;

        [SerializeField] string _name;
        public new string name => _name;

        [SerializeField] Sprite _roomIcon;
        public Sprite roomIcon => _roomIcon;

        [ShowIf("_isSpecialRoom", animate: true)]
        [SerializeField] List<GameObject> _prefabs = new List<GameObject>();
        public List<GameObject> prefabs => _prefabs;

        public delegate void RoomEffectDone();
        public RoomEffectDone effectDone;

        public abstract IEnumerator effect();
    }
}