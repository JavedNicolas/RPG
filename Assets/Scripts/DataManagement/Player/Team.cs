using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RPG.DataManagement.Team
{
    public class Team 
    {
        public const int MAX_TEAM_SIZE = 3;

        List<TeamSlot> _team = new List<TeamSlot>();
        List<Character> _roster = new List<Character>();

        public List<TeamSlot> getCurrentTeam => _team;

        public void addCharacterToTeam(Character character, bool isInFront, BattlePosition position)
        {
            if (_team.Count < MAX_TEAM_SIZE)
            {
                _team.Add(new TeamSlot() { character = character, frontPosition = isInFront, battlePosition = position });
            }
        }
    }
}
