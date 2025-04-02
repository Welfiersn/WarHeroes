using System;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public abstract class AbstractUnit : IUnit
{
    public int ownerId { get; protected set; }
    public IUnitStats Stats { get; set; }

    public ICell CellParent { get; set; }

    public event EventHandler<IUnit> OnDead;

    public event EventHandler<IUnit> OnTurnCompleted;

    private bool _isMoved = false;

    public bool TryUseActiveAbility(IActiveAbility ability, ICell cell, IField field)
    {
        if (cell == CellParent) return false;
        if (Stats.CurrentEnergy < ability.Cost) return false;

        var cellsInRange = field.GetNeighborsRadius(CellParent, ability.Range);
        if (!cellsInRange.Contains(cell))
            return false;

        _isMoved = false;
        if (cell.Unit != null)
        {
            decimal amount = ability.Execute(Stats.Power);

            if (!IsValidTarget(cell.Unit, amount)) return false;

            if (amount < 0) cell.Unit.ApplyDamage(amount);
            if (amount > 0) cell.Unit.ApplyHealth(amount);

            if (Stats.CurrentEnergy - ability.Cost <= 0) Stats.CurrentEnergy = 0;
            else Stats.CurrentEnergy -= ability.Cost;

            OnTurnCompleted?.Invoke(this, this);

            return true;
        }


        return false;
    }

    public bool TryMove(ICell cell, IField field)
    {
        if (_isMoved == true) return false; ;
        if (cell?.Unit != null) return false;
        if (cell?.Obstacle != null) return false;

        if (field.GetNeighborsRadius(CellParent, Stats.DistanceOfMove).Contains(cell))
        {
            CellParent.Deselect();
            CellParent.Unit = null;
            cell.Unit = this;

            _isMoved = true;
            return true;
        }

        return false;
    }

    public void ApplyDamage(decimal damageAmount)
    {
        decimal multiplier = (decimal)(1 - Stats.Armor / 100d);
        if (Stats.CurrentHealth + (multiplier * damageAmount) <= 0)
        {
            Die();
            return;
        }

        Stats.CurrentHealth += damageAmount * multiplier;
    }

    public void ApplyHealth(decimal healthAmount)
    {
        if (Stats.CurrentHealth + healthAmount >= Stats.MaxHealth)
        {
            Stats.CurrentHealth = Stats.MaxHealth;
            return;
        }

        Stats.CurrentHealth += healthAmount;
    }

    public void Die()
    {
        CellParent.Unit = null;

        OnDead?.Invoke(this, this);
    }

    public void SkipTurn()
    {

        if (_isMoved == false)
        {
            if (Stats.CurrentEnergy + Stats.MaxEnergy * (decimal)0.2 >= Stats.MaxEnergy) { Stats.CurrentEnergy = Stats.MaxEnergy; }
            else { Stats.CurrentEnergy += Stats.MaxEnergy * (decimal)0.2; }
        }
        else
        {
            _isMoved = false;
        }

        OnTurnCompleted?.Invoke(this, this);
    }

    public void ApplyPassiveAbilities()
    {
        if (Stats.PassiveAbilities == null) return;

        foreach (IPassiveAbility passiveAbility in Stats.PassiveAbilities)
        {
            passiveAbility.Execute(this);
        }
    }

    protected bool IsValidTarget(IUnit target, decimal effectAmount)
    {
        // Если эффект отрицательный — это урон, атаковать можно только врагов
        if (effectAmount < 0 && target.ownerId == this.ownerId)
        {
            return false;
        }
        // Если эффект положительный — это лечение, лечить можно только союзников
        if (effectAmount > 0 && target.ownerId != this.ownerId)
        {
            return false;
        }
        return true;
    }
}

