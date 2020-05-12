using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace RPG.DataModule
{
    public class TeamViewModel
    {
        /// <summary>
        /// Convert a team in the basic attributs for a view
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        public List<(string name, string description, Sprite icon)> getTeamInfos(Team team)
        {
            List<(string name, string description, Sprite icon)> infos = new List<(string name, string description, Sprite icon)>();
            CharacterViewModel characterViewModel = new CharacterViewModel();
            team.currentTeam.ForEach(x => infos.Add(characterViewModel.getCharacterInfo(x.character)));

            return infos;
        }

        /// <summary>
        /// Get a index for the team slot current battle position.
        /// </summary>
        /// <param name="teamSlot"></param>
        /// <returns></returns>
        public int getIndexForTeamPosition(TeamSlot teamSlot)
        {
            List<string> position = Enum.GetValues(typeof(BattlePosition)).Cast<string>().ToList();
            int indexOfPosition = position.FindIndex(x => x == teamSlot.battlePosition.ToString());
            int isInFrontFactor = teamSlot.frontPosition ? 1 : 0;
            return indexOfPosition + (isInFrontFactor * position.Count);
        }

    }
}