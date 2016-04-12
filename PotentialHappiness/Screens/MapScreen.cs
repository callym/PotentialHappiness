using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PotentialHappiness.Map;
using PotentialHappiness.Characters;
using Microsoft.Xna.Framework;

namespace PotentialHappiness.Screens
{
	public class MapScreen : GameScreen
	{
		public MapScreen() : base()
		{
			MapManager.Instance.CurrentMap = new TileMap();
		}

		public override void Update(GameTime gameTime)
		{
			Camera.Instance.Update(gameTime);
			MapManager.Instance.Update(gameTime);
			CharacterManager.Instance.Update(gameTime);

			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime)
		{
			MapManager.Instance.Draw(ScreenManager.Instance.SpriteBatch);
			CharacterManager.Instance.Draw(ScreenManager.Instance.SpriteBatch);

			ScreenManager.Instance.SpriteBatch.Begin();
			ScreenManager.Instance.SpriteBatch.DrawString(
				ScreenManager.Instance.Fonts["handy-font"],
				CharacterManager.Instance.CurrentCharacter.Name,
				Vector2.Zero,
				Color.Magenta);
			ScreenManager.Instance.SpriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
