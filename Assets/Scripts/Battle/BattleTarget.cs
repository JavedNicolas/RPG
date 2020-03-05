using UnityEngine;
using UnityEngine.UI;

public struct BattleTarget
{
    public Button button;
    public GameObject model;
    public Being being;

    /// <summary> Generate a List of target  </summary>
    /// <param name="spawningPoint"></param>
    /// <returns></returns>
    public BattleTarget(BattleSpawningPoint spawningPoint)
    {
        button = spawningPoint.GetComponentInChildren<Button>();
        model = spawningPoint.actorGameObject;
        being = spawningPoint.actor;
    }
}
