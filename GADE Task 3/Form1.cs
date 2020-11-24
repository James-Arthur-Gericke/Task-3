using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GADE_Task_3
{
    public partial class Form1 : Form
    {
        private GameEngine myMap = new GameEngine();
        private int positionScale = 25;
        private Label[,] gameMapLabels;
        private Label heroStats;
        private Label[] enemyStats;


        public Form1()
        {
            InitializeComponent();
            CreateMap(); UpdateCharacterColourCode(); UpdateItemsColourCode(); DisplayMap();
            CreateCharacterStats(); UpdateCharacterStatsColourCode(); DisplayCharacterStats();
        }



        // Create Methods Start here ############################################################################
        private void CreateMap() // Method creates only the map labels only
        {
            gameMapLabels = new Label[myMap.GameMap.getMapWidth(), myMap.GameMap.getMapHeight()];

            for (int index = 0; index < myMap.GameMap.getMapWidth(); index++)
            {
                for (int index2 = 0; index2 < myMap.GameMap.getMapHeight(); index2++)
                {
                    gameMapLabels[index, index2] = CreateLabel(positionScale * myMap.GameMap.getTile(index, index2).getX(), positionScale * myMap.GameMap.getTile(index, index2).getY(), IdentityToStringConverter(myMap.GameMap.getTile(index, index2).I));
                }
            }
        }

        private void CreateCharacterStats()
        {
            enemyStats = new Label[myMap.GameMap.Enemies.Length]; heroStats = new Label();
            heroStats = CreatecharacterLable(myMap.GameMap._Hero.ToString()); heroStats.Location = new Point(500, 350); heroStats.Width = 75; heroStats.Height = 100;
            enemyStats[0] = CreatecharacterLable(myMap.GameMap.Enemies[0].ToString() + "\n HP: " + myMap.GameMap.Enemies[0].getHP().ToString() + " / " + myMap.GameMap.Enemies[0].getMaxHP().ToString()); enemyStats[0].Location = new Point(465, 10); enemyStats[0].Width = 130; enemyStats[0].Height = 45;
            enemyStats[1] = CreatecharacterLable(myMap.GameMap.Enemies[1].ToString() + "\n HP: " + myMap.GameMap.Enemies[1].getHP().ToString() + " / " + myMap.GameMap.Enemies[1].getMaxHP().ToString()); enemyStats[1].Location = new Point(465, 60); enemyStats[1].Width = 130; enemyStats[1].Height = 45;
            enemyStats[2] = CreatecharacterLable(myMap.GameMap.Enemies[2].ToString() + "\n HP: " + myMap.GameMap.Enemies[2].getHP().ToString() + " / " + myMap.GameMap.Enemies[2].getMaxHP().ToString()); enemyStats[2].Location = new Point(715, 10); enemyStats[2].Width = 130; enemyStats[2].Height = 45;
            enemyStats[3] = CreatecharacterLable(myMap.GameMap.Enemies[3].ToString() + "\n HP: " + myMap.GameMap.Enemies[3].getHP().ToString() + " / " + myMap.GameMap.Enemies[3].getMaxHP().ToString()); enemyStats[3].Location = new Point(715, 60); enemyStats[3].Width = 130; enemyStats[3].Height = 45;
        }

        public Label CreateLabel(int x, int y, string I)
        {
            Label myLabel = new Label();
            myLabel.Text = I;
            myLabel.Width = positionScale;
            myLabel.Height = positionScale;
            myLabel.ForeColor = Color.Red;
            myLabel.BackColor = Color.White;
            myLabel.Location = new Point(x, y);
            myLabel.TextAlign = ContentAlignment.MiddleCenter;

            return myLabel;
        }

        public Label CreatecharacterLable(string stats)
        {
            Label myLabel = new Label(); myLabel.Text = stats;
            myLabel.ForeColor = Color.Red; myLabel.BackColor = Color.White;
            myLabel.Location = new Point(500, 380); myLabel.Width = 70; myLabel.Height = 90;
            myLabel.TextAlign = ContentAlignment.MiddleCenter; return myLabel;
        }






        // Display Methods Start here #####################################################################

        private void DisplayMap() // Method only displays the map
        {
            for (int index = 0; index < myMap.GameMap.getMapWidth(); index++)
            {
                for (int index2 = 0; index2 < myMap.GameMap.getMapHeight(); index2++)
                {
                    Controls.Add(gameMapLabels[index, index2]);
                }
            }
        }

        private void DisplayCharacterStats() // Method only displays character stats
        {
            Controls.Add(heroStats); for (int index = 0; index < enemyStats.Length; index++) Controls.Add(enemyStats[index]);
        }






        // Object colour codes start here ####################################################
        private void UpdateCharacterColourCode()
        {
            if (!myMap.GameMap.Enemies[0].isDead()) gameMapLabels[myMap.GameMap.Enemies[0].getX(), myMap.GameMap.Enemies[0].getY()].ForeColor = System.Drawing.Color.DeepPink;
            if (!myMap.GameMap.Enemies[1].isDead()) gameMapLabels[myMap.GameMap.Enemies[1].getX(), myMap.GameMap.Enemies[1].getY()].ForeColor = System.Drawing.Color.Blue;
            if (!myMap.GameMap.Enemies[2].isDead()) gameMapLabels[myMap.GameMap.Enemies[2].getX(), myMap.GameMap.Enemies[2].getY()].ForeColor = System.Drawing.Color.Green;
            if (!myMap.GameMap.Enemies[3].isDead()) gameMapLabels[myMap.GameMap.Enemies[3].getX(), myMap.GameMap.Enemies[3].getY()].ForeColor = System.Drawing.Color.Red;
            gameMapLabels[myMap.GameMap._Hero.getX(), myMap.GameMap._Hero.getY()].ForeColor = System.Drawing.Color.Black;
        }

        private void UpdateCharacterStatsColourCode()
        {
            enemyStats[0].ForeColor = System.Drawing.Color.DeepPink;
            enemyStats[1].ForeColor = System.Drawing.Color.Blue;
            enemyStats[2].ForeColor = System.Drawing.Color.Green;
            enemyStats[3].ForeColor = System.Drawing.Color.Red;
            heroStats.ForeColor = System.Drawing.Color.Black;
        }

        private void UpdateItemsColourCode()
        {
            gameMapLabels[myMap.GameMap.MyItems[0].getX(), myMap.GameMap.MyItems[0].getY()].ForeColor = System.Drawing.Color.Gold;
            gameMapLabels[myMap.GameMap.MyItems[1].getX(), myMap.GameMap.MyItems[1].getY()].ForeColor = System.Drawing.Color.Gold;
            gameMapLabels[myMap.GameMap.MyItems[2].getX(), myMap.GameMap.MyItems[2].getY()].ForeColor = System.Drawing.Color.Gold;
        }






        // Hero player controls start here ########################################################
        private void button1_Click(object sender, EventArgs e)
        {
            myMap.MovePlayer(MovementEnum.Up); myMap.MoveEnemies(); myMap.EnemyAttacks(); UpdateMapText(); UpdateCharacterColourCode(); UpdateCharacterStatsText();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            myMap.MovePlayer(MovementEnum.Down); myMap.MoveEnemies(); myMap.EnemyAttacks(); UpdateMapText(); UpdateCharacterColourCode(); UpdateCharacterStatsText();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            myMap.MovePlayer(MovementEnum.Left); myMap.MoveEnemies(); myMap.EnemyAttacks(); UpdateMapText(); UpdateCharacterColourCode(); UpdateCharacterStatsText();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            myMap.MovePlayer(MovementEnum.Right); myMap.MoveEnemies(); myMap.EnemyAttacks(); UpdateMapText(); UpdateCharacterColourCode(); UpdateCharacterStatsText();
        }

        // player controls end here ###############################################################








        // Runtime Update Methods start here ###############################################################

        public void UpdateMapText()
        {
            for (int index = 0; index < myMap.GameMap.getMapHeight(); index++)
            {
                for (int index2 = 0; index2 < myMap.GameMap.getMapWidth(); index2++)
                {
                    gameMapLabels[index2, index].Text = IdentityToStringConverter(myMap.GameMap.Mymap[index2, index].I);
                }
            }
        }

        public void UpdateCharacterStatsText()
        {
            heroStats.Text = myMap.GameMap._Hero.ToString();
            enemyStats[0].Text = myMap.GameMap.Enemies[0].ToString() + "\n HP: " + myMap.GameMap.Enemies[0].getHP().ToString() + " / " + myMap.GameMap.Enemies[0].getMaxHP().ToString();
            enemyStats[1].Text = myMap.GameMap.Enemies[1].ToString() + "\n HP: " + myMap.GameMap.Enemies[1].getHP().ToString() + " / " + myMap.GameMap.Enemies[1].getMaxHP().ToString();
            enemyStats[2].Text = myMap.GameMap.Enemies[2].ToString() + "\n HP: " + myMap.GameMap.Enemies[2].getHP().ToString() + " / " + myMap.GameMap.Enemies[2].getMaxHP().ToString();
            enemyStats[3].Text = myMap.GameMap.Enemies[3].ToString() + "\n HP: " + myMap.GameMap.Enemies[3].getHP().ToString() + " / " + myMap.GameMap.Enemies[3].getMaxHP().ToString();
        }
        // Runtime Updates Methods end here




        // Enemy shoot buttoms start here ############################# 

        private void button8_Click(object sender, EventArgs e)
        {
            // button reserved for enemy 1

            if (myMap.GameMap._Hero.CheckRange(myMap.GameMap.Enemies[0]) && !myMap.GameMap.Enemies[0].isDead())
            {
                myMap.GameMap._Hero.Attack(myMap.GameMap.Enemies[0]); myMap.GameMap.UpdateVision();
                enemyStats[0].Text = myMap.GameMap.Enemies[0].ToString() + "\n HP: " + myMap.GameMap.Enemies[0].getHP().ToString() + " / " + myMap.GameMap.Enemies[0].getMaxHP().ToString();

                if (myMap.GameMap.Enemies[0].isDead())
                {
                    // need to create an empty tile in its position
                    myMap.GameMap.Mymap[myMap.GameMap.Enemies[0].getX(), myMap.GameMap.Enemies[0].getY()] = new EmptyTile(myMap.GameMap.Enemies[0].getX(), myMap.GameMap.Enemies[0].getY());
                    gameMapLabels[myMap.GameMap.Enemies[0].getX(), myMap.GameMap.Enemies[0].getY()].Text = "."; gameMapLabels[myMap.GameMap.Enemies[0].getX(), myMap.GameMap.Enemies[0].getY()].ForeColor = System.Drawing.Color.Red;
                }
            }

            myMap.EnemyAttacks(); myMap.GameMap.UpdateVision(); UpdateCharacterStatsText();
            // Need to manage updates to all object states and appearance
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // button reserved for enemy 2
            if (myMap.GameMap._Hero.CheckRange(myMap.GameMap.Enemies[1]) && !myMap.GameMap.Enemies[1].isDead())
            {
                myMap.GameMap._Hero.Attack(myMap.GameMap.Enemies[1]); myMap.GameMap.UpdateVision();
                enemyStats[1].Text = myMap.GameMap.Enemies[1].ToString() + "\n HP: " + myMap.GameMap.Enemies[1].getHP().ToString() + " / " + myMap.GameMap.Enemies[1].getMaxHP().ToString();

                if (myMap.GameMap.Enemies[1].isDead())
                {
                    // need to create an empty tile in its position
                    myMap.GameMap.Mymap[myMap.GameMap.Enemies[1].getX(), myMap.GameMap.Enemies[1].getY()] = new EmptyTile(myMap.GameMap.Enemies[1].getX(), myMap.GameMap.Enemies[1].getY());
                    gameMapLabels[myMap.GameMap.Enemies[1].getX(), myMap.GameMap.Enemies[1].getY()].Text = "."; gameMapLabels[myMap.GameMap.Enemies[1].getX(), myMap.GameMap.Enemies[1].getY()].ForeColor = System.Drawing.Color.Red;
                }
            }

            myMap.EnemyAttacks(); myMap.GameMap.UpdateVision(); UpdateCharacterStatsText();
            // Need to manage updates to all object states and appearance
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // button reserved for enemy 3
            if (myMap.GameMap._Hero.CheckRange(myMap.GameMap.Enemies[2]) && !myMap.GameMap.Enemies[2].isDead())
            {
                myMap.GameMap._Hero.Attack(myMap.GameMap.Enemies[2]); myMap.GameMap.UpdateVision();
                enemyStats[2].Text = myMap.GameMap.Enemies[2].ToString() + "\n HP: " + myMap.GameMap.Enemies[2].getHP().ToString() + " / " + myMap.GameMap.Enemies[2].getMaxHP().ToString();

                if (myMap.GameMap.Enemies[2].isDead())
                {
                    // need to create an empty tile in its position
                    myMap.GameMap.Mymap[myMap.GameMap.Enemies[2].getX(), myMap.GameMap.Enemies[2].getY()] = new EmptyTile(myMap.GameMap.Enemies[2].getX(), myMap.GameMap.Enemies[2].getY());
                    gameMapLabels[myMap.GameMap.Enemies[2].getX(), myMap.GameMap.Enemies[2].getY()].Text = "."; gameMapLabels[myMap.GameMap.Enemies[2].getX(), myMap.GameMap.Enemies[2].getY()].ForeColor = System.Drawing.Color.Red;
                }
            }

            myMap.EnemyAttacks(); myMap.GameMap.UpdateVision(); UpdateCharacterStatsText();
            // Need to manage updates to all object states and appearance
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // button reserved for enemy 4
            if (myMap.GameMap._Hero.CheckRange(myMap.GameMap.Enemies[3]) && !myMap.GameMap.Enemies[3].isDead())
            {
                myMap.GameMap._Hero.Attack(myMap.GameMap.Enemies[3]); myMap.GameMap.UpdateVision();
                enemyStats[3].Text = myMap.GameMap.Enemies[3].ToString() + "\n HP: " + myMap.GameMap.Enemies[3].getHP().ToString() + " / " + myMap.GameMap.Enemies[3].getMaxHP().ToString();

                if (myMap.GameMap.Enemies[3].isDead())
                {
                    // need to create an empty tile in its position
                    myMap.GameMap.Mymap[myMap.GameMap.Enemies[3].getX(), myMap.GameMap.Enemies[3].getY()] = new EmptyTile(myMap.GameMap.Enemies[3].getX(), myMap.GameMap.Enemies[3].getY());
                    gameMapLabels[myMap.GameMap.Enemies[3].getX(), myMap.GameMap.Enemies[3].getY()].Text = "."; gameMapLabels[myMap.GameMap.Enemies[3].getX(), myMap.GameMap.Enemies[3].getY()].ForeColor = System.Drawing.Color.Red;
                }
            }

            myMap.EnemyAttacks(); myMap.GameMap.UpdateVision(); UpdateCharacterStatsText();
            // Need to manage updates to all object states and appearance
        }

        // Enemy shoot buttoms end here ############################# 






        // General Supporting Methods Start Here ##########################################

        private string IdentityToStringConverter(TileType Identity)
        {
            string answer = "";

            switch (Identity)
            {
                case TileType.empty_tile: answer = "."; break;
                case TileType.goblin: answer = "G"; break;
                case TileType.hero: answer = "H"; break;
                case TileType.obstacle: answer = "X"; break;
                case TileType.mage: answer = "M"; break;
                case TileType.gold: answer = "g"; break;
            }
            return answer;
        }
    }
}
