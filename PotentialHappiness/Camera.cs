using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PotentialHappiness.Map;
using Microsoft.Xna.Framework.Graphics;

namespace PotentialHappiness
{
	public sealed class Camera
	{
		public int X = 0;
		public int Y = 0;
		public int Scale = 2;
		public Matrix ScaleMatrix => Matrix.CreateScale(Scale);

		public void Update(GameTime gameTime)
		{
			int newX = X;
			int newY = Y;

			int speed = 1;

			KeyboardState ks = Keyboard.GetState();
			if (ks.IsKeyDown(Keys.Left))
			{
				newX += speed;
			}
			if (ks.IsKeyDown(Keys.Right))
			{
				newX -= speed;
			}
			if (ks.IsKeyDown(Keys.Up))
			{
				newY += speed;
			}
			if (ks.IsKeyDown(Keys.Down))
			{
				newY -= speed;
			}

			int border = PotentialHappinessGame.Instance.VirtualScreenSize / (2 * Scale);
			int mapBorderX = MapManager.Instance.CurrentMap.MapWidth - border;
			int mapBorderY = MapManager.Instance.CurrentMap.MapHeight - border;

			if (newX < border && newX > -mapBorderX)
			{
				X = newX;
			}
			if (newY < border && newY > -mapBorderY)
			{
				Y = newY;
			}
		}

		public Point ToCamera(int x, int y)
		{
			return new Point(x + X, y + Y);
		}

		private static readonly Lazy<Camera> lazy = new Lazy<Camera>(() => new Camera());
		public static Camera Instance => lazy.Value;

		private Camera()
		{

		}
	}
}
