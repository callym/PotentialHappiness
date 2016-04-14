using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PotentialHappiness.GameObjects;
using PotentialHappiness.Map.Areas;

namespace PotentialHappiness.Map.Cells
{
	public class MapCell : PixelGameObject
	{
		public bool Visited { get; set; } = false;

		public TileMap Map = null;

		public Feature Feature = null;

		public MapCell(Color cellColor, int x, int y, TileMap map) : base(cellColor, x, y)
		{
			Map = map;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			Color color = Feature?.Color ?? Pixel.Color;
			spriteBatch.Draw(
				Pixel.Texture,
				new Rectangle(Camera.Instance.ToCamera(X, Y), new Point(1, 1)),
				color);
			//base.Draw(spriteBatch);
		}

		public MapCell GetNeighbour(Directions direction)
		{
			MapCell neighbour = null;
			switch (direction)
			{
				case Directions.East:
					neighbour = Map.IsInMap(X + 1, Y) ? Map[X + 1, Y] : null;
					break;
				case Directions.West:
					neighbour = Map.IsInMap(X - 1, Y) ? Map[X - 1, Y] : null;
					break;
				case Directions.North:
					neighbour = Map.IsInMap(X, Y + 1) ? Map[X, Y + 1] : null;
					break;
				case Directions.South:
					neighbour = Map.IsInMap(X, Y - 1) ? Map[X, Y - 1] : null;
					break;
				default:
					break;
			}
			return neighbour;
		}

		public List<MapCell> GetNeighbours()
		{
			List<MapCell> cells = new List<MapCell>();
			foreach (Directions d in Enum.GetValues(typeof(Directions)))
			{
				MapCell c = GetNeighbour(d);
				if (c != null)
				{
					cells.Add(c);
				}
			}
			return cells;
		}
	}
}
