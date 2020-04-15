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
        [SerializeField] Menu _menu;
        public MapDisplayer mapDisplayer { get; private set; }

        public Menu menu => _menu;

        private void Awake()
        {
            mapDisplayer = GetComponent<MapDisplayer>();
        }

        public void generateCharacterChoices()
        {
            List<Character> characters = DungeonManager.instance.characterDatabase.getRandomElements(3, false);
            _menu.choiceMenus.characterChoiceUI.display(true);
            _menu.choiceMenus.characterChoiceUI.initChoices(characters, DungeonManager.instance.team.getCurrentTeamCharacters());
            _menu.choiceMenus.characterChoiceUI.choosed += DungeonManager.instance.endCurentRoom;
        }
    }
}