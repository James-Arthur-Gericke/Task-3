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
        private static Shop gameShop;

        public Map GameMap { get { return gameMap; } }
        public GameEngine() { gameMap = new Map(14, 16, 14, 16, 4, 3); }

        public Shop GameShop { get { return gameShop; } }

        public bool MovePlayer(MovementEnum direction)
        {
            bool isValid = false;
            switch (direction)
            {
                case MovementEnum.No_Movement: break;
                case MovementEnum.Up:

                    if (gameMap._Hero.isMoveValid(MovementEnum.Up))
                    {
                        // accomodate pick up items
                        Item itemToBeConsumed = gameMap.GetItemAtPosition(gameMap._Hero.getX(), gameMap._Hero.getY() - 1);
                        if (itemToBeConsumed != null) { gameMap._Hero.Pickup(itemToBeConsumed); }

                        Tile tempt = new EmptyTile(gameMap._Hero.getX(), gameMap._Hero.getY()); // store position before update
                        gameMap._Hero.setY(gameMap._Hero.getY() - 1);
                        gameMap.Mymap[gameMap._Hero.getX(), gameMap._Hero.getY()] = gameMap._Hero;
                        gameMap.Mymap[tempt.getX(), tempt.getY()] = tempt; gameMap.UpdateVision(); isValid = true;
                    }
                    break;


                case MovementEnum.Down:

                    if (gameMap._Hero.isMoveValid(MovementEnum.Down))
                    {
                        Item itemToBeConsumed = gameMap.GetItemAtPosition(gameMap._Hero.getX(), gameMap._Hero.getY() + 1);
                        if (itemToBeConsumed != null) { gameMap._Hero.Pickup(itemToBeConsumed); }

                        Tile tempt = new EmptyTile(gameMap._Hero.getX(), gameMap._Hero.getY());
                        gameMap._Hero.setY(gameMap._Hero.getY() + 1);
                        gameMap.Mymap[gameMap._Hero.getX(), gameMap._Hero.getY()] = gameMap._Hero;
                        gameMap.Mymap[gameMap._Hero.getX(), gameMap._Hero.getY() - 1] = tempt; gameMap.UpdateVision(); isValid = true;
                    }
                    break;


                case MovementEnum.Left:

                    if (gameMap._Hero.isMoveValid(MovementEnum.Left))
                    {
                        Item itemToBeConsumed = gameMap.GetItemAtPosition(gameMap._Hero.getX() - 1, gameMap._Hero.getY());
                        if (itemToBeConsumed != null) { gameMap._Hero.Pickup(itemToBeConsumed); }

                        Tile tempt = new EmptyTile(gameMap._Hero.getX(), gameMap._Hero.getY());
                        gameMap._Hero.setX(gameMap._Hero.getX() - 1);
                        gameMap.Mymap[gameMap._Hero.getX(), gameMap._Hero.getY()] = gameMap._Hero;
                        gameMap.Mymap[gameMap._Hero.getX() + 1, gameMap._Hero.getY()] = tempt; gameMap.UpdateVision(); isValid = true;
                    }
                    break;

                case MovementEnum.Right:

                    if (gameMap._Hero.isMoveValid(MovementEnum.Right))
                    {
                        Item itemToBeConsumed = gameMap.GetItemAtPosition(gameMap._Hero.getX() + 1, gameMap._Hero.getY());
                        if (itemToBeConsumed != null) { gameMap._Hero.Pickup(itemToBeConsumed); }

                        Tile tempt = new EmptyTile(gameMap._Hero.getX(), gameMap._Hero.getY());
                        gameMap._Hero.setX(gameMap._Hero.getX() + 1);
                        gameMap.Mymap[gameMap._Hero.getX(), gameMap._Hero.getY()] = gameMap._Hero;
                        gameMap.Mymap[gameMap._Hero.getX() - 1, gameMap._Hero.getY()] = tempt; gameMap.UpdateVision(); isValid = true;
                    }
                    break;
            }

            return isValid;
        }

        public void MoveEnemies()
        {
            for (int index = 0; index < gameMap.Enemies.Length; index++)
            {

                if (!gameMap.Enemies[index].isDead())
                {
                    MovementEnum randomMovementDirection = gameMap.Enemies[index].ReturnMove();                                         // Obtain randon direction 
                    EmptyTile tempt = new EmptyTile(gameMap.Enemies[index].getX(), gameMap.Enemies[index].getY());                      // Enemy's position to be occupied by empty tile
                                                                                                                                        // update enemy position
                    if (randomMovementDirection != MovementEnum.No_Movement)
                    {
                        gameMap.Enemies[index].Move(randomMovementDirection);

                        switch (randomMovementDirection)
                        {
                            case MovementEnum.No_Movement: break;
                            case MovementEnum.Up:

                                gameMap.Mymap[gameMap.Enemies[index].getX(), gameMap.Enemies[index].getY()] = gameMap.Enemies[index];    // update the enemy on the map
                                gameMap.Mymap[tempt.getX(), tempt.getY()] = tempt;                                                      // update the empty tile on the map
                                gameMap.UpdateVision(); break;                                                                             // update the vision of all objects

                            case MovementEnum.Down:

                                gameMap.Mymap[gameMap.Enemies[index].getX(), gameMap.Enemies[index].getY()] = gameMap.Enemies[index];
                                gameMap.Mymap[gameMap.Enemies[index].getX(), gameMap.Enemies[index].getY() - 1] = tempt;
                                gameMap.UpdateVision(); break;

                            case MovementEnum.Left:

                                gameMap.Mymap[gameMap.Enemies[index].getX(), gameMap.Enemies[index].getY()] = gameMap.Enemies[index];
                                gameMap.Mymap[gameMap.Enemies[index].getX() + 1, gameMap.Enemies[index].getY()] = tempt;
                                gameMap.UpdateVision(); break;

                            case MovementEnum.Right:

                                gameMap.Mymap[gameMap.Enemies[index].getX(), gameMap.Enemies[index].getY()] = gameMap.Enemies[index];
                                gameMap.Mymap[gameMap.Enemies[index].getX() - 1, gameMap.Enemies[index].getY()] = tempt;
                                gameMap.UpdateVision(); break;
                        }
                    }
                }
            }
        }

        public void EnemyAttacks()
        {
            for (int index = 0; index < gameMap.Enemies.Length; index++)
            {
                if (gameMap.Enemies[index].I == TileType.goblin && !gameMap.Enemies[index].isDead()) // Goblin attacks only affect the hero object
                {
                    if (gameMap.Enemies[index].CheckRange(gameMap._Hero)) gameMap.Enemies[index].Attack(gameMap._Hero);
                }
                else if (gameMap.Enemies[index].I == TileType.mage && !gameMap.Enemies[index].isDead()) // mage can shoot the hero and all other enemies
                {
                    if (gameMap.Enemies[index].CheckRange(gameMap._Hero)) gameMap.Enemies[index].Attack(gameMap._Hero);  // mage attacks hero

                    for (int index2 = 0; index2 < gameMap.Enemies.Length; index2++)
                    {
                        if (gameMap.Enemies[index].CheckRange(gameMap.Enemies[index2]) && index2 != index) // mage attacks other enemies
                        {
                            gameMap.Enemies[index].Attack(gameMap.Enemies[index2]);

                            if (gameMap.Enemies[index2].isDead())       // Dead enemy should be replaced with an empty tile at its current position
                            {
                                gameMap.Mymap[gameMap.Enemies[index2].getX(), gameMap.Enemies[index2].getY()] = new EmptyTile(gameMap.Enemies[index2].getX(), gameMap.Enemies[index2].getY());
                            }
                        }
                    }
                }
            }
            gameMap.UpdateVision();
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
                case TileType.mage: answer = 'M'; break;
                case TileType.gold: answer = 'g'; break;
            }

            return answer;
        }

        private void mapObjectSwap(Tile object1, Tile Object2)
        {
            Tile tempStorage = object1;
            object1 = Object2; Object2 = tempStorage; Object2.setX(object1.getX()); Object2.setY(object1.getY());
            object1.setX(tempStorage.getX()); object1.setY(tempStorage.getY());

            gameMap.Mymap[object1.getX(), object1.getY()] = object1;                                // obj2 is being stored at obj 1 pos                
            gameMap.Mymap[Object2.getX(), Object2.getY()] = Object2;
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