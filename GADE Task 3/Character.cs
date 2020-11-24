using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE_Task_3
{

    enum MovementEnum { Up, Down, Left, Right, No_Movement, };
    enum VisionPosition { North, South, West, East };


    abstract class Character : Tile
    {

        public Character(int x, int y, TileType i) : base(x, y, i) { objectVision = new Tile[4]; goldPurse = 0; }

        protected int HP, MaxHP, Damage, goldPurse;

        protected Tile[] objectVision;

        public int GoldPurse { get { return goldPurse; } set { goldPurse = value; } }

        public void setHP(int Value) { HP = Value; }

        public void setMaxHP(int Value) { MaxHP = Value; }

        public void setDamage(int Value) { Damage = Value; }

        public int getVisionSize() { return objectVision.Length; }

        public void setVision(VisionPosition objPosition, Tile obj) { objectVision[(int)objPosition] = obj; }

        public int getHP() { return HP; }

        public int getMaxHP() { return MaxHP; }

        public int getDamage() { return Damage; }

        public Tile getVision(VisionPosition objPosition) { return objectVision[(int)objPosition]; }

        public virtual void Attack(Character target) { target.setHP(target.getHP() - getDamage()); }

        public bool isDead() { return HP <= 0; }

        public virtual bool CheckRange(Character target) { return DistanceTo(target) == 1; }

        private int DistanceTo(Character target)
        {
            int xPos = Math.Abs(target.getX() - getX()), yPos = Math.Abs(target.getY() - getY()); return xPos + yPos;
        }

        public void Pickup(Item i)
        {
            if (i.ToString() == TileType.gold.ToString()) { Gold tempGold = (Gold)i; goldPurse += tempGold.Amount; }
            // updates required for weapon updates
        }

        public void Move(MovementEnum move)
        {
            switch (move)
            {
                case MovementEnum.No_Movement: break;
                case MovementEnum.Up: Y--; break;
                case MovementEnum.Down: Y++; break;
                case MovementEnum.Left: X--; break;
                case MovementEnum.Right: X++; break;
            }
        }


        public abstract MovementEnum ReturnMove(MovementEnum move = 0);

        public abstract override string ToString();
    }
}
