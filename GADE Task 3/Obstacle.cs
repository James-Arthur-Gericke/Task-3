using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE_Task_3
{
    class Obstacle : Tile
    {
       public Obstacle(int x, int y) : base(x, y, TileType.obstacle) { }
    }
}
