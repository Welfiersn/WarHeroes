using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IRoundManager
{
    public int Round { get; set; }
    public event EventHandler<int> OnRoundChanged;
    public void NextRound();
}