using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleTargetSelector : MonoBehaviour
{
    [SerializeField] EventSystem _eventSystem;

    INavigationSetter _navigationSetter;

    public delegate void TargetHasBeenChosen(BattleTarget target);
    public TargetHasBeenChosen targetHasBeenChosen;

    private void Awake()
    {
        _navigationSetter = GetComponent<INavigationSetter>();
    }

    private void Update()
    {
        if (Input.GetButton("Cancel"))
        {
            _eventSystem.SetSelectedGameObject(null);
        }
    }

    public void init(List<BattleTarget> targets)
    {
        _navigationSetter.setNavigation(targets.Select(x => x.button).ToList(), _eventSystem);

        targets.ForEach(x => x.button.onClick.AddListener(delegate {
            fireDelegate(x);
        }));

        _eventSystem.SetSelectedGameObject(targets.First().model);
    }

    public void fireDelegate(BattleTarget target)
    {
        _eventSystem.SetSelectedGameObject(null);
        targetHasBeenChosen(target);
    }

   

}
