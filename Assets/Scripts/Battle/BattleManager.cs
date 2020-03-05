using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] BattleMenu _battleMenu;
    [SerializeField] BattleTargetSelector _targetSelector;
    [SerializeField] BattleOrderDisplayer battleOrderDisplayer;

    [Header("Battle")]
    [SerializeField] BattleStateManager _battleStateManager;
    [SerializeField] BattleActorSpawner _battleActorSpawner;

    private void Start()
    {
        _battleActorSpawner.spawnActors();
        _battleStateManager.startBattle(_battleActorSpawner.getCharacters(), _battleActorSpawner.getEnemy(), this);
        _battleMenu.actionHasBeenChosen = setTargetSelector;
        _targetSelector.targetHasBeenChosen = useActionOnTarget;

        InvokeRepeating("updateMenu", 1f, 1f);
        InvokeRepeating("displayBattleOrder", 0f, 1f);
    }

    public void displayBattleOrder()
    {

    }

    public void displayMenu(bool display)
    {
        _battleMenu.displayMenu(display);
    }

    public void resetMenu()
    {
        _battleMenu.resetMenu();
        _battleMenu.displayMenu(false);
    }

    private void updateMenu()
    {
        _battleMenu.updateBattleMenu(ActorTurnBattleState.currentActor as Character);
    }

    public void cancelTargeting()
    {
        displayMenu(true);
    }

    public void useActionOnTarget(BattleTarget target)
    {
        _battleStateManager.stateSwitcher.currentBattleState.useAction(target, getActorSpawingPoint(ActorTurnBattleState.currentActor));
    }

    private void setTargetSelector(Action action)
    {
        ActorTurnBattleState.actionInUse = action;
        displayMenu(false);
        // generate a list with the valid targets
        List<BattleTarget> targets = getValidTarget(_battleActorSpawner.getEnemySpawningPoints(), action);

        // hide the battle menu and start the selection
        _targetSelector.init(targets);
    }

    /// <summary> get the relevant target based on the action </summary>
    /// <param name="spawner"> a list of all the spawning point </param>
    /// <param name="action">The action that we search a target for </param>
    /// <returns></returns>
    protected List<BattleTarget> getValidTarget(List<BattleSpawningPoint> spawner, Action action)
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

    public List<BattleTarget> getCharacterValidtargetPoints(Action action) { return getValidTarget(_battleActorSpawner.getCharacterSpawningPoints(), action); }
    public BattleSpawningPoint getActorSpawingPoint(Being actor) { return _battleActorSpawner.getSpawningPoint(actor); } 

}
