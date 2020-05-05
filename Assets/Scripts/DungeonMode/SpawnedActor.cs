using UnityEngine;
using RPG.Data;

namespace RPG.DungeonMode
{
    public class SpawnedActor<T> : Being
    {
        public T actor;
        public GameObject gameObject;

        public SpawnedActor(T actor, GameObject gameObject)
        {
            this.actor = actor;
            this.gameObject = gameObject;
        }
    }
}