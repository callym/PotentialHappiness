using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PotentialHappiness.GameObjects;

namespace PotentialHappiness.Components
{
	public class Component
	{
		public GameObject Parent { get; private set; }

		public Component(GameObject parent)
		{
			Parent = parent;
			Parent.AddComponent(this);
		}

		public virtual void Unload()
		{
			Parent.RemoveComponent(this);
			Parent = null;
		}

		public virtual void Update(GameTime gameTime)
		{

		}
	}
}
