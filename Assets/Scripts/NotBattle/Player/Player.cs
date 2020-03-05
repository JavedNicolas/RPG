using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public const int MAX_TEAM_SIZE = 3;

    Inventory inventory;
    List<TeamSlot> team = new List<TeamSlot>();
    List<Character> roster = new List<Character>();

    public List<TeamSlot> getCurrentTeam() => team;

    public void addCharacterToTeam(Character character, bool isInFront, BattlePosition position)
    {
        if(team.Count < MAX_TEAM_SIZE)
        {
            team.Add(new TeamSlot() { character = character, frontPosition = isInFront, battlePosition = position });
        }
    }
}
