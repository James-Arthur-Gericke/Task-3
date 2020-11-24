using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE_Task_3
{
    class Map
    {

        private Tile[,] mymap;
        private Hero _hero;
        private Enemy[] enemies;
        private int width, height;
        private Random random;
        private Item[] myItems;


        public Tile[,] Mymap { set { mymap = value; } get { return mymap; } }
        public Enemy[] Enemies { set { enemies = value; } get { return enemies; } }
        public Hero _Hero { get { return _hero; } set { _hero = value; } }
        public Item[] MyItems { get { return myItems; } set { myItems = value; } }

        public Map(int minW, int maxW, int minH, int maxH, int numberOfEnemies, int numberOfGoldDrops)
        {
            // 1. Create random size map
            random = new Random();
            height = random.Next(minH, maxH); width = random.Next(minW, maxW);
            mymap = new Tile[width, height]; enemies = new Enemy[numberOfEnemies];
            createEmptyMap(); createBoarderOfMap();

            // 2. Creating the objects
            _hero = (Hero)Create(TileType.hero); mymap[_hero.getX(), _hero.getY()] = (Tile)_hero; myItems = new Item[numberOfGoldDrops]; // added gold drop array

            for (int index = 0; index < enemies.Length; index++)
            {
                // section updated to randomize between goblin & mage enemy
                int enemyRandomSelect = random.Next(0, 2);  //  ( 0 = Goblin , 1 = Mage)

                if (enemyRandomSelect == 1) { enemies[index] = (Enemy)Create(TileType.goblin); mymap[enemies[index].getX(), enemies[index].getY()] = (Tile)enemies[index]; }
                else { enemies[index] = (Enemy)Create(TileType.mage); mymap[enemies[index].getX(), enemies[index].getY()] = (Tile)enemies[index]; }
            }

            for (int index = 0; index < myItems.Length; index++) // added section to add gold items
            {
                myItems[index] = (Item)Create(TileType.gold); mymap[myItems[index].getX(), myItems[index].getY()] = (Tile)myItems[index];
            }

            // 3. update the vision of all characters
            UpdateVision();
        }

        public Item GetItemAtPosition(int x, int y)
        {
            Item tempItem = null;

            for (int index = 0; index < myItems.Length; index++)
            {
                if (myItems[index].getX() == x && myItems[index].getY() == y)
                { tempItem = myItems[index]; index = myItems.Length + 1; }
            }
            return tempItem;
        }

        public void UpdateVision()
        {
            // 1. update hero's vision

            _hero.setVision(VisionPosition.North, mymap[_hero.getX(), _hero.getY() - 1]);        // top object updated
            _hero.setVision(VisionPosition.South, mymap[_hero.getX(), _hero.getY() + 1]);     // buttom object updated
            _hero.setVision(VisionPosition.East, mymap[_hero.getX() + 1, _hero.getY()]);        // right object updated
            _hero.setVision(VisionPosition.West, mymap[_hero.getX() - 1, _hero.getY()]);      // left object updated

            // 2. update Goblin visions

            for (int index = 0; index < enemies.Length; index++)
            {
                if (!enemies[index].isDead())
                {
                    enemies[index].setVision(VisionPosition.North, mymap[enemies[index].getX(), enemies[index].getY() - 1]);
                    enemies[index].setVision(VisionPosition.South, mymap[enemies[index].getX(), enemies[index].getY() + 1]);
                    enemies[index].setVision(VisionPosition.East, mymap[enemies[index].getX() + 1, enemies[index].getY()]);
                    enemies[index].setVision(VisionPosition.West, mymap[enemies[index].getX() - 1, enemies[index].getY()]);
                }
            }
        }

        private Tile Create(TileType type)
        {
            Tile newObject = new EmptyTile(0, 0);

            int[] position = new int[2];

            switch (type)
            {
                case TileType.obstacle: randPos(TileType.obstacle, position); newObject = new Obstacle(position[0], position[1]); break;
                case TileType.goblin: randPos(TileType.goblin, position); newObject = new Goblin(position[0], position[1]); break;
                case TileType.hero: randPos(TileType.hero, position); newObject = new Hero(position[0], position[1]); break;
                case TileType.empty_tile: randPos(TileType.empty_tile, position); newObject = new EmptyTile(position[0], position[1]); break;
                case TileType.mage: randPos(TileType.mage, position); newObject = new Mage(position[0], position[1]); break;    // section updated to include mage creation
                case TileType.gold: randPos(TileType.gold, position); newObject = new Gold(position[0], position[1]); break;    // section updated to include gold creation
            }
            return newObject;
        }

        private void randPos(TileType obj, int[] coordinate)
        {

            if (obj == TileType.obstacle)
            {

                // use clockwise placement

                bool isSpaceFound = false;

                for (int index = 0; index < width && !isSpaceFound; index++)   // checking space in the top row
                {
                    if (mymap[index, 0].I == TileType.empty_tile) { isSpaceFound = true; coordinate[0] = index; coordinate[1] = 0; }
                }

                for (int index = 0; index < height && !isSpaceFound; index++)   // checking space in the right most column
                {
                    if (mymap[width - 1, index].I == TileType.empty_tile) { isSpaceFound = true; coordinate[0] = width - 1; coordinate[1] = index; }
                }

                for (int index = 0; index < width && !isSpaceFound; index++)   // checking space in the buttom  row
                {
                    if (mymap[index, height - 1].I == TileType.empty_tile) { isSpaceFound = true; coordinate[0] = index; coordinate[1] = height - 1; }
                }

                for (int index = 0; index < height && !isSpaceFound; index++)   // checking space in the buttom  row
                {
                    if (mymap[0, index].I == TileType.empty_tile) { isSpaceFound = true; coordinate[0] = 0; coordinate[1] = index; }
                }

            }
            else
            {
                // randomly place object which is not a obstacle

                bool isSpaceFound = false;

                while (!isSpaceFound)
                {
                    int xPos = random.Next(1, width - 1), yPos = random.Next(1, height - 1);

                    if (mymap[xPos, yPos].I == TileType.empty_tile) { isSpaceFound = true; coordinate[0] = xPos; coordinate[1] = yPos; }
                }

            }
        }

        private void createEmptyMap()
        {
            for (int index = 0; index < width; index++)
            {
                for (int index2 = 0; index2 < height; index2++)
                {
                    mymap[index, index2] = new EmptyTile(index, index2);
                }
            }
        }

        private void createBoarderOfMap()
        {

            for (int index = 0; index < width; index++)   // creating the top row
            {
                mymap[index, 0] = new Obstacle(index, 0);
            }

            for (int index = 0; index < height; index++)   // creating the right most column
            {
                mymap[width - 1, index] = new Obstacle(width - 1, index);
            }

            for (int index = 0; index < width; index++)   // creating the bottom  row
            {
                mymap[index, height - 1] = new Obstacle(index, height - 1);
            }

            for (int index = 0; index < height; index++)   // creating the left  column
            {
                mymap[0, index] = new Obstacle(0, index);
            }
        }

        public Tile getTile(int xPos, int yPos) { return mymap[xPos, yPos]; }

        public int getMapHeight() { return height; }

        public int getMapWidth() { return width; }

    }
}
