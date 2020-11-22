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
            InitializeMap();
        }

        private void InitializeMap()
        {
            gameMapLabels = new Label[myMap.GameMap.getMapWidth(), myMap.GameMap.getMapHeight()];
            enemyStats = new Label[myMap.GameMap.Enemies.Length];
            LoadMap(); LoadCharacterStats();
        }

        public Label getLabel(int x, int y, string I)
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


        public void LoadMap()
        {
            for (int index = 0; index < myMap.GameMap.getMapWidth(); index++)
            {
                for (int index2 = 0; index2 < myMap.GameMap.getMapHeight(); index2++)
                {
                    gameMapLabels[index, index2] = getLabel(positionScale * myMap.GameMap.getTile(index, index2).getX(), positionScale * myMap.GameMap.getTile(index, index2).getY(), IdentityToStringConverter(myMap.GameMap.getTile(index, index2).I));
                }
            }
            LoadEnemyColourCode();

            gameMapLabels[myMap.GameMap._Hero.getX(), myMap.GameMap._Hero.getY()].ForeColor = System.Drawing.Color.Black;

            for (int index = 0; index < myMap.GameMap.getMapWidth(); index++)
            {
                for (int index2 = 0; index2 < myMap.GameMap.getMapHeight(); index2++)
                {
                    Controls.Add(gameMapLabels[index, index2]);
                }
            }
        }

        private void LoadEnemyColourCode()
        {
            gameMapLabels[myMap.GameMap.Enemies[0].getX(), myMap.GameMap.Enemies[0].getY()].ForeColor = System.Drawing.Color.DeepPink;
            gameMapLabels[myMap.GameMap.Enemies[1].getX(), myMap.GameMap.Enemies[1].getY()].ForeColor = System.Drawing.Color.Blue;
            gameMapLabels[myMap.GameMap.Enemies[2].getX(), myMap.GameMap.Enemies[2].getY()].ForeColor = System.Drawing.Color.Green;
            gameMapLabels[myMap.GameMap.Enemies[3].getX(), myMap.GameMap.Enemies[3].getY()].ForeColor = System.Drawing.Color.Red;
        }

        private string IdentityToStringConverter(TileType Identity)
        {
            string answer = "";

            switch (Identity)
            {
                case TileType.empty_tile: answer = "."; break;
                case TileType.goblin: answer = "G"; break;
                case TileType.hero: answer = "H"; break;
                case TileType.obstacle: answer = "X"; break;
            }
            return answer;
        }

        // Hero game controls start here ########################################################
        private void button1_Click(object sender, EventArgs e)
        {
            myMap.MovePlayer(MovementEnum.Up);
            gameMapLabels[myMap.GameMap._Hero.getX(), myMap.GameMap._Hero.getY()].Text = IdentityToStringConverter(myMap.GameMap._Hero.I);
            gameMapLabels[myMap.GameMap._Hero.getX(), myMap.GameMap._Hero.getY() + 1].Text = IdentityToStringConverter(myMap.GameMap.Mymap[myMap.GameMap._Hero.getX(), myMap.GameMap._Hero.getY() + 1].I);
            gameMapLabels[myMap.GameMap._Hero.getX(), myMap.GameMap._Hero.getY()].ForeColor = System.Drawing.Color.Black;
            heroStats.Text = myMap.GameMap._Hero.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            myMap.MovePlayer(MovementEnum.Down);
            gameMapLabels[myMap.GameMap._Hero.getX(), myMap.GameMap._Hero.getY()].Text = IdentityToStringConverter(myMap.GameMap._Hero.I);
            gameMapLabels[myMap.GameMap._Hero.getX(), myMap.GameMap._Hero.getY() - 1].Text = IdentityToStringConverter(myMap.GameMap.Mymap[myMap.GameMap._Hero.getX(), myMap.GameMap._Hero.getY() - 1].I);
            gameMapLabels[myMap.GameMap._Hero.getX(), myMap.GameMap._Hero.getY()].ForeColor = System.Drawing.Color.Black;
            heroStats.Text = myMap.GameMap._Hero.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            myMap.MovePlayer(MovementEnum.Left);
            gameMapLabels[myMap.GameMap._Hero.getX(), myMap.GameMap._Hero.getY()].Text = IdentityToStringConverter(myMap.GameMap._Hero.I);
            gameMapLabels[myMap.GameMap._Hero.getX() + 1, myMap.GameMap._Hero.getY()].Text = IdentityToStringConverter(myMap.GameMap.Mymap[myMap.GameMap._Hero.getX() + 1, myMap.GameMap._Hero.getY()].I);
            gameMapLabels[myMap.GameMap._Hero.getX(), myMap.GameMap._Hero.getY()].ForeColor = System.Drawing.Color.Black;
            heroStats.Text = myMap.GameMap._Hero.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            myMap.MovePlayer(MovementEnum.Right);
            gameMapLabels[myMap.GameMap._Hero.getX(), myMap.GameMap._Hero.getY()].Text = IdentityToStringConverter(myMap.GameMap._Hero.I);
            gameMapLabels[myMap.GameMap._Hero.getX() - 1, myMap.GameMap._Hero.getY()].Text = IdentityToStringConverter(myMap.GameMap.Mymap[myMap.GameMap._Hero.getX() - 1, myMap.GameMap._Hero.getY()].I);
            gameMapLabels[myMap.GameMap._Hero.getX(), myMap.GameMap._Hero.getY()].ForeColor = System.Drawing.Color.Black;
            heroStats.Text = myMap.GameMap._Hero.ToString();
        }

        // Hero game controls end here ###############################################################

        public Label characterLable(string stats)
        {
            Label myLabel = new Label();
            myLabel.Text = stats;
            myLabel.ForeColor = Color.Red;
            myLabel.BackColor = Color.White;
            myLabel.Location = new Point(500, 380);
            myLabel.Width = 70;
            myLabel.Height = 90;
            myLabel.TextAlign = ContentAlignment.MiddleCenter;


            return myLabel;
        }

        public void LoadCharacterStats()
        {
            heroStats = characterLable(myMap.GameMap._Hero.ToString());
            enemyStats[0] = characterLable(myMap.GameMap.Enemies[0].ToString() + "\n HP: " + myMap.GameMap.Enemies[0].getHP().ToString() + " / " + myMap.GameMap.Enemies[0].getMaxHP().ToString()); enemyStats[0].Location = new Point(465, 10); enemyStats[0].ForeColor = System.Drawing.Color.DeepPink; enemyStats[0].Width = 130; enemyStats[0].Height = 45;
            enemyStats[1] = characterLable(myMap.GameMap.Enemies[1].ToString() + "\n HP: " + myMap.GameMap.Enemies[1].getHP().ToString() + " / " + myMap.GameMap.Enemies[1].getMaxHP().ToString()); enemyStats[1].Location = new Point(465, 60); enemyStats[1].ForeColor = System.Drawing.Color.Blue; enemyStats[1].Width = 130; enemyStats[1].Height = 45;
            enemyStats[2] = characterLable(myMap.GameMap.Enemies[2].ToString() + "\n HP: " + myMap.GameMap.Enemies[2].getHP().ToString() + " / " + myMap.GameMap.Enemies[2].getMaxHP().ToString()); enemyStats[2].Location = new Point(715, 10); enemyStats[2].ForeColor = System.Drawing.Color.Green; enemyStats[2].Width = 130; enemyStats[2].Height = 45;
            enemyStats[3] = characterLable(myMap.GameMap.Enemies[3].ToString() + "\n HP: " + myMap.GameMap.Enemies[3].getHP().ToString() + " / " + myMap.GameMap.Enemies[3].getMaxHP().ToString()); enemyStats[3].Location = new Point(715, 60); enemyStats[3].ForeColor = System.Drawing.Color.Red; enemyStats[3].Width = 130; enemyStats[3].Height = 45;
            Controls.Add(heroStats); Controls.Add(enemyStats[0]); Controls.Add(enemyStats[1]); Controls.Add(enemyStats[2]); Controls.Add(enemyStats[3]);
        }

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
        }

        // Enemy shoot buttoms end here ############################# 

        
    }
}
