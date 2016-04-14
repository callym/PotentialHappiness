using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PotentialHappiness.Components;

namespace PotentialHappiness.GameObjects
{
	public class PixelGameObject : GameObject
	{
		public Pixel Pixel;

		public PixelGameObject() : base()
		{
			Pixel = new Pixel(Color.Black);
		}

		public PixelGameObject(Color color) : this()
		{
			Pixel.Color = color;
		}

		public PixelGameObject(Color color, int x, int y) : this(color)
		{
			X = x;
			Y = y;
		}

		/// <summary>
		/// Code to draw the object onto the map.
		/// </summary>
		/// <param name="spriteBatch">The current spriteBatch (after it's begun)</param>
		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(
				Pixel.Texture,
				new Rectangle(Camera.Instance.ToCamera(X, Y), new Point(1, 1)),
				Pixel.Color);

			base.Draw(spriteBatch);
		}
	}
}
