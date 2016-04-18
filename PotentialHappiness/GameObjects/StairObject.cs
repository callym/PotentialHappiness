using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PotentialHappiness.Characters;
using PotentialHappiness.Components;
using PotentialHappiness.Map;
using PotentialHappiness.Map.Areas;

namespace PotentialHappiness.GameObjects
{
	public class StairObject : PixelGameObject
	{
		bool Up = RandomManager.Instance.Next(0, 99) > 49;
		public StairObject(TileMap map) : base(Color.LightSeaGreen, map)
		{

		}

		public StairObject(int x, int y, TileMap map) : base(Color.LightSeaGreen, x, y, map)
		{

		}

		protected override void Init()
		{
			base.Init();

			CollisionComponent collision = new CollisionComponent(this);
			collision.AddEvent(typeof(PlayableCharacter), false, (o, e) =>
			{
				if (o == CharacterManager.Instance.CurrentCharacter)
				{
					TileMap newMap;
					if ((!Up || MapManager.Instance.Maps.Count <= 1) && !GoalManager.Instance.AllGoalsPlaced())
					{
						newMap = new TileMap(MapManager.Instance.CurrentMap.Screen);
					}
					else
					{
						do
						{
							newMap = MapManager.Instance.Maps[RandomManager.Instance.Next(MapManager.Instance.Maps.Count)];
						}
						while (newMap == Map);
					}

					(o as PlayableCharacter).Map = newMap;
					Room startingRoom = newMap.Features.Find((f) => f is Room) as Room;
					(o as PlayableCharacter).SetPosition(startingRoom.Bounds.Center.X, startingRoom.Bounds.Center.Y);

					MapManager.Instance.CurrentMap = newMap;
				}
			});
		}
	}
}
