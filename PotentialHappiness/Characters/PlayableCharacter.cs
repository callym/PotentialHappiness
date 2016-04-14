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
		int moving = 0;
		Directions movingDirection;

		public PlayableCharacter(string name, Color playerColor) : base(name, playerColor)
		{
			InputComponent input = new InputComponent(this);
			input.AddEvent(Keys.Left, Input.Held, (o, e) => { this.ChangePosition(-Speed, 0); });
			input.AddEvent(Keys.Right, Input.Held, (o, e) => { this.ChangePosition(Speed, 0); });
			input.AddEvent(Keys.Up, Input.Held, (o, e) => { this.ChangePosition(0, -Speed); });
			input.AddEvent(Keys.Down, Input.Held, (o, e) => { this.ChangePosition(0, Speed); });
		}

		int RepeatTime = 50;
		int previousTime = 0;
		public override void Update(GameTime gameTime)
		{
			if (previousTime == 0)
			{
				previousTime = gameTime.TotalGameTime.Milliseconds;
			}

			if (Math.Abs(gameTime.TotalGameTime.Milliseconds - previousTime) > RepeatTime)
			{
				previousTime = gameTime.TotalGameTime.Milliseconds;
				if (moving > 0)
				{
					switch (movingDirection)
					{
						case Directions.North:
							Pixel.Y--;
							break;
						case Directions.East:
							Pixel.X++;
							break;
						case Directions.South:
							Pixel.Y++;
							break;
						case Directions.West:
							Pixel.X--;
							break;
						default:
							break;
					}
					moving--;
					if (moving == 0)
					{
						ChangePosition(movingDirection, false);
					}
				}
			}

			Camera.Instance.SetPosition(Pixel.X, Pixel.Y);

			base.Update(gameTime);
		}

		public bool CanMove(int x, int y)
		{
			if (MapManager.Instance.CurrentMap.IsInMap(x, y))
			{
				if (MapManager.Instance.CurrentMap[x, y].Feature != null)
				{
					return true;
				}
			}
			return false;
		}

		public void SetPosition(int newX, int newY)
		{
			if (CanMove(newX, newY))
			{
				Y = newY;
				X = newX;
			}
		}

		public void ChangePosition(int newX = 0, int newY = 0, bool smooth = true)
		{
			if (smooth && moving == 0 && CanMove(X + newX, Y + newY))
			{
				moving = Camera.Instance.Scale;
				if (newX > 0)
				{
					movingDirection = Directions.East;
				}
				else if (newX < 0)
				{
					movingDirection = Directions.West;
				}
				else if (newY > 0)
				{
					movingDirection = Directions.South;
				}
				else if (newY < 0)
				{
					movingDirection = Directions.North;
				}
			}
			else if (!smooth)
			{
				SetPosition(X + newX, Y + newY);
			}
		}

		void ChangePosition(Directions dir, bool smooth = true)
		{
			switch (dir)
			{
				case Directions.North:
					ChangePosition(newY: -1, smooth: smooth);
					break;
				case Directions.East:
					ChangePosition(newX: 1, smooth: smooth);
					break;
				case Directions.South:
					ChangePosition(newY: 1, smooth: smooth);
					break;
				case Directions.West:
					ChangePosition(newX: -1, smooth: smooth);
					break;
				default:
					break;
			}
		}
	}
}
