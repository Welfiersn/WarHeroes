using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FactoriesUnitsFactory
{
    public IAbstractFactoryUnits CreateFactory(TypeFaction typeFaction) => typeFaction switch
    {
        TypeFaction.HUMANS => new HumansFactoryUnits(),
        TypeFaction.ORCS => new OrcsFactoryUnits(),
        TypeFaction.UNDEADS => new UndeadsFactoryUnits(),
        _ => throw new NotImplementedException("Такой фабрики не существует"),
    };
}

