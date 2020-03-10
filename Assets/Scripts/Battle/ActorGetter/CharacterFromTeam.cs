using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RPG.Data.Team;

namespace RPG.Battle
{
    public class CharacterFromTeam : MonoBehaviour, ICharacterBattleGetter
    {
        public List<TeamSlot> getCharacters()
        {
            return GameManager.instance.team.getCurrentTeam;
        }
    }
}
