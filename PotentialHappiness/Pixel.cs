using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PotentialHappiness
{
	public class Pixel
	{
		public Texture2D Texture => Screens.ScreenManager.Instance.PixelTexture;
		public Color Color { get; set; }

		public int X;
		public int Y;

		public Pixel(Color color)
		{
			Color = color;
		}
	}
}
