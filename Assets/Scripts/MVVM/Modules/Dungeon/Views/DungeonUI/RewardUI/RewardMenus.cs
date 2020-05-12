using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RPG.DungeonModule.View
{
    public class RewardMenus : MonoBehaviour
    {
        [SerializeField] CharacterRewardUI _characterReward;

        public CharacterRewardUI characterRewardUI { get => _characterReward; set => _characterReward = value; }
    }
}