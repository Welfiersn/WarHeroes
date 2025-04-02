using Assets.Scripts.Abilities.ActiveAbilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Configs
{
    [CreateAssetMenu(fileName = "ActiveAbilityConfig", menuName = "ScriptableObjects/Configs/ActiveAbilityConfig")]
    [Serializable]
    public class ActiveAbilityConfig : ScriptableObject, IActiveAbility
    {
        [field: SerializeField] public string Name { get; set; }
        [SerializeField] private double _coefficient;
        public decimal Coefficient { get => (decimal)_coefficient; set { } }
        [field: SerializeField] public string Description { get; set; }
        [field: SerializeField] public int Cost { get; set; }
        [field: SerializeField] public int Range { get; set; }
        [field: SerializeField] public bool IsHeal { get; set; }

        public decimal Execute(decimal power)
        {
            if (IsHeal == true) return power * Coefficient;
            return -power * Coefficient;
        }
    }
}
