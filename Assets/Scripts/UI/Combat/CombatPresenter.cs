using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Combat
{
    public class CombatPresenter
    {
        private ICombatView _view;

        private IRoundManager _roundManager = new RoundManager();
        public ICombatManager CombatManager { get; private set; }

        private List<ICell> _selectedCells = new List<ICell>();

        private ICell _selectedCell;

        private UnitStateAction _unitAction;

        private int _abilityIndex;

        public CombatPresenter(ICombatView view, GameConfig gameConfig, List<IPlayer> players)
        {
            _view = view;   

            Field field = new Field(gameConfig.FieldWidth, gameConfig.FieldHeight);

            CombatManager = new CombatManager(field, players, _roundManager);

            CombatManager.OnCurrentUnitChanged += CurrentUnitChanged;

            _view.AbilityButton.onClick.AddListener(UnitUseAbility);
            _view.MoveButton.onClick.AddListener(UnitMove);
            _view.SkipButton.onClick.AddListener(UnitSkipTurn);
            _view.QuitButton.onClick.AddListener(QuitGame);

            _view.AbilityBoardView.OnAbilityChanged += ChangeAbilityIndex;

            CombatManager.RoundManager.OnRoundChanged += RoundChanged;
            CombatManager.OnPlayerLose += PlayerLose;

            SubscribeCells();

            ClearInfoBoard();

            StartFirstRound();
        }

        private void SubscribeCells()
        {
            for (int x = 0; x < _view.CellsViews.GetLength(0); x++)
            {
                for (int y = 0; y < _view.CellsViews.GetLength(1); y++)
                {
                    _view.CellsViews[x, y].OnClicked += CellClicked;
                    _view.CellsViews[x, y].OnMouseEntered += CellMouseEntered;
                    CombatManager.GameField.Cells[x, y].OnModelChanged += _view.CellsViews[x, y].ChangeModel;
                    CombatManager.GameField.Cells[x, y].OnDeselected += _view.CellsViews[x, y].Deselect;
                    CombatManager.GameField.Cells[x, y].OnSelected += _view.CellsViews[x, y].Select;
                }
            }
        }

        public void StartFirstRound()
        {
            for (int i = 0; i < CombatManager.Players.Count; i++)
            {
                for (int j = 0; j < CombatManager.Players[i].ControlledUnits.Count; j++)
                {
                    int x = i * (CombatManager.GameField.Width - 1);
                    CombatManager.GameField.AddUnit(CombatManager.Players[i].ControlledUnits[j], x, j);
                }
            }

            CombatManager.StartGame();
        }

        public void RoundChanged(object sender, int round)
        {
            _view.RoundTitle.text = "Round: " + round.ToString();
        }

        public void CellClicked(int x, int y)
        {
            _selectedCell = CombatManager.GameField.Cells[x, y];

            switch (_unitAction)
            {
                case UnitStateAction.None:
                    break;
                case UnitStateAction.Ability:
                    if (CombatManager.CurrentUnit.TryUseActiveAbility(CombatManager.CurrentUnit.Stats.ActiveAbilities[_abilityIndex], _selectedCell, CombatManager.GameField))
                    {
                        DeselectCells(_selectedCells);
                        CombatManager.CurrentUnit.CellParent.Select();
                        _unitAction = UnitStateAction.None;
                    }
                    break;
                case UnitStateAction.Move:
                    if (CombatManager.CurrentUnit.TryMove(_selectedCell, CombatManager.GameField))
                    {
                        DeselectCells(_selectedCells);
                        CombatManager.CurrentUnit.CellParent.Select();
                        _view.MoveButton.interactable = false;
                        _view.MoveButton.GetComponent<Image>().color = Color.gray;
                        _unitAction = UnitStateAction.None;
                    }
                    break;
                default:
                    break;
            }
        }

        public void CellMouseEntered(int x, int y)
        {
            IUnit unit = CombatManager.GameField.Cells[x, y].Unit;
            if (unit == null)
            {
                ClearInfoBoard();
                return;
            }

            UpdateInfoBoard(unit.Stats);
        }

        private void ClearInfoBoard()
        {
            _view.InfoBoardView.UnitNameText.text = "None";
            _view.InfoBoardView.HealthUI.text = "0/0";
            _view.InfoBoardView.ArmorUI.text = "0/0";
            _view.InfoBoardView.PowerUI.text = "0";
            _view.InfoBoardView.EnergyUI.text = "0/0";
            _view.InfoBoardView.DistanceMoveUI.text = "0";
            _view.InfoBoardView.InitiativeUI.text = "0";
            _view.InfoBoardView.DescriptionUI.text = "";
        }

        private void UpdateInfoBoard(IUnitStats stats)
        {
            _view.InfoBoardView.UnitNameText.text = stats.Name;
            _view.InfoBoardView.HealthUI.text = $"{stats.CurrentHealth:#.#}/{stats.MaxHealth:#.#}";
            _view.InfoBoardView.ArmorUI.text = $"{stats.Armor:#.#}/{stats.MaxArmor:#.#}";
            _view.InfoBoardView.PowerUI.text = $"{stats.Power:#.#}";
            _view.InfoBoardView.EnergyUI.text = $"{stats.CurrentEnergy:#.#}/{stats.MaxEnergy:#.#}";
            _view.InfoBoardView.DistanceMoveUI.text = $"{stats.DistanceOfMove:#.#}";
            _view.InfoBoardView.InitiativeUI.text = $"{stats.Initiative:#.#}";
            _view.InfoBoardView.DescriptionUI.text = $"{stats.Description:#.#}";
        }

        private void CurrentUnitChanged(object sender, EventArgs eventArgs)
        {
            CombatManager.CurrentUnit.CellParent.Select();

            _view.AbilityBoardView.Hide();

            DeselectCells(_selectedCells);
            _selectedCells.Clear();

            _view.MoveButton.interactable = true;
            _view.MoveButton.GetComponent<Image>().color = Color.white;

            _view.PlayerTitle.text = "Turn: " + CombatManager.CurrentPlayer.Name;
        }

        public void PlayerLose(object sender, IPlayer player)
        {
            _view.EndGameView.Show();
            _view.EndGameView.Init(player.Name);
        }

        public void UnitSkipTurn()
        {
            _unitAction = UnitStateAction.None;
            _view.AbilityBoardView.Hide();
            CombatManager.CurrentUnit.SkipTurn();
        }

        public void UnitUseAbility()
        {
            DeselectCells(_selectedCells);

            CombatManager.CurrentUnit.CellParent.Select();

            _view.AbilityBoardView.Init(CombatManager.CurrentUnit.Stats.ActiveAbilities);
            _view.AbilityBoardView.Show();
        }

        public void UnitMove()
        {
            CombatManager.CurrentUnit.CellParent.Deselect();
            DeselectCells(_selectedCells);


            _unitAction = UnitStateAction.Move;

            _view.AbilityBoardView.Hide();

            _selectedCells = CombatManager.GameField.GetNeighborsRadius(CombatManager.CurrentUnit.CellParent, 
                CombatManager.CurrentUnit.Stats.DistanceOfMove);

            SelectCells(_selectedCells);
        }

        private void SelectCells(List<ICell> cells)
        {
            foreach (ICell cell in cells) cell.Select();
        }

        private void DeselectCells(List<ICell> cells)
        {
            foreach (ICell cell in cells) cell.Deselect();
        }

        private void ChangeAbilityIndex(object sender, int index)
        {
            _unitAction = UnitStateAction.Ability;

            _abilityIndex = index;

            DeselectCells(_selectedCells);

            CombatManager.CurrentUnit.CellParent.Deselect();

            _selectedCells = CombatManager.GameField.GetNeighborsRadius(CombatManager.CurrentUnit.CellParent,
                CombatManager.CurrentUnit.Stats.ActiveAbilities[_abilityIndex].Range);

            SelectCells(_selectedCells);
        }

        private void QuitGame()
        {
            Application.Quit();
        }
    }

    public enum UnitStateAction
    {
        None, Ability, Move
    }
}
