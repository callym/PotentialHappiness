using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PotentialHappiness.Map.Cells;

namespace PotentialHappiness.Map.Generators
{
	public class MapGenerator
	{
		TileMap Map;
		public virtual void Generate(TileMap map)
		{
			Map = map;

			Map.ForEach((c) => c.Pixel.Color = c.Visited ? Color.White : Color.Black);
		}

		List<MapCell> GetAdjacentCells(MapCell cell)
		{
			List<MapCell> cells = new List<MapCell>();

			int x = cell.X;
			int y = cell.Y;

			if (Map.IsInMap(Map[x + 1, y]))
			{
				cells.Add(Map[x + 1, y]);
			}
			if (Map.IsInMap(Map[x - 1, y]))
			{
				cells.Add(Map[x - 1, y]);
			}
			if (Map.IsInMap(Map[x, y + 1]))
			{
				cells.Add(Map[x, y + 1]);
			}
			if (Map.IsInMap(Map[x, y - 1]))
			{
				cells.Add(Map[x, y - 1]);
			}

			return cells;
		}

		void MarkAllUnvisited() => Map.ForEach((c) => c.Visited = false);
	}
}
