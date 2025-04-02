using Assets.Scripts.Configs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpArmorConfig", menuName = "ScriptableObjects/Configs/PassiveAbilities/UpArmor")]
public class UpArmorConfig : PassiveAbilityConfig
{
    public override void Execute(IUnit unit)
    {
        if (unit.Stats.Armor + (int)Coefficient >= unit.Stats.MaxArmor) { unit.Stats.Armor = unit.Stats.MaxArmor; }
        else { unit.Stats.Armor += (int)Coefficient; }
    }
}
