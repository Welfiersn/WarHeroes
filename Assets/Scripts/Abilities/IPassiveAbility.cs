using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IPassiveAbility : IAbility
{
    public void Execute(IUnit unit);
}

