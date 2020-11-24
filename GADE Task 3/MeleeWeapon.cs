using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE_Task_3
{

    class MeleeWeapon : Weapon
    {

        public enum Types { Dagger, Longsword }; public Types Type;

        public MeleeWeapon(int xPos, int yPos, Types type) : base(TileType.meleeWeapon, xPos, yPos)
        {
            Type = type; if (type == Types.Dagger)
            { weaponType = "Dagger"; durability = 10; damage = 13; cost = 3; }
            else { weaponType = "Longsword"; durability = 6; damage = 6; cost = 5; }
        }
        public override int Range { get { return 1; } set { range = 1; } }

        public override string ToString()
        {
            return weaponType; // implementation requirement not yet specified
        }
    }
}
