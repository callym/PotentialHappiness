using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PotentialHappiness.GameObjects;

namespace PotentialHappiness.Map
{
	public class MapCell : PixelGameObject
	{
		public MapCell(Color cellColor, int x, int y) : base(cellColor, x, y)
		{

		}
	}
}
