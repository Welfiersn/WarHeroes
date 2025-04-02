using Assets.Scripts.Abilities.ActiveAbilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models.Units
{
    internal class BaseUnitStats : IUnitStats
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal MaxHealth { get; set; }
        public decimal CurrentHealth { get; set; }
        public decimal MaxEnergy { get; set; }
        public decimal CurrentEnergy { get; set; }
        public decimal Power { get; set; }
        public int MaxArmor { get; set; }
        public int Armor { get; set; }
        public int DistanceOfMove { get; set; }
        public int Initiative { get; set; }
        public string Description { get; set; }
        public List<IPassiveAbility> PassiveAbilities { get; set; } = new List<IPassiveAbility>();
        public List<IActiveAbility> ActiveAbilities { get; set ; } = new List<IActiveAbility>();

        public BaseUnitStats(IUnitStats unitStats)
        {
            ID = unitStats.ID;
            Name = unitStats.Name;
            MaxHealth = unitStats.MaxHealth;
            CurrentHealth = unitStats.MaxHealth;
            MaxEnergy = unitStats.MaxEnergy;
            CurrentEnergy = unitStats.MaxEnergy;
            Power = unitStats.Power;
            MaxArmor = unitStats.MaxArmor;
            Armor = unitStats.Armor;
            DistanceOfMove = unitStats.DistanceOfMove;
            Initiative = unitStats.Initiative;
            Description = unitStats.Description;
            PassiveAbilities = unitStats.PassiveAbilities;
            ActiveAbilities = unitStats.ActiveAbilities;
        }
    }
}
