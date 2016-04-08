using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PotentialHappiness.Screens
{
	public class GameScreen
	{
		public bool IsActive = true;
		public bool IsPopup = false;
		public Color BackgroundColor = Color.CornflowerBlue;

		public virtual void LoadAssets()
		{

		}

		public virtual void UnloadAssets()
		{

		}

		public virtual void Update(GameTime gameTime)
		{

		}

		public virtual void Draw(GameTime gameTime)
		{

		}
	}
}
