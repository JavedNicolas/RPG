using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using RPG.settings;
using System.Linq;

namespace RPG.DataModule
{
    public class Team 
    {
        public const int MAX_TEAM_SIZE = 3;

        List<TeamSlot> _team = new List<TeamSlot>();
        List<Character> _roster = new List<Character>();

        public List<TeamSlot> currentTeam => _team;

        public Team()
        {
            List<BattlePosition> position = Enum.GetValues(typeof(BattlePosition)).Cast<BattlePosition>().ToList();

            for (int i = 0; i < MAX_TEAM_SIZE; i++)
            {
                bool isInFront = i / position.Count > 1 ? false : true;
                int currentPositionIndex = i % (MAX_TEAM_SIZE);
                Character character = new Character();
                character.initEmpty();
                _team.Add(new TeamSlot(character, isInFront, position[currentPositionIndex]));
            }
        }

        public List<Character> getCurrentTeamCharacters()
        {
            List<Character> currentTeamCharacter = new List<Character>();
            _team.ForEach(x => currentTeamCharacter.Add(x.character));

            return currentTeamCharacter;
        }

        /// <summary>
        /// Add a character to the current team
        /// </summary>
        /// <param name="character">The character to add</param>
        /// <param name="isInFront">is the character in front </param>
        /// <param name="battlePosition"> His position in the battle </param>
        public void addCharacterToTeam(Character character, bool isInFront, BattlePosition battlePosition)
        {

            if(!_team.Exists(x => x.character.isEmpty()))
            { 
                Debug.Log("No space in team, character is added to roster");
                addCharacterToRoster(character);
                return;
            }

            int index = _team.FindIndex(x => x.character.isEmpty());
            _team[index] = new TeamSlot(character, isInFront, battlePosition);
        }

        /// <summary>
        /// Add character to the roster which is available character for the player to put in battle
        /// </summary>
        /// <param name="character"></param>
        public void addCharacterToRoster(Character character)
        {
            _roster.Add(character);
        }

        /// <summary>
        /// remove a character from the roster
        /// </summary>
        /// <param name="character"></param>
        public void removeCharacterFromRoster(Character character)
        {
            _roster.Remove(character);
        }


        public void fullHealTeam()
        {
            _team.ForEach(x => x.character.damage(-x.character.maxLife));
        }
    }
}
