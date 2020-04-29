using UnityEngine;
using System.Collections;

namespace RPG.DungeonMode
{
    public class CharacterSpawner : MonoBehaviour
    {
        [SerializeField] Transform _characterHolder;

        public GameObject spawnCharacter(GameObject model, Vector3 position)
        {
            return Instantiate(model, position, Quaternion.identity, _characterHolder);
        }
    }
}