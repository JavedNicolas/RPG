using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.StandardMode
{
    public class NoBattleManager : MonoBehaviour
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

