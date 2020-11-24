using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE_Task_3
{
    class Hero : Character
    {


        public Hero(int x, int y) : base(x, y, TileType.hero) { setDamage(2); setHP(10); setMaxHP(10); } // Hp & Max HP not specified in brief

        public override MovementEnum ReturnMove(MovementEnum move = 0)
        {
            if (isMoveValid(move)) return move; else return MovementEnum.No_Movement;
        }

        public bool isMoveValid(MovementEnum direction)
        {

            if (direction == MovementEnum.Down)
            {
                return getVision(VisionPosition.South).I == TileType.empty_tile || getVision(VisionPosition.South).I == TileType.gold;
            }
            else if (direction == MovementEnum.Up)
            {
                return getVision(VisionPosition.North).I == TileType.empty_tile || getVision(VisionPosition.North).I == TileType.gold;
            }
            else if (direction == MovementEnum.Left)
            {
                return getVision(VisionPosition.West).I == TileType.empty_tile || getVision(VisionPosition.West).I == TileType.gold;
            }
            else { return getVision(VisionPosition.East).I == TileType.empty_tile || getVision(VisionPosition.East).I == TileType.gold; }
        }

        public override string ToString()
        {
            return " Player Stats: \n HP: " + getHP().ToString() + " / " + getMaxHP().ToString() + "\n Damage: 2\n[" + getX().ToString() + "," + getY().ToString() + "]" + "\n GA: " + goldPurse.ToString();
        }
    }
}


