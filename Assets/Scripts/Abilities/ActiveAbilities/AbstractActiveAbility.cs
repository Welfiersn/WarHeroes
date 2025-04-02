using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Abilities.ActiveAbilities
{
    public abstract class AbstractActiveAbility : IActiveAbility
    {
        private int _cost;
        private string _description;
        private int _range;
        private decimal _coefficient;
        private string _name;

        public decimal Coefficient
        {
            get { return _coefficient; }
            set { if (value < 0) throw new ArgumentOutOfRangeException("Coefficient не может быть отрицательным"); _coefficient = value; }
        }

        public bool IsHeal { get; set; }

        public string Description
        {
            get { return _description; }
            set { if (string.IsNullOrEmpty(value)) throw new ArgumentException("Description не может быть null или empty"); _description = value; }
        }

        public int Cost
        {
            get { return _cost; }
            set { if (value < 0) throw new ArgumentOutOfRangeException("Cost не может быть отрицательным"); _cost = value; }
        }

        public int Range
        {
            get { return _range; }
            set { if (value < 0) throw new ArgumentOutOfRangeException("Range не может быть отрицательным"); _range = value; }
        }

        public string Name
        {
            get { return _name; }
            set { if (string.IsNullOrEmpty(value)) throw new ArgumentException("Name не может быть null или empty"); _name = value; }
        }


        public AbstractActiveAbility(string name, int cost, string description, decimal coefficient, bool isHeal, int range)
        {
            Name = name;
            Range = range;
            Description = description;
            Cost = cost;
            Coefficient = coefficient;
            IsHeal = isHeal;
        }

        public decimal Execute(decimal power)
        {
           if (IsHeal == true) return power * Coefficient;
           return -power * Coefficient;
        }
    }
}
