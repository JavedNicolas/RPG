using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;

[CreateAssetMenu(menuName = Path.ACTION_SO_MENU_NAME)]
public class Action : DatabaseElement, IBattleAction
{
    [SerializeField, EnumToggleButtons] ActionCategory _category;
    [SerializeField] string _localizationKey;
    [SerializeField] bool _canByPassFront;
    [SerializeField, EnumToggleButtons] ActionAnimationType _animationType;
    [SerializeField] List<ActionEffect> _effects = new List<ActionEffect>();

    #region delegate
    public delegate void ActionHasBeenChosen(Action action);
    public ActionHasBeenChosen actionHasBeenChosen;
    #endregion

    public bool execute(Being sender, Being target)
    {
        if (_effects == null || _effects.Count == 0)
            return false;

        _effects.ForEach(x => x.execute(sender, target));

        return true;
    }

    public string getLocalisationKey()
    {
        return _localizationKey;
    }

    public void fireDelegate()
    {
        actionHasBeenChosen(this);
    }

    public ActionCategory getCategory() { return _category; }
    public bool canByPassFrontSlot() { return _canByPassFront; }
    public ActionAnimationType GetAnimationType() { return _animationType; }
}
