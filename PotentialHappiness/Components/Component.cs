using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PotentialHappiness.GameObjects;

namespace PotentialHappiness.Components
{
	public class Component
	{
		public GameObject Parent { get; private set; }
		public bool Enabled = true;
		public EventHandler drawEvents;

		public Component(GameObject parent)
		{
			Parent = parent;
			Parent.Components.Add(this);
		}

		public virtual void Unload()
		{
			Parent.Components.Remove(this);
			Parent = null;
		}

		public virtual void Update(GameTime gameTime)
		{

		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			drawEvents?.Invoke(spriteBatch, EventArgs.Empty);
		}
	}
}
