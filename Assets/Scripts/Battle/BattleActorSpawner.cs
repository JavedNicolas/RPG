using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BattleActorSpawner : MonoBehaviour
{
    [SerializeField] List<BattleSpawningPoint> _characterSpawningPoints;
    [SerializeField] List<BattleSpawningPoint> _enemySpawningPoints;

    // actor getters
    IEnemyBattleGetter _enemyGetter;
    ICharacterBattleGetter _characterBattleGetter;

    private void Awake()
    {
        _enemyGetter = GetComponent<IEnemyBattleGetter>();
        _characterBattleGetter = GetComponent<ICharacterBattleGetter>();
    }

    public void spawnActors()
    {
        List<TeamSlot> teamSlots = _characterBattleGetter.getCharacters();
        foreach (TeamSlot teamSlot in teamSlots)
        {
            BattleSpawningPoint battleSpawningPoint = _characterSpawningPoints.Find(x => x.position == teamSlot.battlePosition && x.isFrontSpawn == teamSlot.frontPosition);
            battleSpawningPoint.actor = teamSlot.character;
            battleSpawningPoint.actorGameObject = GameObject.Instantiate(teamSlot.character.model, battleSpawningPoint.transform);
        }

        List<Enemy> enemies = _enemyGetter.getEnemies();
        foreach (Enemy enemy in enemies)
        {
            List<BattleSpawningPoint> spawnPointAvailable = _enemySpawningPoints.FindAll(x => x.actor == null);

            if (spawnPointAvailable == null || spawnPointAvailable.Count == 0)
                return;

            BattleSpawningPoint battleSpawningPoint = spawnPointAvailable.getRandomElement();
            battleSpawningPoint.actor = enemy;
            battleSpawningPoint.actorGameObject = GameObject.Instantiate(enemy.model, battleSpawningPoint.transform);
        }
    }

    public List<BattleSpawningPoint> getCharacterSpawningPoints() { return _characterSpawningPoints; }
    public List<BattleSpawningPoint> getEnemySpawningPoints() { return _enemySpawningPoints; }

    public List<Being> getCharacters() { return _characterSpawningPoints.FindAll(x => x.actor != null).Select(x => x.actor).ToList(); }
    public List<Being> getEnemy() { return _enemySpawningPoints.FindAll(x => x.actor != null).Select(x => x.actor).ToList(); }

    public BattleSpawningPoint getSpawningPoint(Being being)
    {
        List<BattleSpawningPoint> spawns = new List<BattleSpawningPoint>().join(_characterSpawningPoints, _enemySpawningPoints);
        return spawns.Find(x => x.actor == being);
    }

}
