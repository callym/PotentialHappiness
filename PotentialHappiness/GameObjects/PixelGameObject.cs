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

		public override int X
		{
			get
			{
				return base.X;
			}

			set
			{
				base.X = value;
				Pixel.X = value * Camera.Instance.Scale;
			}
		}

		public override int Y
		{
			get
			{
				return base.Y;
			}

			set
			{
				base.Y = value;
				Pixel.Y = value * Camera.Instance.Scale;
			}
		}

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
				new Rectangle(Camera.Instance.ToCamera(Pixel.X, Pixel.Y), new Point(Camera.Instance.Scale)),
				Pixel.Color);

			base.Draw(spriteBatch);
		}
	}
}
