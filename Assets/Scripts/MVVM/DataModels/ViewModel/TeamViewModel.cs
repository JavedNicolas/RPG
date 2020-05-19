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
            team.currentTeam.ForEach(x => infos.Add(characterViewModel.getCharacterInfo(x.being)));

            return infos;
        }

        /// <summary>
        /// Get a index for the team slot current battle position.
        /// </summary>
        /// <param name="teamSlot"></param>
        /// <returns></returns>
        public int getIndexForTeamPosition<T>(TeamSlot<T> teamSlot)
        {
            List<BattlePosition> position = Enum.GetValues(typeof(BattlePosition)).Cast<BattlePosition>().ToList();
            int indexOfPosition = position.FindIndex(x => x.ToString() == teamSlot.battlePosition.ToString());
            int isInFrontFactor = teamSlot.frontPosition ? 0 : 1;
            return indexOfPosition + (isInFrontFactor * position.Count);
        }

    }
}