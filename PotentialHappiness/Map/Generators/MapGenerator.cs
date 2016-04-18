using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PotentialHappiness.Characters;
using PotentialHappiness.Map.Cells;

namespace PotentialHappiness.Map.Generators
{
	public class MapGenerator
	{
		protected TileMap Map;
		public virtual void Generate(TileMap map)
		{
			Map = map;
		}
	}
}
