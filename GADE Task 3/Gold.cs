using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE_Task_3
{
    class Gold : Item
    {
        private int amount;
        private Random random;

        public Gold(int xPos, int yPos) : base(xPos, yPos) { I = TileType.gold; ; random = new Random(Guid.NewGuid().GetHashCode()); amount = random.Next(1, 6); }
        public int Amount { get { return amount; } }
        public override string ToString() { return I.ToString(); }
    }
}
