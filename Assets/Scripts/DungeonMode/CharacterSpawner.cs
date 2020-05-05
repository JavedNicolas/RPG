using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RPG.Data;

namespace RPG.DungeonMode
{
    public class CharacterSpawner : MonoBehaviour
    {
        [SerializeField] Transform _characterHolder;
        [SerializeField] public List<SpawnedActor<Character>> spawnedCharacter { get; private set; }

        private void Start()
        {
            spawnedCharacter = new List<SpawnedActor<Character>>();
        }

        public void spawnCharacter(Character character, Vector3 position)
        {
            GameObject gameObject = Instantiate(character.dungeonModeModel, position, Quaternion.identity, _characterHolder);
            gameObject.transform.LookAt( gameObject.transform.position + _characterHolder.forward);
            spawnedCharacter.Add(new SpawnedActor<Character>(character, gameObject));
        }
    }
}