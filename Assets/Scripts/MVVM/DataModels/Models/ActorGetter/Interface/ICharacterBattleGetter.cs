using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RPG.DataModule
{
    public interface ICharacterBattleGetter
    {
        List<TeamSlot<Character>> getCharacters();
    }
}
