using Assets.Scripts.Models.Units;

public class Unit : AbstractUnit
{
    public Unit(IUnitStats unitStats, int id)
    {
        Stats = new BaseUnitStats(unitStats);
        ownerId = id;
    }
}