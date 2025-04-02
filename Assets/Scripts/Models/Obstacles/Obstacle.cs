using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Obstacle : IObstacle
{
    public int ID {  get; set; }

    private string _description;
    public string Description 
    {
        get { return _description; }
        set { _description = value; }
    }

    public Obstacle(string description)
    {
        Description = description;
    }
}
