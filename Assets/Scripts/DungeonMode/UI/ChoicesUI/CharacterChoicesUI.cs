using UnityEngine;
using System.Collections;
using RPG.Data;
using RPG.Data.Team;
using System.Collections.Generic;

namespace RPG.DungeonMode.UI
{
    public class CharacterChoicesUI : DungeonChoiceUI<Character>
    {
        public override void choiceDone()
        {
            List<ChoiceElements<Character>> choices = getSelectedChoices();
            if (choices == null)
                return;

            for (int i = 0; i < choices.Count; i++)
            {
                Team team = DungeonManager.instance.team;
                TeamSlot teamSlot = team.currentTeam.Find(x => x.character == choices[i].playerElement);

                DungeonManager.instance.team.addCharacterToTeam(choices[i].choice.element, teamSlot.frontPosition, teamSlot.battlePosition);
            };
        }
    }
}