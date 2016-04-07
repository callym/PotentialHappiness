using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PotentialHappiness.Map
{
	public class MapCell : GameObject
	{
		public MapCell(Color cellColor, int x, int y) : base(cellColor, x, y)
		{

		}
	}
}
