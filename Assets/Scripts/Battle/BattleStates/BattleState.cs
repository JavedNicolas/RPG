using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public abstract class BattleState
{
    protected static BattleStateManager _battleStateManager { get; private set; }
    protected static BattleManager _battleManager { get; private set; }

    /// <summary>
    /// init the battle manager instance for each battle state
    /// </summary>
    /// <param name="battleStateManager"></param>
    public static void init(BattleStateManager battleStateManager, BattleManager battleManager)
    {
        _battleStateManager = battleStateManager;
        _battleManager = battleManager;
    }

    /// <summary>
    /// init the state
    /// </summary>
    public abstract void start();

    /// <summary>
    /// Contain execute logic of the state
    /// </summary>
    public abstract void executeState();
    public abstract void endTurn();

    public abstract void useAction(BattleTarget target, BattleSpawningPoint senderSpawn);

    protected void changeStateBasedOnOrderList()
    {
        List<Being> battleOrders = _battleStateManager.battleOrder.current;
        ActorTurnBattleState.currentActor = battleOrders.First();

        if (battleOrders.First().GetType() == typeof(Enemy))
            ChangeState(typeof(EnemyTurn));
        else
            ChangeState(typeof(PlayerTurn));
    }
    protected virtual void ChangeState(Type stateType) {
        _battleStateManager.changeState(stateType); 
    }
}
