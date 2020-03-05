using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace RPG.Battle.UI
{
    public class BattleOrderDisplayer : MonoBehaviour
    {
        [SerializeField] Transform _battleOrderList;
        [SerializeField] GameObject _battleOrderItemPrefab;
        [SerializeField] GameObject _selectionArrow;

        List<BattleOrderActorDisplayer> _actors = new List<BattleOrderActorDisplayer>();
        List<BattleTarget> _battleTargets = new List<BattleTarget>();

        public void displayOrderList(BattleOrder battleData)
        {
            _selectionArrow.SetActive(false);
            _battleOrderList.clearChild();
            battleData.current.ForEach(x =>
            {
                GameObject gameObject = Instantiate(_battleOrderItemPrefab, _battleOrderList);
                BattleOrderActorDisplayer battleOrderActorDisplayer = gameObject.GetComponent<BattleOrderActorDisplayer>();
                battleOrderActorDisplayer.set(x.icon, x.name);
                _actors.Add(battleOrderActorDisplayer);
            });
        }
    }
}
