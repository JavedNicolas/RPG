using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RPG.DataManagement.Team;

namespace RPG.Battle
{
    public interface ICharacterBattleGetter
    {
        List<TeamSlot> getCharacters();
    }
}
