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
		List<Component> componentsToAdd;
		List<Component> componentsToRemove;

		public int X = 0;
		public int Y = 0;
		
		public GameObject()
		{
			Components = new List<Component>();
			componentsToAdd = new List<Component>();
			componentsToRemove = new List<Component>();
		}

		public void AddComponent(Component c)
		{
			componentsToAdd.Add(c);
		}

		public void RemoveComponent(Component c)
		{
			componentsToRemove.Add(c);
		}

		public virtual void Unload()
		{
			Components.ForEach(c => c.Unload());
		}

		public virtual void Update(GameTime gameTime)
		{
			Components.ForEach(c => c.Update(gameTime));
			componentsToAdd.ForEach(c => Components.Add(c));
			componentsToRemove.ForEach(c => Components.Remove(c));
			componentsToAdd.Clear();
			componentsToRemove.Clear();
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
