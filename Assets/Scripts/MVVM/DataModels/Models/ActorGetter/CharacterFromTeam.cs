using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RPG.DataModule
{
    public class CharacterFromTeam : ICharacterBattleGetter
    {
        public List<TeamSlot<Character>> getCharacters()
        {
            return GameManager.instance.team.currentTeam;
        }
    }
}
