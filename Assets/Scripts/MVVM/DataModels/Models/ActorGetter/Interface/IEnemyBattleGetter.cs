using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RPG.DataModule;

namespace RPG.DataModule
{
    public interface IEnemyBattleGetter
    {
        List<Enemy> getEnemies();
    }
}