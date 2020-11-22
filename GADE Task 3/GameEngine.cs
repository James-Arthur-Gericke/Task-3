using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GADE_Task_3
{
    class GameEngine
    {
        private static Map gameMap;

        public Map GameMap { get { return gameMap; } }
        public GameEngine() { gameMap = new Map(10, 16, 10, 16, 4); }

        public bool MovePlayer(MovementEnum direction)
        {
            bool isValid = false;
            switch (direction)
            {
                case MovementEnum.No_Movement: break;
                case MovementEnum.Up:

                    if (gameMap._Hero.isMoveValid(MovementEnum.Up))
                    {
                        // hero has to move up
                        Tile tempt = new EmptyTile(gameMap._Hero.getX(), gameMap._Hero.getY()); // store position before update
                        gameMap._Hero.setY(gameMap._Hero.getY() - 1);
                        gameMap.Mymap[gameMap._Hero.getX(), gameMap._Hero.getY()] = gameMap._Hero;
                        gameMap.Mymap[gameMap._Hero.getX(), gameMap._Hero.getY() + 1] = tempt; gameMap.UpdateVision();
                    }
                    break;


                case MovementEnum.Down:

                    if (gameMap._Hero.isMoveValid(MovementEnum.Down))
                    {
                        Tile tempt = new EmptyTile(gameMap._Hero.getX(), gameMap._Hero.getY());
                        gameMap._Hero.setY(gameMap._Hero.getY() + 1);
                        gameMap.Mymap[gameMap._Hero.getX(), gameMap._Hero.getY()] = gameMap._Hero;
                        gameMap.Mymap[gameMap._Hero.getX(), gameMap._Hero.getY() - 1] = tempt; gameMap.UpdateVision();
                    }
                    break;


                case MovementEnum.Left:

                    if (gameMap._Hero.isMoveValid(MovementEnum.Left))
                    {
                        Tile tempt = new EmptyTile(gameMap._Hero.getX(), gameMap._Hero.getY());
                        gameMap._Hero.setX(gameMap._Hero.getX() - 1);
                        gameMap.Mymap[gameMap._Hero.getX(), gameMap._Hero.getY()] = gameMap._Hero;
                        gameMap.Mymap[gameMap._Hero.getX() + 1, gameMap._Hero.getY()] = tempt; gameMap.UpdateVision();
                    }
                    break;

                case MovementEnum.Right:

                    if (gameMap._Hero.isMoveValid(MovementEnum.Right))
                    {
                        Tile tempt = new EmptyTile(gameMap._Hero.getX(), gameMap._Hero.getY());
                        gameMap._Hero.setX(gameMap._Hero.getX() + 1);
                        gameMap.Mymap[gameMap._Hero.getX(), gameMap._Hero.getY()] = gameMap._Hero;
                        gameMap.Mymap[gameMap._Hero.getX() - 1, gameMap._Hero.getY()] = tempt; gameMap.UpdateVision();
                    }
                    break;
            }

            return isValid;
        }

        private static char readOnlyCharGameObjects(int xIndex, int yIndex)
        {
            char answer = ' ';

            switch (gameMap.Mymap[xIndex, yIndex].I)
            {
                case TileType.empty_tile: answer = '.'; break;
                case TileType.goblin: answer = 'G'; break;
                case TileType.hero: answer = 'H'; break;
                case TileType.obstacle: answer = 'X'; break;
            }

            return answer;
        }

        public override string ToString()
        {
            string answer = "";

            for (int index = 0; index < gameMap.getMapHeight(); index++)
            {
                for (int index2 = 0; index2 < gameMap.getMapWidth(); index2++)
                {
                    answer += readOnlyCharGameObjects(index2, index);

                    if (index2 == gameMap.getMapWidth()) answer += "\n";
                }
            }

            return answer;
        }
    }
}



// This is where I saved 