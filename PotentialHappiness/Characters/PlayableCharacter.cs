using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PotentialHappiness.Components;
using PotentialHappiness.Map;

namespace PotentialHappiness.Characters
{
	public class PlayableCharacter : Character
	{
		public PlayableCharacter(string name, Color playerColor) : base(name, playerColor)
		{
			InputComponent input = new InputComponent(this);
			input.AddEvent(Keys.Left, Input.Held, (o, e) => { this.ChangePosition(-Speed, 0); });
			input.AddEvent(Keys.Right, Input.Held, (o, e) => { this.ChangePosition(Speed, 0); });
			input.AddEvent(Keys.Up, Input.Held, (o, e) => { this.ChangePosition(0, -Speed); });
			input.AddEvent(Keys.Down, Input.Held, (o, e) => { this.ChangePosition(0, Speed); });
		}

		public override void Update(GameTime gameTime)
		{
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
