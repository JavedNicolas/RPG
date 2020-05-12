using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RPG.DataModule.Team;

namespace RPG.DataModule
{
    public class CharacterFromTeam : ICharacterBattleGetter
    {
        public List<TeamSlot> getCharacters()
        {
            return GameManager.instance.team.currentTeam;
        }
    }
}
