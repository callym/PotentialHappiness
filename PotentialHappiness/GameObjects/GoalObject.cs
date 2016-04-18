using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PotentialHappiness.Characters;
using PotentialHappiness.Components;
using PotentialHappiness.Map;
using PotentialHappiness.Screens;

namespace PotentialHappiness.GameObjects
{
	public class GoalObject : PixelGameObject
	{
		static int numberOfGoals = 3;
		static int currentNumber = 0;
		public GoalObject(Color color, TileMap map) : base(color, map)
		{

		}

		public GoalObject(Color color, int x, int y, TileMap map) : base(color, x, y, map)
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
					currentNumber++;
					if (currentNumber >= numberOfGoals)
					{
						ScreenManager.Instance.ChangeScreens(MapManager.Instance.CurrentMap.Screen, new EndGameScreen(true));
					}
					this.unload = true;
				}
			});
		}
	}
}
