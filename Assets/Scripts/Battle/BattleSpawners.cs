﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RPG.Battle
{
    using RPG.Data;
    using RPG.Data.Team;

    public class BattleSpawners : MonoBehaviour
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


        /// <summary> get the relevant target based on the action </summary>
        /// <param name="spawner"> a list of all the spawning point </param>
        /// <param name="action">The action that we search a target for </param>
        /// <returns></returns>
        public List<BattleTarget> getValidTargets(ActorType actorType, Action action)
        {
            List<BattleSpawningPoint> spawns = getSpawns(actorType);

            List<BattleSpawningPoint> spawnsWithBeing = spawns.FindAll(x => x.actorGameObject != null && x.actor != null && !x.actor.isDead());
            List<BattleTarget> validTargets = new List<BattleTarget>();
            if (!action.canByPassFrontSlot())
                spawnsWithBeing.ForEach(x =>
                {
                    if (x.isFrontSpawn || !spawnsWithBeing.Exists(y => y.position == x.position && y.isFrontSpawn))
                        validTargets.Add(new BattleTarget(x));
                });

            return validTargets;
        }

        private List<BattleSpawningPoint> getSpawns(ActorType actorType)
        {
            List<BattleSpawningPoint> spawners = new List<BattleSpawningPoint>();

            switch (actorType)
            {
                case ActorType.Character: spawners.AddRange(_characterSpawningPoints); break;
                case ActorType.Enemy: spawners.AddRange(_enemySpawningPoints); break;
                case ActorType.All: spawners = new List<BattleSpawningPoint>().join(_characterSpawningPoints, _enemySpawningPoints); break;
            }

            return spawners;
        }

        public List<Being> getCharacters(bool includeDead = false)  { return getActors(_characterSpawningPoints, includeDead);  }
        public List<Being> getEnemies(bool includeDead = false)  { return getActors(_enemySpawningPoints, includeDead); }

        /// <summary> get all actor from the spawns</summary>
        /// <param name="spawns"></param>
        /// <param name="includeDead"> Include dead actors</param>
        /// <returns></returns>
        private List<Being> getActors(List<BattleSpawningPoint> spawns, bool includeDead)
        {
            return includeDead ?
                spawns.FindAll(x => x.actor != null).Select(x => x.actor).ToList() :
                spawns.FindAll(x => x.actor != null && !x.actor.isDead()).Select(x => x.actor).ToList();
        }

        public BattleSpawningPoint getSpawningPoint(Being being)
        {
            List<BattleSpawningPoint> spawns = new List<BattleSpawningPoint>().join(_characterSpawningPoints, _enemySpawningPoints);
            return spawns.Find(x => x.actor != null && x.actor.GetInstanceID() == being.GetInstanceID());
        }
    }
}
