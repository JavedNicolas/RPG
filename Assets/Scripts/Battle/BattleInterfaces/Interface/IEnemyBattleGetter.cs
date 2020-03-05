using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RPG.DataManagement;

namespace RPG.Battle
{
    public interface IEnemyBattleGetter
    {
        List<Enemy> getEnemies();
    }
}