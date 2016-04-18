using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PotentialHappiness.Components;
using PotentialHappiness.GameObjects;
using PotentialHappiness.Interface;
using PotentialHappiness.Map;
using PotentialHappiness.Map.Cells;
using PotentialHappiness.Screens;
using static PotentialHappiness.Extensions.GraphicsExtensions;

namespace PotentialHappiness.Characters
{
	public class PlayableCharacter : Character
	{
		int moving = 0;
		Directions movingDirection;

		public PlayableCharacter(string name, Color playerColor, TileMap map) : base(name, playerColor, map)
		{

		}
		protected override void Init()
		{
			InputComponent input = new InputComponent(this);
			input.AddEvent(Keys.Left, Input.Held, (o, e) =>
			{
				if (!Map.Screen.HasPopup)
				{
					this.ChangePosition(-Speed, 0);
					DoMessage();
				}
			});
			input.AddEvent(Keys.Right, Input.Held, (o, e) =>
			{
				if (!Map.Screen.HasPopup)
				{
					this.ChangePosition(Speed, 0);
					DoMessage();
				}
			});
			input.AddEvent(Keys.Up, Input.Held, (o, e) =>
			{
				if (!Map.Screen.HasPopup)
				{
					this.ChangePosition(0, -Speed);
					DoMessage();
				}
			});
			input.AddEvent(Keys.Down, Input.Held, (o, e) =>
			{
				if (!Map.Screen.HasPopup)
				{
					this.ChangePosition(0, Speed);
					DoMessage();
				}
			});

			CollisionComponent collision = new CollisionComponent(this);
			collision.AddEvent(CellType.Wall, true);
			collision.AddEvent(CellType.Floor, false, (o, e) =>
			{
				GetComponents(typeof(LevelComponent)).ForEach((comp) =>
				{
					LevelComponent c = comp as LevelComponent;
					if (c.Name == "Health")
					{
						c.CurrentLevel -= 5;
					}
				});
			});

			LevelComponent health = new LevelComponent("Health", this);
			health.MaxLevel = 1000;
			health.CurrentLevel = health.MaxLevel;
			health.DrawEvents += (o, e) =>
			{
				SpriteBatch s = o as SpriteBatch;
				Color c = Color.Black;
				switch (GoalManager.Instance.Current)
				{
					case 1: // rage
						c = Color.DarkRed;
						break;
					case 2: // despair
						c = new Color(73, 73, 112); //midnight blue desaturated - https://bgrins.github.io/TinyColor/ to hsv then reduce s then copy rgb
						break;
					case 3: //anxiety
						c = Color.Salmon;
						break;
					case 4: //joy - getting this ends the game!
						c = Color.SpringGreen;
						break;
					default:
						break;
				}
				c = c.ToAlpha(health.CurrentLevel / 1000f);
				if (health.CurrentLevel < 250 && flash)
				{
					c = c.ToAlpha(100f);
				}
				this.Pixel.Color = c;
			};
			health.OnMinLevel += (o, e) => ScreenManager.Instance.ChangeScreens(Map.Screen, new EndGameScreen(false));

			base.Init();
		}

		int cooldown = 0;
		void DoMessage()
		{
			List<string> messages = new List<string>()
			{
				"this is so... tiring...",
				"why do i even bother?",
				"haha even if i do get the gems, what then",
				"why did i even agree to do this",
				"my feet hurt so much",
				"what sort of wizard would even make these",
				"ugh i can't wait to get back into bed",
				"i'm going to fail anyway",
				"these gems have been lost for ages, why would i be able to find them",
				"i'm nothing special, why do i have to do this",
				"i bet they don't even exist",
				"i am not in the mood to be walking around this cave"
			};
			if (RandomManager.Instance.Next(100) < 5 && cooldown <= 0)
			{
				PopupTextBox ptb = new PopupTextBox(Map.Screen);
				ptb.Text = messages[RandomManager.Instance.Next(messages.Count)];
				cooldown = 100;
			}
			cooldown--;
		}

		int RepeatTime = 50;
		int previousTime = 0;
		int flashTime = 250;
		int previousFlashTime = 0;
		bool flash = false;
		public override void Update(GameTime gameTime)
		{
			if (previousTime == 0)
			{
				previousTime = gameTime.TotalGameTime.Milliseconds;
			}
			if (previousFlashTime == 0)
			{
				previousFlashTime = gameTime.TotalGameTime.Milliseconds;
			}
			if (Math.Abs(gameTime.TotalGameTime.Milliseconds - previousFlashTime) > flashTime)
			{
				previousFlashTime = gameTime.TotalGameTime.Milliseconds;
				flash = !flash;
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
			if (Map.IsInMap(x, y))
			{
				List<Component> collisionComponents = GetComponents(typeof(CollisionComponent));
				collisionComponents.ForEach((comp) =>
				{
					CollisionComponent c = comp as CollisionComponent;
					bool clear = c.CanMove(Map[x, y].Type);
					if (!clear)
					{
						canMove = false;
					}
					collidesWith.Add(Map[x, y]);
				});
				Map.GameObjects.ForEach((o) =>
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
			Camera.Instance.SetPosition(Pixel.X, Pixel.Y);
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
