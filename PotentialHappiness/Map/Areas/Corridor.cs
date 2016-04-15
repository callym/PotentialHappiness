using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PotentialHappiness.Map.Areas
{
	public class Corridor : Feature
	{
		public Corridor(TileMap map) : base(map)
		{
			Priority = 2;
			Color = Color.Beige;
		}
	}
}
