using Assets.Scripts.Configs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Scripting;

[CreateAssetMenu(fileName = "UnitsConfig", menuName = "ScriptableObjects/Configs/UnitsConfig")]
public class UnitsConfig : ScriptableObject
{
    [field : SerializeField] public UnitConfig[] Humans {  get; private set; }
    [field: SerializeField] public UnitConfig[] Orcs { get; private set; }
    [field: SerializeField] public UnitConfig[] Undeads { get; private set; }

    public List<UnitConfig> AllUnitsConfigs => Humans.Concat(Orcs).Concat(Undeads).ToList();

    public Dictionary<int, UnitConfig> UnitConfigDictionary
    {
        get
        {
            Dictionary<int, UnitConfig> keyValuePairs = new Dictionary<int, UnitConfig>();

            foreach (var pair in AllUnitsConfigs)
            {
                keyValuePairs.Add(pair.ID, pair);
            }

            return keyValuePairs;
        }
    }

    public UnitConfig[] GetByFaction(TypeFaction typeFaction)
    {
        return typeFaction switch
        {
            TypeFaction.HUMANS => Humans,
            TypeFaction.ORCS => Orcs,
            TypeFaction.UNDEADS => Undeads,
            _ => null,
        };
    }
}

[Serializable]
public class UnitConfig : IUnitStats
{
    [field: SerializeField] public int ID { get; set; }

    [field: SerializeField] public string Name { get; set; }

    [field: SerializeField, TextArea] public string Description { get; set; }

    [SerializeField] private double _maxHealth;
    public decimal MaxHealth { get => (decimal)_maxHealth; set { } }

    public decimal CurrentHealth { get; set; }

    [SerializeField] private double _power;
    public decimal Power { get => (decimal)_power; set { } }

    [field: SerializeField] public int MaxArmor { get; set; }
    [field: SerializeField] public int Armor { get; set; }

    [field: SerializeField] public int DistanceOfMove { get; set; }
    [field: SerializeField] public int Initiative { get; set; }

    [SerializeField] private double _maxEnergy;
    public decimal MaxEnergy { get => (decimal)_maxEnergy; set { } }

    public decimal CurrentEnergy { get; set; }

    public UnitAbilitiesConfig AbilitiesConfig;
    public List<IPassiveAbility> PassiveAbilities { get => AbilitiesConfig.PassiveAbilities; set { } }
    public List<IActiveAbility> ActiveAbilities { get => AbilitiesConfig.ActiveAbilities; set { } }
}
