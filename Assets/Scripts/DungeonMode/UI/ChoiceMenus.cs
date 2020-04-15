using UnityEngine;
using System.Collections;
using RPG.Data;
using System.Collections.Generic;

namespace RPG.DungeonMode.UI
{
    public class ChoiceMenus : MonoBehaviour
    {
        [SerializeField] CharacterChoicesUI _characterChoice;

        public CharacterChoicesUI characterChoiceUI { get => _characterChoice; set => _characterChoice = value; }
    }
}