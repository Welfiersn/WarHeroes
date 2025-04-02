using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    class UndeadsFactoryUnits : IAbstractFactoryUnits
    {
        public AbstractUnit CreateArcher(IUnitStats unitStats, int ownerId)
        {
        return new Unit(unitStats, ownerId);
    }

        public AbstractUnit CreateMage(IUnitStats unitStats, int ownerId)
        {
        return new Unit(unitStats, ownerId);
    }

        public AbstractUnit CreateWarrior(IUnitStats unitStats, int ownerId)
        {
        return new Unit(unitStats, ownerId);
    }
    }

