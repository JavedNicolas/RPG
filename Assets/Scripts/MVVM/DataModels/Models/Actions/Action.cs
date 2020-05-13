using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace RPG.DataModule
{
    using RPG.GlobalModule;

    [CreateAssetMenu(menuName = AssetsPath.ACTION_SO_MENU_NAME)]
    public class Action : DatabaseElement, IAction
    {
        [SerializeField, EnumToggleButtons] ActionCategory _category;
        [SerializeField] string _localizationKey;
        [SerializeField] bool _canByPassFront;
        [SerializeField, EnumToggleButtons] ActionAnimationType _animationType;
        [SerializeField] int _cost;
        [HideInInspector] public int cost => _cost;
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
}
