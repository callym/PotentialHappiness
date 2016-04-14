using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PotentialHappiness.Screens;
using PotentialHappiness.Map.Cells;

namespace PotentialHappiness.Map
{
	public class TileMap
	{
		public List<MapRow> Rows = new List<MapRow>();
		public int MapWidth = 64;
		public int MapHeight = 64;

		Random r = new Random();

		public TileMap()
		{
			for (int y = 0; y < MapHeight; y++)
			{
				MapRow thisRow = new MapRow();
				for (int x = 0; x < MapWidth; x++)
				{
					MapCell newCell = new MapCell(Color.LightSeaGreen, x, y);
					thisRow.Columns.Add(newCell);
				}
				Rows.Add(thisRow);
			}

			GenerateDungeon();

			MapManager.Instance.Maps.Add(this);
		}

		public bool IsVisible(int x, int y)
		{
			int border = Camera.Instance.BorderSize / 2;
			int screenSize = ScreenManager.Instance.VirtualScreenSize / Camera.Instance.Scale;
			int cameraX = Math.Abs(Camera.Instance.X) - border;
			int cameraY = Math.Abs(Camera.Instance.Y) - border;

			int cameraWidth = cameraX + screenSize + border;
			int cameraHeight = cameraY + screenSize + border;

			if ((x >= cameraX && x < cameraWidth) &&
				(y >= cameraY && y < cameraHeight))
			{
				return true;
			}

			return false;
		}

		public bool IsInMap(int x, int y)
		{
			if (y < MapHeight && y > -1)
			{
				if (x < MapWidth && x > -1)
				{
					return true;
				}
			}
			return false;
		}

		public bool IsInMap(MapCell cell) => IsInMap(cell.X, cell.Y);

		public void Update(GameTime gameTime)
		{

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Begin(transformMatrix: Camera.Instance.ScaleMatrix);
			ForEach((c) =>
			{
				if (IsVisible(c.X, c.Y))
				{
					c.Draw(spriteBatch);
				}
			});
			spriteBatch.End();
		}

		public MapCell this[int x, int y] => Rows[y].Columns[x];

		public void ForEach(Action<MapCell> action)
		{
			for (int y = 0; y < MapHeight; y++)
			{
				for (int x = 0; x < MapWidth; x++)
				{
					action(this[x, y]);
				}
			}
		}

		void GenerateDungeon()
		{
			MarkAllUnvisited();

			//GenerateRooms();

			ForEach((c) => c.Pixel.Color = c.Visited ? Color.White : Color.Black);
		}

		void GenerateRooms()
		{
			int numberOfTries = r.Next(5, 10);
			
			for (int i = 0; i < numberOfTries; i++)
			{
				int width = r.Next(15, 45);
				int height = r.Next(15, 45);

				int x = r.Next(1, MapWidth - width - 1);
				int y = r.Next(1, MapHeight - height - 1);

				ForEach((c) =>
				{
					if (c.X > (x) && c.X < (x + width) &&
						c.Y > (y) && c.Y < (y + height))
					{
						c.Visited = true;
					}
				});
			}
		}

		List<MapCell> GetAdjacentCells(MapCell cell)
		{
			List<MapCell> cells = new List<MapCell>();

			int x = cell.X;
			int y = cell.Y;

			if (IsInMap(this[x + 1, y]))
			{
				cells.Add(this[x + 1, y]);
			}
			if (IsInMap(this[x - 1, y]))
			{
				cells.Add(this[x - 1, y]);
			}
			if (IsInMap(this[x, y + 1]))
			{
				cells.Add(this[x, y + 1]);
			}
			if (IsInMap(this[x, y - 1]))
			{
				cells.Add(this[x, y - 1]);
			}

			return cells;
		}

		void MarkAllUnvisited() => ForEach((c) => c.Visited = false);
	}
}
