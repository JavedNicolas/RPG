using UnityEngine;
using System.Collections;

namespace RPG.DataModule
{
    public class CharacterViewModel
    {
        public (string name, string description, Sprite icon) getCharacterInfo(Character character)
        {
            return (name: character.name, description: character.description, icon: character.icon);
        }
    }
}