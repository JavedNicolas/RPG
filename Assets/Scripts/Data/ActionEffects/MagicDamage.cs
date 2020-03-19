using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace RPG.Data
{
    [CreateAssetMenu(menuName = AssetsPath.ACTION_EFFECT_SO_MENU_NAME + "MagicDamage")]
    public class MagicDamage : ActionEffect
    {
        [Header("Attributs"), SerializeField, EnumToggleButtons]
        DamageType damageType;

        public override bool execute(Being sender, Being target)
        {
            target.damage(sender.magicAttack);
            return true;
        }
    }
}
