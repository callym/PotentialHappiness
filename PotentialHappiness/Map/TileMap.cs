using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PotentialHappiness.Map
{
	public class TileMap
	{
		public List<MapRow> Rows = new List<MapRow>();
		public int MapWidth = 64;
		public int MapHeight = 64;

		public TileMap()
		{
			for (int y = 0; y < MapHeight; y++)
			{
				MapRow thisRow = new MapRow();
				for (int x = 0; x < MapWidth; x++)
				{
					MapCell newCell = new MapCell(Color.LightSeaGreen, x, y);
					thisRow.Columns.Add(newCell);

					if (x == (MapWidth / 2))
					{
						newCell.Pixel.Color = Color.DeepPink;
					}
				}
				Rows.Add(thisRow);

				if (y == (MapHeight / 2))
				{
					Rows[y].Columns.ForEach(c => c.Pixel.Color = Color.Crimson);
				}
			}

			Rows[0].Columns[3].Pixel.Color = Color.Yellow;
			Rows[0].Columns[4].Pixel.Color = Color.Yellow;
			Rows[0].Columns[5].Pixel.Color = Color.Brown;
			Rows[0].Columns[6].Pixel.Color = Color.Brown;
			Rows[0].Columns[7].Pixel.Color = Color.Brown;

			Rows[1].Columns[3].Pixel.Color = Color.Yellow;
			Rows[1].Columns[4].Pixel.Color = Color.Brown;
			Rows[1].Columns[5].Pixel.Color = Color.Brown;
			Rows[1].Columns[6].Pixel.Color = Color.Brown;
			Rows[1].Columns[7].Pixel.Color = Color.Brown;

			Rows[2].Columns[2].Pixel.Color = Color.Yellow;
			Rows[2].Columns[3].Pixel.Color = Color.Brown;
			Rows[2].Columns[4].Pixel.Color = Color.Brown;
			Rows[2].Columns[5].Pixel.Color = Color.Brown;
			Rows[2].Columns[6].Pixel.Color = Color.Brown;
			Rows[2].Columns[7].Pixel.Color = Color.Brown;

			Rows[3].Columns[2].Pixel.Color = Color.Yellow;
			Rows[3].Columns[3].Pixel.Color = Color.Brown;
			Rows[3].Columns[4].Pixel.Color = Color.Brown;
			Rows[3].Columns[5].Pixel.Color = Color.Green;
			Rows[3].Columns[6].Pixel.Color = Color.Green;
			Rows[3].Columns[7].Pixel.Color = Color.Green;

			Rows[4].Columns[2].Pixel.Color = Color.Yellow;
			Rows[4].Columns[3].Pixel.Color = Color.Brown;
			Rows[4].Columns[4].Pixel.Color = Color.Brown;
			Rows[4].Columns[5].Pixel.Color = Color.Green;
			Rows[4].Columns[6].Pixel.Color = Color.Green;
			Rows[4].Columns[7].Pixel.Color = Color.Green;

			Rows[5].Columns[2].Pixel.Color = Color.Yellow;
			Rows[5].Columns[3].Pixel.Color = Color.Brown;
			Rows[5].Columns[4].Pixel.Color = Color.Brown;
			Rows[5].Columns[5].Pixel.Color = Color.Green;
			Rows[5].Columns[6].Pixel.Color = Color.Green;
			Rows[5].Columns[7].Pixel.Color = Color.Green;

			Rows[63].Columns[63].Pixel.Color = Color.Magenta;

			MapManager.Instance.Maps.Add(this);
		}

		public bool IsVisible(int x, int y)
		{
			int cameraX = Camera.Instance.X;
			int cameraY = Camera.Instance.Y;

			if (y < MapHeight && y > -1)
			{
				if (x < MapWidth && x > -1)
				{
					return true;
				}
			}
			return false;
		}

		public void Update(GameTime gameTime)
		{

		}

		public void Draw(SpriteBatch spriteBatch)
		{
/*			int visibleSquares = Camera.Instance.VisibleSquares;
			int cameraX = Camera.Instance.X;
			int cameraY = Camera.Instance.Y;*/

			spriteBatch.Begin(transformMatrix: Camera.Instance.ScaleMatrix);
			for (int y = 0; y < MapHeight; y++)
			{
				for (int x = 0; x < MapWidth; x++)
				{
					if (IsVisible(x, y))
					{
						Rows[y].Columns[x].Draw(spriteBatch);
					}
				}
			}
			spriteBatch.End();
		}
	}
}
