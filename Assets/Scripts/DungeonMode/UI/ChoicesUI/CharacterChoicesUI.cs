using UnityEngine;
using System.Collections;
using RPG.Data;
using RPG.Data.Team;

namespace RPG.DungeonMode.UI
{
    public class CharacterChoicesUI : DungeonChoiceUI<Character>
    {
        public override void choiceDone()
        {
            Choice<Character> choice = choices.Find(x => x.isSelected);
            if(choice != null)
            {
                Team team = DungeonManager.instance.team;
                TeamSlot teamSlot = team.currentTeam.Find(x => x.character == currentSelectedPlayerElement);

                DungeonManager.instance.team.addCharacterToTeam(currentSelectedChoice.element, teamSlot.frontPosition, teamSlot.battlePosition);
            }
        }
    }
}