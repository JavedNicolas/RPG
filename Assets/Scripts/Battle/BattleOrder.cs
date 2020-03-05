using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using RPG.DataManagement;


namespace RPG.Battle
{
    public class BattleOrder
    {
        List<Being> _battleOrders = new List<Being>();
        List<Being> _nextBattleOrders = new List<Being>();
        public List<Being> current => new List<Being>().join(_battleOrders, _nextBattleOrders);

        public void generateBattleOrder(params List<Being>[] actors)
        {
            List<Being> battleActors = new List<Being>().join(actors);
            _battleOrders = battleActors.OrderByDescending(x => x.speed).ToList();
        }

        public void checkForDead()
        {

        }

        public void updateBattleOrder()
        {
            // move the actor which played his turn in the next Battle order
            _nextBattleOrders.Add(_battleOrders.First());
            _battleOrders.RemoveAt(0);

            // if the _battleOrder is empty then when switch to the nextBattleOrder
            if (_battleOrders.Count == 0)
            {
                _battleOrders = new List<Being>(_nextBattleOrders);
                _nextBattleOrders = new List<Being>();
            }

            // reorder the lists
            _battleOrders = _battleOrders.OrderByDescending(x => x.speed).ToList();
            _nextBattleOrders = _nextBattleOrders.OrderByDescending(x => x.speed).ToList();
        }
    }
}
