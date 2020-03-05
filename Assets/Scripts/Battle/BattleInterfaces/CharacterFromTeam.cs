using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterFromTeam : MonoBehaviour, ICharacterBattleGetter
{
    public List<TeamSlot> getCharacters()
    {
        return GameManager.instance.player.getCurrentTeam();
    }
}
