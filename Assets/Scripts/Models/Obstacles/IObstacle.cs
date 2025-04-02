using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IObstacle
{
    public int ID { get; set; }
    public string Description { get; set; }
}

