using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.StoryMode
{
    public class StoryModeManager : MonoBehaviour
    {
        // Attributs
        [SerializeField] public bool canMove = true;
        [SerializeField] public bool isInsideInventory = false;
        [SerializeField] public bool isRandomBattleOn = true;

        public void Update()
        {

        }
    }
}

