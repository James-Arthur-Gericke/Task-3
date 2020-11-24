using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE_Task_3
{
    class Leader : Enemy
    {
        private Tile target;



        public Leader(int xPos, int yPos) : base(xPos, yPos, TileType.leader, 2, 20, 20) { target = new Tile(0, 0, TileType.hero); }

        public Tile Target { get { return target; } set { target = value; } }

        public override MovementEnum ReturnMove(MovementEnum move = 0)
        {
            int answer = -1;
            // 1. Move towards the target

            int deltaX = target.getX() - getX();    // Start by matching the xPos first
            if (deltaX > 0 && getVision(VisionPosition.East).I == TileType.empty_tile) { answer = 3; } // Leader can move right
            else if (deltaX < 0 && getVision(VisionPosition.West).I == TileType.empty_tile) { answer = 2; } // Leader can move left

            if (answer == -1) // Leader could not move left or right, priorities moving up or down 
            {
                int deltaY = target.getY() - getY();    // now matching the yPos
                if (deltaY > 0 && getVision(VisionPosition.South).I == TileType.empty_tile) { answer = 1; } // Leader can move down
                else if (deltaY < 0 && getVision(VisionPosition.North).I == TileType.empty_tile) { answer = 0; } // Leader can move left
            }

            if (answer == -1) // Leader failed to move towards the hero, now has to move randomly in a valid direction 
            {
                // 2. Move randomly
                R = new Random(Guid.NewGuid().GetHashCode()); answer = R.Next(0, 4);

                while (!isMoveValid(answer)) { R = new Random(Guid.NewGuid().GetHashCode()); answer = R.Next(0, 4); } // roll until a valid direction is obtained
            }
            return (MovementEnum)answer;
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
