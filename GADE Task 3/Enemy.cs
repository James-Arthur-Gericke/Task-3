using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE_Task_3
{
    abstract class Enemy : Character
    {
        protected Random R;

        public Enemy(int x, int y, TileType i, int d, int h, int hm) : base(x, y, i) { setDamage(d); setHP(h); setMaxHP(hm); R = new Random(); }

        public abstract override MovementEnum ReturnMove(MovementEnum move = 0);

        public override string ToString()
        {
            return I.ToString() + " at [" + getX().ToString() + ", " + getY().ToString() + "] (" + getDamage().ToString() + " DMG)";
        }
    }

}


