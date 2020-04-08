using UnityEngine;
using System.Collections;
using RPG.UI;
using RPG.DungeonMode.UI;
using RPG.Data;
using System.Collections.Generic;

namespace RPG.DungeonMode
{
    public class DungeonModeUI : MonoBehaviour
    {
        [SerializeField] CharacterChoicesUI _characterChoiceUI;
        public MapDisplayer mapDisplayer { get; private set; }

        public CharacterChoicesUI characterChoiceUI => _characterChoiceUI;

        private void Awake()
        {
            mapDisplayer = GetComponent<MapDisplayer>();
        }

        public void generateCharacterChoices()
        {
            List<Character> characters = DungeonManager.instance.characterDatabase.getRandomElements(3, false);
            characterChoiceUI.display(true);
            characterChoiceUI.initChoices(characters, DungeonManager.instance.team.getCurrentTeamCharacters());
            characterChoiceUI.choosed += DungeonManager.instance.endCurentRoom;
        }
    }
}