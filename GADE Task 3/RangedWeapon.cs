using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE_Task_3
{
    class RangedWeapon : Weapon
    {
        public enum Types { Rifle, Longbow }; public Types Type;

        public override int Range { get { return base.Range; } set { range = value; } }

        public RangedWeapon(Types type, int xPos, int yPos) : base(TileType.rangedWeapon, xPos, yPos)
        {
            if (type == Types.Rifle)
            {
                weaponType = "Rifle"; durability = 3; range = 3; damage = 5; cost = 7;
            }
            else
            {
                weaponType = "Longbow"; durability = 4; range = 2; damage = 4; cost = 6;
            }
            Type = type;
        }

        public RangedWeapon(Types type, int _durability) : base(TileType.rangedWeapon)
        {
            if (type == Types.Rifle)
            {
                weaponType = "Rifle"; _durability = 3; range = 3; damage = 5; cost = 7;
            }
            else
            {
                weaponType = "Longbow"; _durability = 4; range = 2; damage = 4; cost = 6;
            }
            Type = type;
        }

        public override string ToString()
        {
            return weaponType; // implementation requirement not yet specified
        }
    }
}
