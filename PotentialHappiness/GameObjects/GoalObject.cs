using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PotentialHappiness.Characters;
using PotentialHappiness.Components;
using PotentialHappiness.Interface;
using PotentialHappiness.Map;
using PotentialHappiness.Screens;

namespace PotentialHappiness.GameObjects
{
	public class GoalObject : PixelGameObject
	{
		public string Message = "collected this strange gem";
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
				if (!unload && o == CharacterManager.Instance.CurrentCharacter)
				{
					unload = true;
					GoalManager.Instance.Current++;
					PopupTextBox box = new PopupTextBox(MapManager.Instance.CurrentMap.Screen);
					box.Text = Message;
					box.OnClose += (oo, ee) =>
					{
						Program.Log(Message);
						if (GoalManager.Instance.Current >= GoalManager.Instance.Aim)
						{
							ScreenManager.Instance.ChangeScreens(MapManager.Instance.CurrentMap.Screen, new EndGameScreen(true));
						}
					};
				}
			});
		}
	}
}
