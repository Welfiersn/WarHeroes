using Assets.Scripts.Configs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RegenerationConfig", menuName = "ScriptableObjects/Configs/PassiveAbilities/Regeneration")]
public class RegenerationConfig : PassiveAbilityConfig
{
    public override void Execute(IUnit unit)
    {
        if (unit.Stats.Power * Coefficient + unit.Stats.CurrentHealth >= unit.Stats.MaxHealth) { unit.Stats.CurrentHealth = unit.Stats.MaxHealth; }
        else { unit.Stats.CurrentHealth = unit.Stats.Power * Coefficient + unit.Stats.CurrentHealth; }
    }
}