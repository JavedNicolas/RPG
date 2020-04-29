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
        [SerializeField] TabMenu _menu;
        public MapDisplayer mapDisplayer { get; private set; }

        public TabMenu menu => _menu;

        private void Awake()
        {
            mapDisplayer = GetComponent<MapDisplayer>();
        }

        public void displayCharacterReward(List<Character> characters)
        {
            _menu.display(true, true);
            _menu.rewardMenus.characterRewardUI.initRewards(characters, DungeonManager.instance.team.getCurrentTeamCharacters());
            _menu.rewardMenus.characterRewardUI.isDone = DungeonManager.instance.endCurentState;
        }
    }
}