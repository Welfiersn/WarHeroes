using Assets.Scripts.Abilities.ActiveAbilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Configs
{
    [Serializable]
    public abstract class PassiveAbilityConfig : ScriptableObject, IPassiveAbility
    {
        [field: SerializeField] public string Name { get; set; }

        [field: SerializeField] public string Description { get; set; }

        [SerializeField] private double _coefficient;
        public decimal Coefficient { get => (decimal)_coefficient; set { } }

        public abstract void Execute(IUnit unit);
    }
}
