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
	public class GameObject
	{
		public List<Component> Components;

		public int X = 0;
		public int Y = 0;
		
		public GameObject()
		{
			Components = new List<Component>();
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

		}
	}
}
