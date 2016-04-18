using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PotentialHappiness.GameObjects;

namespace PotentialHappiness.Screens
{
	public class GameScreen
	{
		public bool IsActive = true;
		public bool IsPopup = false;
		public int NumberOfPopups = 0;
		public bool HasPopup => NumberOfPopups > 0;
		public Color BackgroundColor = Color.Black;
		public GameList<GameObject> GameObjects;

		public GameScreen()
		{
			GameObjects = new GameList<GameObject>();
		}

		public virtual void LoadAssets()
		{

		}

		public virtual void UnloadAssets()
		{
			GameObjects.ForEach(go => go.Unload());
		}

		public virtual void Update(GameTime gameTime)
		{
			InputManager.Instance.Update(gameTime);
			GameObjects.ForEach(go =>
			{
				go.Update(gameTime);
			});
		}

		public virtual void Draw(GameTime gameTime)
		{
			SpriteBatch sb = ScreenManager.Instance.SpriteBatch;
			sb.Begin();
			GameObjects.ForEach(go =>
			{
				if (go.Visible)
				{
					go.Draw(sb);
				}
			});
			sb.End();
		}
	}
}
