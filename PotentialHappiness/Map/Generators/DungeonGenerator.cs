using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PotentialHappiness.AStar;
using PotentialHappiness.Map.Cells;
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

			GenerateCorridors();
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

		void GenerateCorridors()
		{
			List<Rectangle> doneRooms = new List<Rectangle>();
			rooms.ForEach((r) => CreateCorridor(r, doneRooms));
			rooms.ForEach((r) => CreateCorridor(r));
		}

		void CreateCorridor(Rectangle r, List<Rectangle> doneRooms = null)
		{
			Rectangle closestRoom = FindClosestRoom(r, doneRooms);

			List<MapCell> cells = TileMapAStar.Instance.Pathfind(
				Map[r.Center.X, r.Center.Y],
				Map[closestRoom.Center.X, closestRoom.Center.Y],
				Map);

			doneRooms?.Add(r);

			cells.ForEach((c) =>
			{
				c.Pixel.Color = Color.Beige;
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

		Rectangle FindClosestRoom(Rectangle room, List<Rectangle> notTheseRooms = null)
		{
			Rectangle closest = room;
			int distance = 1000;

			rooms.ForEach((r) =>
			{
				if (r != room && (!notTheseRooms?.Contains(r) ?? true))
				{
					int thisDistance = Math.Abs(room.Center.X - r.Center.X) + Math.Abs(room.Center.Y - r.Center.Y);

					if (thisDistance < distance)
					{
						distance = thisDistance;
						closest = r;
					}
				}
			});

			return closest;
		}
	}
}
