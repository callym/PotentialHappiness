using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PotentialHappiness.AStar;
using PotentialHappiness.Characters;
using PotentialHappiness.GameObjects;
using PotentialHappiness.Map.Areas;
using PotentialHappiness.Map.Cells;
using static PotentialHappiness.Extensions.LINQExtensions;

namespace PotentialHappiness.Map.Generators
{
	public class DungeonGenerator : MapGenerator
	{
		List<Room> rooms = new List<Room>();
		List<Corridor> corridors = new List<Corridor>();
		int roomCount = RandomManager.Instance.Next(10, 20);
		int minSize = 10;
		int maxSize = 20;

		public override void Generate(TileMap map)
		{
			base.Generate(map);

			GenerateRooms();

			GenerateStairs();

			GenerateGems();

			GenerateCorridors();

			Map.Features.AddRange(rooms);
			Map.Features.AddRange(corridors);
		}

		public override void PlaceCharacter(PlayableCharacter c)
		{
			Room startingRoom = rooms.First();
			c.SetPosition(startingRoom.Bounds.Center.X, startingRoom.Bounds.Center.Y);
			GoalObject goal = new GoalObject(Color.FloralWhite, Map);
			Room endRoom = FindFurthestRoom(startingRoom);
			goal.X = endRoom.Bounds.Center.X;
			goal.Y = endRoom.Bounds.Center.Y;
		}

		void GenerateRooms()
		{
			for (int i = 0; i < roomCount; i++)
			{
				Room newRoom = CreateRoom();
				if (newRoom != null)
				{
					rooms.Add(newRoom);
				}
			}

			rooms.ForEach((r) =>
			{
				int shrink = -1;
				Rectangle shrunken = new Rectangle(r.Bounds.Location, r.Bounds.Size);
				shrunken.Inflate(shrink, shrink);
				r.Bounds = shrunken;
				r.Recalculate();
			});
		}

		void GenerateCorridors()
		{
			List<Room> doneRooms = new List<Room>();
			rooms.ForEach((r) => CreateCorridor(r, doneRooms));
			rooms.ForEach((r) => CreateCorridor(r));
		}

		void GenerateStairs()
		{
			List<Room> addedStairs = new List<Room>();
			for (int i = 0; i <= 3; i++)
			{
				Room r = rooms[RandomManager.Instance.Next(rooms.Count - 1)];
				while (addedStairs.Contains(r))
				{
					r = rooms[RandomManager.Instance.Next(rooms.Count - 1)];
				}
				StairObject stairs = new StairObject(Color.LightPink, Map);
				MapCell c = r.Cells[RandomManager.Instance.Next(r.Cells.Count - 1)];
				stairs.X = c.X;
				stairs.Y = c.Y;
			}
		}

		void GenerateGems()
		{
			int maxTotal = 100;
			int maxRoom = maxTotal / rooms.Count;
			int current = 0;
			int currentRoom = 0;

			rooms.ForEach((r) =>
			{
				r.Cells.ForEach((c) =>
				{
					if (RandomManager.Instance.Next(100) < 10)
					{
						GemObject gem = new GemObject(Color.Yellow, c.X, c.Y, Map);
						current++;
						currentRoom++;
						Program.Log($"Created Gem at ({c.X}, {c.Y})");
					}
				});
			});
		}

		void CreateCorridor(Room r, List<Room> doneRooms = null)
		{
			Room closestRoom = FindClosestRoom(r, doneRooms);

			List<MapCell> cells = TileMapAStar.Instance.Pathfind(
				Map[r.Bounds.Center.X, r.Bounds.Center.Y],
				Map[closestRoom.Bounds.Center.X, closestRoom.Bounds.Center.Y],
				Map);

			doneRooms?.Add(r);

			Corridor corridor = new Corridor(Map);
			corridor.Add(cells, CellType.Floor);

			corridors.Add(corridor);
		}

		Room CreateRoom()
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

			if (room.HasValue && IsValid(room))
			{
				Room successfulRoom = new Room(Map);
				successfulRoom.Color = Color.DimGray;
				successfulRoom.Bounds = room.Value;
				return successfulRoom;
			}

			return null;
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
				if (r.Bounds.Intersects(room.Value) || r.Bounds.Contains(room.Value))
				{
					b = false;
				}
			});

			return b;
		}

		Room FindClosestRoom(Room room, List<Room> notTheseRooms = null)
		{
			Room closest = room;
			int distance = 1000;

			rooms.ForEach((r) =>
			{
				if (r != room && (!notTheseRooms?.Contains(r) ?? true))
				{
					int thisDistance = Math.Abs(room.Bounds.Center.X - r.Bounds.Center.X) + Math.Abs(room.Bounds.Center.Y - r.Bounds.Center.Y);

					if (thisDistance < distance)
					{
						distance = thisDistance;
						closest = r;
					}
				}
			});

			return closest;
		}

		Room FindFurthestRoom(Room room)
		{
			Room closest = room;
			int distance = 0;

			rooms.ForEach((r) =>
			{
				if (r != room)
				{
					int thisDistance = Math.Abs(room.Bounds.Center.X - r.Bounds.Center.X) + Math.Abs(room.Bounds.Center.Y - r.Bounds.Center.Y);

					if (thisDistance > distance)
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
