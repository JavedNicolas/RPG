using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RPG.Data.Team;

namespace RPG.Battle
{
    public interface ICharacterBattleGetter
    {
        List<TeamSlot> getCharacters();
    }
}
