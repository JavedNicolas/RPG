using UnityEngine;
using System.Collections;
using RPG.Data;
using System.Collections.Generic;

namespace RPG.DungeonMode.UI
{
    public class RewardMenus : MonoBehaviour
    {
        [SerializeField] CharacterRewardUI _characterReward;

        public CharacterRewardUI characterRewardUI { get => _characterReward; set => _characterReward = value; }
    }
}