using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PotentialHappiness.Map;

namespace PotentialHappiness.Characters
{
	public class PlayableCharacter : Character
	{
		public PlayableCharacter(string name, Color playerColor) : base(name, playerColor)
		{

		}

		public override void Update(GameTime gameTime)
		{
			KeyboardState ks = Keyboard.GetState();
			if (ks.IsKeyDown(Keys.Left))
			{
				ChangePosition(-Speed, 0);
			}
			if (ks.IsKeyDown(Keys.Right))
			{
				ChangePosition(Speed, 0);
			}
			if (ks.IsKeyDown(Keys.Up))
			{
				ChangePosition(0, -Speed);
			}
			if (ks.IsKeyDown(Keys.Down))
			{
				ChangePosition(0, Speed);
			}
			Camera.Instance.SetPosition(X, Y);

			base.Update(gameTime);
		}

		public void SetPosition(int newX, int newY)
		{
			if (MapManager.Instance.CurrentMap.IsInMap(X, newY))
			{
				Y = newY;
			}
			if (MapManager.Instance.CurrentMap.IsInMap(newX, Y))
			{
				X = newX;
			}
		}

		public void ChangePosition(int newX = 0, int newY = 0)
		{
			SetPosition(X + newX, Y + newY);
		}
	}
}
