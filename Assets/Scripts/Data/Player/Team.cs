using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RPG.Data.Team
{
    public class Team 
    {
        public const int MAX_TEAM_SIZE = 3;

        List<TeamSlot> _team = new List<TeamSlot>();
        List<Character> _roster = new List<Character>();

        public List<TeamSlot> currentTeam => _team;

        public List<Character> getCurrentTeamCharacters()
        {
            List<Character> currentTeamCharacter = new List<Character>();
            _team.ForEach(x => currentTeamCharacter.Add(x.character));

            return currentTeamCharacter;
        }

        public void addCharacterToTeam(Character character, bool isInFront, BattlePosition position)
        {
            if (_team.Count < MAX_TEAM_SIZE)
            {
                _team.Add(new TeamSlot() { character = character, frontPosition = isInFront, battlePosition = position });
            }
        }

        public void fullHealTeam()
        {
            _team.ForEach(x => x.character.damage(-x.character.maxLife));
        }
    }
}
