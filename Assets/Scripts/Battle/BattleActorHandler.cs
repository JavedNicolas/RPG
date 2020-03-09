using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RPG.Battle
{
    using RPG.DataManagement;
    using RPG.DataManagement.Team;

    public class BattleActorHandler : MonoBehaviour
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

        public List<BattleTarget> getValidCharacterTargets(Action action)
        {
            return getValidTarget(_characterSpawningPoints, action);
        }

        public List<BattleTarget> getValidEnemyTargets(Action action)
        {
            return getValidTarget(_enemySpawningPoints, action);
        }

        /// <summary> get the relevant target based on the action </summary>
        /// <param name="spawner"> a list of all the spawning point </param>
        /// <param name="action">The action that we search a target for </param>
        /// <returns></returns>
        private List<BattleTarget> getValidTarget(List<BattleSpawningPoint> spawner, Action action)
        {
            List<BattleSpawningPoint> spawnsWithBeing = spawner.FindAll(x => x.actorGameObject != null && x.actor != null);
            List<BattleTarget> validTargets = new List<BattleTarget>();
            if (!action.canByPassFrontSlot())
                spawnsWithBeing.ForEach(x =>
                {
                    if (x.isFrontSpawn || !spawnsWithBeing.Exists(y => y.position == x.position && y.isFrontSpawn))
                        validTargets.Add(new BattleTarget(x));
                });

            return validTargets;
        }

        public List<Being> getCharacters() { return _characterSpawningPoints.FindAll(x => x.actor != null).Select(x => x.actor).ToList(); }
        public List<Being> getEnemies() { return _enemySpawningPoints.FindAll(x => x.actor != null).Select(x => x.actor).ToList(); }

        public BattleSpawningPoint getSpawningPoint(Being being)
        {
            List<BattleSpawningPoint> spawns = new List<BattleSpawningPoint>().join(_characterSpawningPoints, _enemySpawningPoints);
            return spawns.Find(x => x.actor != null && x.actor.GetInstanceID() == being.GetInstanceID());
        }
    }
}
