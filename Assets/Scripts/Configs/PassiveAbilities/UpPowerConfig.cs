using Assets.Scripts.Configs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpPowerConfig", menuName = "ScriptableObjects/Configs/PassiveAbilities/UpPower")]
public class UpPowerConfig : PassiveAbilityConfig
{
    public override void Execute(IUnit unit)
    {
        unit.Stats.Power = unit.Stats.Power * Coefficient;
    }
}
