using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PotentialHappiness.Map.Cells;

namespace PotentialHappiness.Map.Areas
{
	public class Area : Feature
	{
		public Rectangle Bounds { get; set; }

		public Area(TileMap map) : base(map)
		{

		}

		public Area(Rectangle bounds, TileMap map) : this(map)
		{
			Bounds = bounds;
		}

		public void Recalculate()
		{
			List<MapCell> newCells = new List<MapCell>();
			Map.ForEach((c) =>
			{
				if (Bounds.Contains(c.X, c.Y))
				{
					newCells.Add(c);
				}
			});

			Clear();
			Add(newCells, CellType.Floor);
		}
	}
}
