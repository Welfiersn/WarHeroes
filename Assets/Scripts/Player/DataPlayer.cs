public class DataPlayer : IDataPlayer
{
    public string Name { get; set; }
    public TypeFaction Faction { get; set; }

    public DataPlayer(string name, TypeFaction faction)
    {
        Name = name;
        Faction = faction;
    }
}