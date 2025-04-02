using Assets.Scripts.Abilities.ActiveAbilities;
using Assets.Scripts.Models.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace Assets.Scripts.UI
{
    public class PlayerUnitsCollection : IPlayerUnitsCollection
    {
        private IAbstractFactoryUnits _factoryUnits;
        private IUnitStats[] _unitStats;
        private int _playerId;

        public List<IUnit> AllUnits => Warriors.Concat(Archers).Concat(Mages).ToList();

        private List<IUnit> _warriors = new List<IUnit>();
        public List<IUnit> Warriors => _warriors;

        private List<IUnit> _archers = new List<IUnit>();
        public List<IUnit> Archers => _archers;

        private List<IUnit> _mages = new List<IUnit>();
        public List<IUnit> Mages => _mages;


        public event EventHandler<int> OnWarriorAmountChanged;
        public event EventHandler<int> OnArcherAmountChanged;
        public event EventHandler<int> OnMageAmountChanged;


        public PlayerUnitsCollection(UnitsConfig unitsConfig, TypeFaction typeFaction, int id)
        {
            _unitStats = unitsConfig.GetByFaction(typeFaction);
            _factoryUnits = new FactoriesUnitsFactory().CreateFactory(typeFaction);
            _playerId = id;
        }

        public void AddUnit(TypeRole role)
        {
            switch (role)
            {
                case TypeRole.WARRIOR:
                    Warriors.Add(new Unit(new BaseUnitStats(_unitStats[(int)role]), _playerId));
                    OnWarriorAmountChanged?.Invoke(this, Warriors.Count);
                    break;
                case TypeRole.ARCHER:
                    Archers.Add(new Unit(new BaseUnitStats(_unitStats[(int)role]), _playerId));
                    OnArcherAmountChanged?.Invoke(this, Archers.Count);
                    break;
                case TypeRole.MAGE:
                    Mages.Add(new Unit(new BaseUnitStats(_unitStats[(int)role]), _playerId));
                    OnMageAmountChanged?.Invoke(this, Mages.Count);
                    break;
            }
        }

        public void RemoveUnit(TypeRole role)
        {
            switch (role)
            {
                case TypeRole.WARRIOR:
                    if (Warriors.Count < 1) return;
                    Warriors.RemoveAt(0);
                    OnWarriorAmountChanged?.Invoke(this, Warriors.Count);
                    break;
                case TypeRole.ARCHER:
                    if (Archers.Count < 1) return;
                    Archers.RemoveAt(0);
                    OnArcherAmountChanged?.Invoke(this, Archers.Count);
                    break;
                case TypeRole.MAGE:
                    if (Mages.Count < 1) return;
                    Mages.RemoveAt(0);
                    OnMageAmountChanged?.Invoke(this, Mages.Count);
                    break;
            }
        }
            
        public void SetPlayerId(int id)
        {
            _playerId = id;
        }
    }
}
