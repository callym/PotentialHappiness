﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PotentialHappiness.Screens;
using PotentialHappiness.Map.Cells;
using PotentialHappiness.Map.Generators;
using PotentialHappiness.Map.Areas;
using PotentialHappiness.GameObjects;

namespace PotentialHappiness.Map
{
	public class TileMap
	{
		public GameScreen Screen;
		public List<MapRow> Rows = new List<MapRow>();
		public int MapWidth = 64;
		public int MapHeight = 64;

		public GameList<Feature> Features = new GameList<Feature>();
		public GameList<GameObject> GameObjects = new GameList<GameObject>();

		public MapGenerator Generator;

		public TileMap(GameScreen screen)
		{
			Screen = screen;
			for (int y = 0; y < MapHeight; y++)
			{
				MapRow thisRow = new MapRow();
				for (int x = 0; x < MapWidth; x++)
				{
					MapCell newCell = new MapCell(Color.Black, x, y, this);
					thisRow.Columns.Add(newCell);
				}
				Rows.Add(thisRow);
			}

			Generator = new DungeonGenerator();
			Generator.Generate(this);

			if (!MapManager.Instance.Maps.Contains(this))
			{
				MapManager.Instance.Maps.Add(this);
			}
		}

		public bool IsVisible(int x, int y)
		{
			int border = Camera.Instance.BorderSize / 2;
			int screenSize = ScreenManager.Instance.VirtualScreenSize;
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
			GameObjects.ForEach((o) =>
			{
				o.Update(gameTime);
			});
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();
			ForEach((c) =>
			{
				if (IsVisible(c.Pixel.X, c.Pixel.Y))
				{
					c.Draw(spriteBatch);
				}
			});
			GameObjects.ForEach((o) =>
			{
				if (o is PixelGameObject)
				{
					PixelGameObject p = o as PixelGameObject;
					if (IsVisible(p.Pixel.X, p.Pixel.Y))
					{
						p.Draw(spriteBatch);
					}
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
	}
}
