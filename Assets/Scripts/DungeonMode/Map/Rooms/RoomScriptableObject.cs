using UnityEngine;

namespace RPG.DungeonMode.Map
{
    public abstract class RoomScriptableObject : ScriptableObject
    {
        [SerializeField] string _name;
        [SerializeField] GameObject _prefab;
        public GameObject prefab => _prefab;

        public abstract void effect();
    }
}