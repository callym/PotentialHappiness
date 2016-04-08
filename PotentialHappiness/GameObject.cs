using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PotentialHappiness.Components;

namespace PotentialHappiness
{
	public class GameObject
	{
		public Pixel Pixel;
		public List<Component> Components;

		public int X = 0;
		public int Y = 0;
		
		public GameObject()
		{
			Pixel = new Pixel(Color.Black);
			Components = new List<Component>();
		}

		public GameObject(Color color) : this()
		{
			Pixel.Color = color;
		}

		public GameObject(Color color, int x, int y) : this(color)
		{
			X = x;
			Y = y;
		}

		public virtual void Update(GameTime gameTime)
		{
			Components.ForEach(c => c.Update(gameTime));
		}

		/// <summary>
		/// Code to draw the object onto the map.
		/// </summary>
		/// <param name="spriteBatch">The current spriteBatch (after it's begun)</param>
		public virtual void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(
				Pixel.Texture,
				new Rectangle(Camera.Instance.ToCamera(X, Y), new Point(1, 1)),
				Color.White);
		}
	}
}
