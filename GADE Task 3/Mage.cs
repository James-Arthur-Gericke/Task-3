using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE_Task_3
{
    class Mage : Enemy
    {
        public Mage(int xPos, int yPos) : base(xPos, yPos, TileType.mage, 5, 5, 5) { }
        public override MovementEnum ReturnMove(MovementEnum move = 0) { return MovementEnum.No_Movement; }
        public override bool CheckRange(Character target)
        {
            int xPos = Math.Abs(target.getX() - getX()), yPos = Math.Abs(target.getY() - getY());
            return xPos < 2 && yPos < 2;
        }
    }
}
