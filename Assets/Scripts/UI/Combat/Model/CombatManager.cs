using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

public class CombatManager : ICombatManager
{
    private IRoundManager _roundManager;

    private IField _gameField;

    private List<IPlayer> _players;

    public IPlayer CurrentPlayer { get; set; }
    private IUnit _currentUnit;
    public IUnit CurrentUnit
    {
        get { return _currentUnit; }
        set
        {
            _currentUnit?.CellParent.Deselect();
            _currentUnit = value;
            _currentUnit?.CellParent.Select();
            OnCurrentUnitChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    
    public List<IUnit> UnitsCanTakeAction { get; set; } = new List<IUnit>();
    public PriorityQueue<IUnit, int> UnitsPriorityQueue { get; set; } = new PriorityQueue<IUnit, int>();
    
    public List<IUnit> AllUnits
    {
        get
        {
            List<IUnit> result = new List<IUnit>();

            foreach (IPlayer player in Players)
            {
                foreach (IUnit unit in player.ControlledUnits)
                {
                    result.Add(unit);
                }
            }

            return result;
        }
    }

    public event EventHandler<IPlayer> OnPlayerLose;
    public event EventHandler OnCurrentUnitChanged;


    public IField GameField
    {
        get { return _gameField; }
        set { _gameField = value; }
    }

    public List<IPlayer> Players
    {
        get { return _players; }
        set { _players = value; }
    }

    public IRoundManager RoundManager 
    {
        get { return _roundManager; }
        set { _roundManager = value; } 
    }

    public CombatManager(IField gameField, List<IPlayer> players, IRoundManager roundManager)
    {
        GameField = gameField;
        Players = players;
        RoundManager = roundManager;
        SubscribeUnitEvent();
        OnCurrentUnitChanged += ChangeCurrentPlayer;
    }

    public void ChangeCurrentPlayer(object sender, EventArgs eventArgs)
    {
        foreach (IPlayer player in _players)
        {
            if (player.ControlledUnits.Contains(CurrentUnit)) CurrentPlayer = player;
        }
    }

    public void ChangeUnitsCanTakeAction()
    {  
        UnitsCanTakeAction.Clear();

        foreach (IUnit unit in AllUnits)
        {
            UnitsCanTakeAction.Add(unit);
        }
    }

    public void RebuildQueue()
    {
        UnitsPriorityQueue.Clear();

        foreach (IUnit unit in UnitsCanTakeAction)
        {
            UnitsPriorityQueue.Enqueue(unit, -unit.Stats.Initiative);
        }
    }

    public void SubscribeUnitEvent() {
        foreach (IUnit unit in AllUnits) {
            unit.OnTurnCompleted += NextTurn;
            unit.OnDead += RemoveDeadUnitFromField;
        }
    }


    public void NextTurn(object sender, IUnit args)
    {
        foreach (IPlayer player in _players)
        {
            if (player.ControlledUnits.Count == 0)
            {
                OnPlayerLose.Invoke(this, player);
                return;
            }
        }

        if (UnitsPriorityQueue.Count == 0)
        {
            RoundManager.NextRound();

            ChangeUnitsCanTakeAction();

            RebuildQueue();

            ApplyAllPassiveAbilities();
        }

        UnitsCanTakeAction.Remove(CurrentUnit);
        CurrentUnit = UnitsPriorityQueue.Dequeue();
    }

    public void StartGame()
    {
        foreach (IPlayer player in _players)
        {
            if (player.ControlledUnits.Count == 0)
            {
                OnPlayerLose.Invoke(this, player);
                return;
            }
        }

        RoundManager.NextRound();

        ChangeUnitsCanTakeAction();

        RebuildQueue();

        ApplyAllPassiveAbilities();

        UnitsCanTakeAction.Remove(CurrentUnit);
        CurrentUnit = UnitsPriorityQueue.Dequeue();
    }

    public void RemoveDeadUnitFromField(object sender, IUnit unit) {
        foreach (IPlayer player in Players)
        {
            if (player.ControlledUnits.Contains(unit))
            {
                player.ControlledUnits.Remove(unit);
                if (player.ControlledUnits.Count == 0)
                {
                    OnPlayerLose.Invoke(this, player);
                    return;
                }
                else {
                    unit.OnDead -= RemoveDeadUnitFromField;
                    unit.OnTurnCompleted -= NextTurn;

                    if (UnitsCanTakeAction.Contains(unit)) 
                    {
                        UnitsCanTakeAction.Remove(unit); 
                    }

                    RebuildQueue();
                    return;
                }
            }
        }
    }

    private void ApplyAllPassiveAbilities() {
        foreach (IUnit unit in AllUnits) {
            unit.ApplyPassiveAbilities();
        }
    }
}

