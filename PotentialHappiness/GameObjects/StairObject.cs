using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PotentialHappiness.Characters;
using PotentialHappiness.Components;
using PotentialHappiness.Map;

namespace PotentialHappiness.GameObjects
{
	public class StairObject : PixelGameObject
	{
		bool Up = RandomManager.Instance.Next(0, 99) > 49;
		public StairObject(Color color, TileMap map) : base(color, map)
		{

		}

		public StairObject(Color color, int x, int y, TileMap map) : base(color, x, y, map)
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
					if ((!Up || (MapManager.Instance.Maps.Count <= 1)) && !(GoalObject.GoalsPlaced.HasFlag(GoalObject.Types.Anger) &&
																			GoalObject.GoalsPlaced.HasFlag(GoalObject.Types.Anxiety) &&
																			GoalObject.GoalsPlaced.HasFlag(GoalObject.Types.Depression)))
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
					newMap.Generator.PlaceCharacter(o as PlayableCharacter);
					MapManager.Instance.CurrentMap = newMap;
				}
			});
		}
	}
}
