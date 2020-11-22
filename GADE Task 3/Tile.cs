using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE_Task_3
{

	public enum TileType
	{ hero, weapon, gold, enemy, obstacle, empty_tile, goblin }


	class Tile
	{

		protected int X, Y;
		public TileType I;

		public void setX(int Value) { X = Value; }
		public void setY(int Value) { Y = Value; }
		public int getX() { return X; }
		public int getY() { return Y; }
		public Tile(int x, int y, TileType i) { setX(x); setY(y); I = i; }

	}
}
