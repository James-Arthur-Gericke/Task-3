using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE_Task_3
{
    class EmptyTile : Tile
    {
            public EmptyTile(int x, int y) : base(x, y, TileType.empty_tile) { }
    }
}
