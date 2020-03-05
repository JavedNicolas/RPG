using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RPG.DataManagement;

namespace RPG.Battle
{
    public class RandomEnemyFromDatabase : MonoBehaviour, IEnemyBattleGetter
    {
        public List<Enemy> getEnemies()
        {
            int randomEnemy = UnityEngine.Random.Range(2, 6);
            List<Enemy> enemies = new List<Enemy>();
            GameManager.instance.enemyDatabase.getRandomElements(randomEnemy, true).ForEach(x =>
            {
                Enemy enemy = Enemy.CreateInstance<Enemy>();
                enemy.init(x);
                enemies.Add(enemy);
            });
            return enemies;
        }
    }
}
