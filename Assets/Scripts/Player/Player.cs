using System;
using System.Collections.Generic;

public class Player : IPlayer
{
    private string _name;

    public int Id { get; private set; }

    private List<IUnit> _controlledUnits;

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public List<IUnit> ControlledUnits
    {
        get { return _controlledUnits; }
        set { _controlledUnits = value; }
    }

    public Player(string name, List<IUnit> units, int id)
    {
        Name = name;
        ControlledUnits = units;
        Id = id;
    }
}
