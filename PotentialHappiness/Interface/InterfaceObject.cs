using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PotentialHappiness.GameObjects;
using PotentialHappiness.Screens;

namespace PotentialHappiness.Interface
{
	public class InterfaceObject : GameObject
	{
		protected GameScreen Screen;

		public InterfaceObject(GameScreen screen)
		{
			Screen = screen;
			Screen.GameObjects.Add(this);
		}

		public override void Unload()
		{
			base.Unload();

			Screen.GameObjects.Remove(this);
		}
	}
}
