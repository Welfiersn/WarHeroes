using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Field : IField
{
    private ICell[,] _cells;
    public ICell[,] Cells => _cells;

    private int _width;
    private int _height;

    public int Width => _width;
    public int Height => _height;

    public Field(int fieldWidth, int fieldHeight)
    {
        _width = fieldWidth;
        _height = fieldHeight;

        _cells = new ICell[Width, Height];

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                ICell cell = new Cell();
                _cells[x, y] = cell;
            }
        }

        AddNeighborsAll();
    }

    public void AddUnit(IUnit unit, int x, int y)
    {
        if (_cells[x, y].Unit == null && _cells[x, y].Obstacle == null) _cells[x, y].Unit = unit;
        else 
        {
            throw new ArgumentException("Клетка уже занята");
        }
    }
    public void AddObstacle(IObstacle obstacle, int x, int y)
    {
        if (_cells[x, y].Unit == null && _cells[x, y].Obstacle == null) _cells[x, y].Obstacle = obstacle;
        else
        {
            throw new ArgumentException("Клетка уже занята");
        }
    }

    public void ClearField()
    {
        foreach (ICell cell in _cells)
        {
            cell.Unit = null;
            cell.Obstacle = null;
        }
    }

    private void AddNeighborsAll()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                AddNeighbors(x, y);
            }
        }
    }

    private IEnumerable<int> Shifts(int v, int? max)
    {
        yield return 0;
        if (v > 0) yield return -1;
        if (v < max - 1) yield return 1;
    }

    private void AddNeighbors(int x, int y)
    {
        foreach (int dx in Shifts(x, Width))
        {
            foreach (int dy in Shifts(y, Height))
            {
                if ((dx == 0) && (dy == 0))
                    continue;
                _cells[x, y].Neighbors.Add(_cells[x + dx, y + dy]);
            }
        }
    }

    public List<ICell> GetNeighborsRadius(ICell cell, int radius)
    {
        if (cell == null) throw new ArgumentNullException("Пустая ссылка на клетку");
        if (radius < 0) throw new ArgumentOutOfRangeException("Радиус не может быть меньше 0");

        HashSet<ICell> result = new HashSet<ICell>(cell.Neighbors)
        {
            cell
        };

        if (radius == 1) new List<ICell>(result);

        int count = 1;

        HashSet<ICell> neighbors = new HashSet<ICell>(cell.Neighbors);

        while (count != radius)
        {
            List<ICell> currentCells = new List<ICell>(neighbors);
            neighbors.Clear();

            for (int i = 0; i < currentCells.Count; i++)
            {
                for (int j = 0; j < currentCells[i].Neighbors.Count; j++)
                {
                    neighbors.Add(currentCells[i].Neighbors[j]);
                    result.Add(currentCells[i].Neighbors[j]);
                }
            }
            count++;
        }

        result.Remove(cell);

        return new List<ICell>(result);
    }
}
