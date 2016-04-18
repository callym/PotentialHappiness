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
		bool? _changeEnabled = null;
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
				if (!_changeEnabled.HasValue)
				{
					_changeEnabled = value;
}
				else
				{
					_enabled = value;
					Components.ForEach((c) => c.Enabled = value);
					if (_enabled)
					{
						OnEnable?.Invoke(this, EventArgs.Empty);
					}
					else
					{
						OnDisable?.Invoke(this, EventArgs.Empty);
					}
				}
			}
		}

		public virtual bool Visible { get; set; } = true;

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
			this.Enabled = false;
			this.Visible = false;
			Components.ForEach(c => c.Unload());
		}

		public virtual void Update(GameTime gameTime)
		{
			if (_changeEnabled.HasValue)
			{
				if (Enabled != _changeEnabled.Value)
				{
					Enabled = _changeEnabled.Value;
					_changeEnabled = null;
				}
			}

			if (Enabled)
			{
				Components.ForEach(c =>
				{
					if (c.Enabled)
					{
						c.Update(gameTime);
					}
				});
			}

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
