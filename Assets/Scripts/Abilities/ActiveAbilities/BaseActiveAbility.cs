using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Abilities.ActiveAbilities
{
    public class BaseActiveAbility : AbstractActiveAbility
    {
        public BaseActiveAbility(string name, int cost, string description, decimal coefficient, bool isHeal, int range) 
            : base(name, cost, description, coefficient, isHeal, range)
        {

        }
    }
}
