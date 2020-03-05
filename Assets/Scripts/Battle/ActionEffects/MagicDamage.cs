using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = Path.ACTION_EFFECT_SO_MENU_NAME + "MagicDamage")]
public class MagicDamage: ActionEffect
{
    [Header("Attributs"), SerializeField, EnumToggleButtons]
    DamageType damageType;

    public override bool execute(Being sender, Being target)
    {
        target.damage(sender.magicAttack);
        return true;
    }
}
