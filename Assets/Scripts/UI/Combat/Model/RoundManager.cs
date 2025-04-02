using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RoundManager : IRoundManager
{
    public int Round { get; set; }

    public RoundManager()
    {
        Round = 0;
    }

    public event EventHandler<int> OnRoundChanged;

    public void NextRound()
    {
        Round++;
        OnRoundChanged?.Invoke(this, Round);
    }
}
