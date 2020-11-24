using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE_Task_3
{
    abstract class Weapon : Item
    {
        protected int damage, range, durability, cost;
        protected string weaponType;

        public Weapon(TileType i, int xPos = 0, int yPos = 0) : base(xPos, yPos) { I = i; }
        public int Damage { get { return damage; } set { damage = value; } }
        public virtual int Range { get { return range; } set { range = value; } }
        public int Durability { get { return durability; } set { durability = value; } }
        public int Cost { get { return cost; } set { cost = value; } }
        public string WeaponType { get { return weaponType; } set { weaponType = value; } }
    }
}
