using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IActiveAbility: IAbility
{
    public bool IsHeal { get; set; }
    public int Range { get; set; }
    public int Cost { get; set; }
    public decimal Execute(decimal power);
}

