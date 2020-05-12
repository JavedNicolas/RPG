using UnityEngine;
using RPG.DataModule;

namespace RPG.DataModule
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