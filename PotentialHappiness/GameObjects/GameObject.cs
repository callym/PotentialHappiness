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
		public GameList<Component> Components;

		bool _enabled = true;
		public EventHandler OnEnable;
		public EventHandler OnDisable;
		public bool Enabled
		{
			get
			{
				return _enabled;
			}
			set
			{
				_enabled = value;
				if (_enabled)
				{
					OnEnable.Invoke(this, EventArgs.Empty);
				}
				else
				{
					OnDisable.Invoke(this, EventArgs.Empty);
				}
			}
		}

		public bool Visible = true;

		public virtual int X { get; set; } = 0;
		public virtual int Y { get; set; } = 0;

		protected bool unload = false;
		
		public GameObject()
		{
			Components = new GameList<Component>();

			Init();
		}

		protected virtual void Init()
		{

		}

		public virtual List<Component> GetComponents(Type t) => Components.FindAll((c) => t.IsAssignableFrom(c.GetType()));

		public virtual void Unload()
		{
			Components.ForEach(c => c.Unload());
		}

		public virtual void Update(GameTime gameTime)
		{
			Components.ForEach(c =>
			{
				if (c.Enabled)
				{
					c.Update(gameTime);
				}
			});

			if (unload)
			{
				Unload();
			}
		}

		/// <summary>
		/// Code to draw the object onto the map.
		/// </summary>
		/// <param name="spriteBatch">The current spriteBatch (after it's begun)</param>
		public virtual void Draw(SpriteBatch spriteBatch)
		{
			Components.ForEach((c) => c.Draw(spriteBatch));
		}
	}
}
