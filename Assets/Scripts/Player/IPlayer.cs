
using System.Collections.Generic;

public interface IPlayer
{
    public string Name { get; set; }

    public List<IUnit> ControlledUnits { get; set; }

}
