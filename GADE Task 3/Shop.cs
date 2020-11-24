using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE_Task_3
{
    class Shop
    {
        private Weapon[] weapons;
        private Random R;
        private Character Buyer;


        public Shop(Character buyer)
        {
            weapons = new Weapon[3]; R = new Random(Guid.NewGuid().GetHashCode());
            for (int index = 0; index < weapons.Length; index++) { weapons[index] = RandomWeapon(); }
        }
        public bool CanBuy(int num) { return Buyer.GoldPurse >= weapons[num].Cost; }
        public void Buy(int num)
        { Buyer.GoldPurse -= weapons[num].Cost; Buyer.Pickup(weapons[num]); weapons[num] = RandomWeapon(); }
        public string DisplayWeapon(int num) { return "Buy " + weapons[num].WeaponType + " (" + weapons[num].Cost.ToString() + " Gold)"; }

        private Weapon RandomWeapon()
        {
            Weapon answer; R = new Random(Guid.NewGuid().GetHashCode()); int randomSelect = R.Next(0, 5);

            if (randomSelect == 0) { answer = new RangedWeapon(RangedWeapon.Types.Rifle, 0, 0); }      // Create Riffle
            else if (randomSelect == 1) { answer = new RangedWeapon(RangedWeapon.Types.Longbow, 0, 0); }    // Create Longbow
            else if (randomSelect == 2) { answer = new MeleeWeapon(0, 0, MeleeWeapon.Types.Dagger); }      // Create Dagger
            else { answer = new MeleeWeapon(0, 0, MeleeWeapon.Types.Longsword); }    // Create Longsword

            return answer;
        }
    }
}
