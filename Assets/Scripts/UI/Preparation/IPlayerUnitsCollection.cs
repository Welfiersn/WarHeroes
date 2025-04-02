using System;
using System.Collections.Generic;

namespace Assets.Scripts.UI
{
    public interface IPlayerUnitsCollection
    {
        public List<IUnit> Warriors { get; }
        public List<IUnit> Archers { get; }
        public List<IUnit> Mages { get; }

        public void AddUnit(TypeRole role);
        public void RemoveUnit(TypeRole role);

        public event EventHandler<int> OnWarriorAmountChanged;
        public event EventHandler<int> OnArcherAmountChanged;
        public event EventHandler<int> OnMageAmountChanged;
    }
}
