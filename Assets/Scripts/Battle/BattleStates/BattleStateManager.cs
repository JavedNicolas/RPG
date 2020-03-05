using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleStateManager : MonoBehaviour
{
    #region attributs
    //Battle states
    public BattleStateSwitcher stateSwitcher { get; private set; }

    //battle order
    public BattleOrder battleOrder { get; private set; }
    #endregion

    public void startBattle(List<Being> characters, List<Being> enemies, BattleManager battleManager)
    {
        stateSwitcher = new BattleStateSwitcher(this, battleManager);
        battleOrder = new BattleOrder();
        battleOrder.generateBattleOrder(characters, enemies);
        changeState(typeof(BattleStartState));
    }

    public void changeState(Type battleStateType)
    {
        stateSwitcher.changeState(battleStateType);
    }

    // getter
    public bool isPlayerTurn() { return stateSwitcher.currentBattleState is PlayerTurn ? true : false; }


}
