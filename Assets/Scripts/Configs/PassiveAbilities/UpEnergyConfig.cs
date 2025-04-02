using Assets.Scripts.Configs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpEnergyConfig", menuName = "ScriptableObjects/Configs/PassiveAbilities/UpEnergy")]
public class UpEnergyConfig : PassiveAbilityConfig
{
    public override void Execute(IUnit unit)
    {
        if (unit.Stats.CurrentEnergy + unit.Stats.MaxEnergy * Coefficient >= unit.Stats.MaxEnergy) { unit.Stats.CurrentEnergy = unit.Stats.MaxEnergy; }
        else { unit.Stats.CurrentEnergy += unit.Stats.MaxEnergy * Coefficient; }
    }
}
