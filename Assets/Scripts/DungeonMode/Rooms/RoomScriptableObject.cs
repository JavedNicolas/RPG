using RPG.Data;
using UnityEngine;

namespace RPG.DungeonMode.Dungeon
{
    public abstract class RoomScriptableObject : DatabaseElement
    {
        [Tooltip("If true this room is a static room, this mean that it will always be placed at a specific position in the dungeon. \n" +
            "This room will not be pulled randomly .\n" +
            "Ex: Starting Room, Boss etc.")]
        [SerializeField] bool _isStaticRoom = false;
        public bool isStaticRoom => _isStaticRoom;

        [SerializeField] bool _cannotBranch = false;
        public bool cannotBranch => _cannotBranch;

        [SerializeField] string _name;
        public new string name => _name;

        [SerializeField] Sprite _roomIcon;
        public Sprite roomIcon => _roomIcon;

        public abstract void effect();
    }
}