using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE_Task_3
{
    abstract class Item : Tile
    {
        public Item(int xPos, int yPos) : base(xPos, yPos, TileType.item) { }
        public abstract override string ToString();
    }
}
