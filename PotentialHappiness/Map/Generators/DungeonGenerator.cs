using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using static PotentialHappiness.Extensions.LINQExtensions;

namespace PotentialHappiness.Map.Generators
{
	public class DungeonGenerator : MapGenerator
	{
		List<Rectangle> rooms = new List<Rectangle>();
		int roomCount = RandomManager.Instance.Next(10, 20);
		int minSize = 10;
		int maxSize = 20;

		public override void Generate(TileMap map)
		{
			base.Generate(map);

			GenerateRooms();

			rooms.ForEach((r) =>
			{
				Color color = RandomManager.Instance.Color(Extensions.GraphicsExtensions.ColorTypes.Pastel);
				Map.ForEach((c) =>
				{
					if (r.Contains(c.X, c.Y))
					{
						c.Pixel.Color = color;
					}
				});
			});
		}

		void GenerateRooms()
		{
			for (int i = 0; i < roomCount; i++)
			{
				Rectangle newRoom = CreateRoom();
				rooms.Add(newRoom);
			}

			rooms.ForEachIndex((r, i) =>
			{
				int shrink = -1;
				r.Inflate(shrink, shrink);
				rooms[i] = r;
			});
		}

		Rectangle CreateRoom()
		{
			Rectangle? room = null;

			int maxTries = 100;
			int current = 0;

			while (!IsValid(room) && (current < maxTries))
			{
				Rectangle tryRoom = new Rectangle();
				tryRoom.X = RandomManager.Instance.Next(1, Map.MapWidth - maxSize - 1);
				tryRoom.Y = RandomManager.Instance.Next(1, Map.MapHeight - maxSize - 1);
				tryRoom.Width = RandomManager.Instance.Next(minSize, maxSize);
				tryRoom.Height = RandomManager.Instance.Next(minSize, maxSize);
				room = tryRoom;
				current++;
			}

			Rectangle successfulRoom;
			if (room.HasValue && IsValid(room))
			{
				successfulRoom = room.Value;
			}
			else
			{
				// for now!! just make the room the first in the list
				// needs to change when rooms become actual entities?
				successfulRoom = rooms[0];
			}

			return successfulRoom;
		}

		bool IsValid(Rectangle? room)
		{
			bool b = true;

			if (!room.HasValue)
			{
				return false;
			}

			rooms.ForEach((r) =>
			{
				if (r.Intersects(room.Value) || r.Contains(room.Value))
				{
					b = false;
				}
			});

			return b;
		}
	}
}
