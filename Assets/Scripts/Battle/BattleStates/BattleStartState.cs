using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class BattleStartState : BattleState
{
    public override void start()
    {
        endTurn();
    }

    public override void executeState()
    {
        
    }

    public override void useAction(BattleTarget target, BattleSpawningPoint senderSpawn)
    {
        
    }

    public override void endTurn()
    {
        changeStateBasedOnOrderList();
    }
}
