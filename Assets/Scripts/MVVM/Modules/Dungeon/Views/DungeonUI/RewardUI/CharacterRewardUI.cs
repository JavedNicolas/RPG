using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace RPG.DungeonModule.View
{
    using RPG.OpenModule.View;

    public class CharacterRewardUI : RewardUI
    {
        public override void choiceDone()
        {
            List<Choices> choices = getSelectedRewards();
            if (choices == null)
                return;

            for (int i = 0; i < choices.Count; i++)
            {
                Team team = DungeonManager.instance.team;
                TeamSlot teamSlot = team.currentTeam[i];
                Character character = Character.CreateInstance<Character>();
                character.init(choices[i].reward.element);
                DungeonManager.instance.team.addCharacterToTeam(character, teamSlot.frontPosition, teamSlot.battlePosition);
            };

            isDone();
        }
    }
}