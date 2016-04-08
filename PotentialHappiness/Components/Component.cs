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
		public GameObject Parent { get; }

		public Component(GameObject parent)
		{
			Parent = parent;
			Parent.Components.Add(this);
		}

		public virtual void Update(GameTime gameTime)
		{

		}
	}
}
