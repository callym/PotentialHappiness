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
		public int Scale = 4;
		public int BorderSize = 8;
		public Matrix ScaleMatrix => Matrix.CreateScale(Scale);

		public void Update(GameTime gameTime)
		{
			
		}

		public void SetPosition(int newX, int newY)
		{
			/*
			the size of the border on each side of the camera
			top-left sides have 1/nth border,
			bottom-right have (n-1)/nth "border" - which is the amount of map visible
			*/
			int borderTL = Screens.ScreenManager.Instance.VirtualScreenSize / (BorderSize * Scale);
			int borderBR = borderTL * (BorderSize - 1);

			newX -= Screens.ScreenManager.Instance.VirtualScreenSize / (Scale / 2);
			newY -= Screens.ScreenManager.Instance.VirtualScreenSize / (Scale / 2);

			int mapBorderX = (MapManager.Instance.CurrentMap.MapWidth * Scale) - borderBR;
			int mapBorderY = (MapManager.Instance.CurrentMap.MapHeight * Scale) - borderBR;

			newX = -newX;
			newY = -newY;

			X = MathHelper.Clamp(newX, -mapBorderX, borderTL);
			Y = MathHelper.Clamp(newY, -mapBorderY, borderTL);
		}

		public void ChangePosition(int newX = 0, int newY = 0)
		{
			SetPosition(X + newX, Y + newY);
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
