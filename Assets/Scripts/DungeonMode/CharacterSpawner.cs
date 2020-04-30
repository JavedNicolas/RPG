using UnityEngine;
using System.Collections;

namespace RPG.DungeonMode
{
    public class CharacterSpawner : MonoBehaviour
    {
        [SerializeField] Transform _characterHolder;

        public GameObject spawnCharacter(GameObject model, Vector3 position)
        {
            GameObject gameObject = Instantiate(model, position, Quaternion.identity, _characterHolder);
            gameObject.transform.LookAt( gameObject.transform.position + _characterHolder.forward);
            return gameObject;
        }
    }
}