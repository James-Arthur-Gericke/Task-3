using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE_Task_3
{

	public enum TileType
	{ hero, weapon, gold, enemy, obstacle, empty_tile, goblin, item, mage, meleeWeapon, rangedWeapon, leader }

	class Tile
	{
		protected int X, Y;
		public TileType I;

		public Tile(int xPos, int yPos, TileType i) { X = xPos; Y = yPos; I = i; }
		public void setX(int Value) { X = Value; }
		public void setY(int Value) { Y = Value; }
		public int getX() { return X; }
		public int getY() { return Y; }
	}
}
