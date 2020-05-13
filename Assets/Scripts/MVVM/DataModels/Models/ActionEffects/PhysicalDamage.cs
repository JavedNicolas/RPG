using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace RPG.DataModule
{
    using RPG.GlobalModule;

    [CreateAssetMenu(menuName = AssetsPath.ACTION_EFFECT_SO_MENU_NAME + "PhysicalDamage")]
    public class PhysicalDamage : ActionEffect
    {
        [Header("Attributs"), SerializeField, EnumToggleButtons]
        DamageType damageType;


        public override bool execute(Being sender, Being target)
        {
            target.damage(sender.attack);
            return true;
        }
    }
}