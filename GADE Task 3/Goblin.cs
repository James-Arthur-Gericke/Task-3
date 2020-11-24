using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE_Task_3
{
    class Goblin : Enemy
    {
        public Goblin(int x, int y) : base(x, y, TileType.goblin, 1, 10, 10) { }

        public override MovementEnum ReturnMove(MovementEnum move = 0)
        {
            R = new Random(Guid.NewGuid().GetHashCode());
            int min = 0, max = 3, randomNumber = R.Next(min, max + 1);

            while (!isMoveValid(randomNumber)) { R = new Random(Guid.NewGuid().GetHashCode()); randomNumber = R.Next(min, max + 1); }

            return (MovementEnum)randomNumber;
        }

        private bool isMoveValid(int randomNumber)
        {
            if (randomNumber == 0)
            {
                return getVision(VisionPosition.North).I == TileType.empty_tile;
            }
            else if (randomNumber == 1) // Down
            {
                return getVision(VisionPosition.South).I == TileType.empty_tile;
            }
            else if (randomNumber == 2) // Left
            {
                return getVision(VisionPosition.West).I == TileType.empty_tile;
            }
            else { return getVision(VisionPosition.East).I == TileType.empty_tile; } // right
        }

    }
}
