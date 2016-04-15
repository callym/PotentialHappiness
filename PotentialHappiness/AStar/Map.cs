using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PotentialHappiness.Map;
using PotentialHappiness.Map.Areas;

namespace PotentialHappiness.AStar
{
	public class Map
	{
		int Width;
		int Height;

		int[] mapArray;

		public Map(TileMap map)
		{
			Width = map.MapWidth;
			Height = map.MapHeight;
			mapArray = new int[Width * Height];
			
			for (int i = 0; i < mapArray.Count(); i++)
			{
				mapArray[i] = 1;
			}

			map.ForEach((c) =>
			{
				if (c.Feature is Corridor)
				{
					SetMap(c.X, c.Y, 1);
				}
				else if (c.Feature is Room)
				{
					SetMap(c.X, c.Y, 1);
				}
				else if (c.Feature == null)
				{
					SetMap(c.X, c.Y, 4);
				}
			});
		}

		public bool InMap(int x, int y)
		{
			if (x < 0 || x >= Width ||
				y < 0 || y >= Height)
			{
				return false;
			}
			return true;
		}

		public int GetMap(int x, int y)
		{
			if (!InMap(x, y))
			{
				return 9; //impassable!
			}

			return mapArray[(y * Width) + x];
		}

		public void SetMap(int x, int y, int value)
		{
			if (InMap(x, y))
			{
				mapArray[(y * Width) + x] = value;
			}
		}
	}
}
