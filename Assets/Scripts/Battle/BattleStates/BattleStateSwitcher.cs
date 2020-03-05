using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BattleStateSwitcher 
{
    // States
    public BattleState currentBattleState { get; private set; }
    public Dictionary<Type, BattleState> battleStates { get; private set; }

    public BattleStateSwitcher(BattleStateManager battleStateManager, BattleManager battleManager)
    {
        BattleState.init(battleStateManager, battleManager);
        initBattleStates();
    }

    public void initBattleStates()
    {
        battleStates = new Dictionary<Type, BattleState>();
        battleStates.Add(typeof(BattleStartState), new BattleStartState());
        battleStates.Add(typeof(PlayerTurn), new PlayerTurn());
        battleStates.Add(typeof(EnemyTurn), new EnemyTurn());
    }

    private bool updateCurrentState(Type battleStateType)
    {
        BattleState battleState = battleStates[battleStateType];
        if (battleState == null)
            return false;

        currentBattleState = battleState;
        return true;
    }

    public void changeState(Type battleStateType)
    {
        if (updateCurrentState(battleStateType))
            currentBattleState.start();
        else
            Debug.LogErrorFormat("State {0} not found !", battleStateType);
    }

}
