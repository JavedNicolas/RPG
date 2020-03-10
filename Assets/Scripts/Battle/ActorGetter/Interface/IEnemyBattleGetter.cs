using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RPG.Data;

namespace RPG.Battle
{
    public interface IEnemyBattleGetter
    {
        List<Enemy> getEnemies();
    }
}