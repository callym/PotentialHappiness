using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PotentialHappiness.Map.Areas
{
	public class Room : Area
	{
		public Room(TileMap map) : base(map)
		{
			Priority = 5;
		}

		public Room(Rectangle bounds, TileMap map) : base(bounds, map)
		{
			Priority = 5;
		}
	}
}
