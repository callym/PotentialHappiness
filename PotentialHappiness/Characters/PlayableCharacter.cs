using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PotentialHappiness.Components;
using PotentialHappiness.GameObjects;
using PotentialHappiness.Map;

namespace PotentialHappiness.Characters
{
	public class PlayableCharacter : Character
	{
		int moving = 0;
		Directions movingDirection;

		public PlayableCharacter(string name, Color playerColor, TileMap map) : base(name, playerColor, map)
		{
			InputComponent input = new InputComponent(this);
			input.AddEvent(Keys.Left, Input.Held, (o, e) => { this.ChangePosition(-Speed, 0); });
			input.AddEvent(Keys.Right, Input.Held, (o, e) => { this.ChangePosition(Speed, 0); });
			input.AddEvent(Keys.Up, Input.Held, (o, e) => { this.ChangePosition(0, -Speed); });
			input.AddEvent(Keys.Down, Input.Held, (o, e) => { this.ChangePosition(0, Speed); });

			CollisionComponent collision = new CollisionComponent(this);
			collision.AddEvent(Map.Cells.CellType.Wall, true, (o, e) => Program.Log("Collided"));
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

		public Tuple<bool, List<MapObject>> CanMove(int x, int y)
		{
			bool canMove = true;
			List<MapObject> collidesWith = new List<MapObject>();
			if (MapManager.Instance.CurrentMap.IsInMap(x, y))
			{
				List<Component> collisionComponents = GetComponents(typeof(CollisionComponent));
				collisionComponents.ForEach((comp) =>
				{
					CollisionComponent c = comp as CollisionComponent;
					bool clear = c.CanMove(MapManager.Instance.CurrentMap[x, y].Type);
					if (!clear)
					{
						canMove = false;
					}
					collidesWith.Add(MapManager.Instance.CurrentMap[x, y]);
				});
				MapManager.Instance.CurrentMap.GameObjects.ForEach((o) =>
				{
					if (o is MapObject)
					{
						MapObject m = o as MapObject;
						if (m.X == x && m.Y == y)
						{
							collisionComponents.ForEach((comp) =>
							{
								CollisionComponent c = comp as CollisionComponent;
								bool clear = c.CanMove(m.GetType());
								if (!clear)
								{
									canMove = false;
								}
							});
							collidesWith.Add(m);
						}
					}
				});
			}
			return new Tuple<bool, List<MapObject>>(canMove, collidesWith);
		}

		void DoCollide(List<MapObject> collidesWith)
		{
			GetComponents(typeof(CollisionComponent)).ForEach((comp) =>
			{
				CollisionComponent c = comp as CollisionComponent;
				c.OnCollide(collidesWith);
			});
		}

		public void SetPosition(int newX, int newY)
		{
			var move = CanMove(newX, newY);
			if (move.Item1)
			{
				Y = newY;
				X = newX;
			}
			DoCollide(move.Item2);
		}

		public void ChangePosition(int newX = 0, int newY = 0, bool smooth = true)
		{
			var move = CanMove(X + newX, Y + newY);
			if (smooth && moving == 0 && move.Item1)
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
			DoCollide(move.Item2);
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
