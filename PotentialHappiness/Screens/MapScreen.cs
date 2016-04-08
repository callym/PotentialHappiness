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
			CharacterManager.Instance.CurrentCharacter = new PlayableCharacter("Callym", Color.LightSkyBlue);
			CharacterManager.Instance.CurrentCharacter.X = 5;
			CharacterManager.Instance.CurrentCharacter.Y = 5;
		}

		public override void Update(GameTime gameTime)
		{
			InputManager.Instance.Update(gameTime);
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
			ScreenManager.Instance.SpriteBatch.DrawString(ScreenManager.Instance.Fonts["handy-font"], "abcdefABCDEF", Vector2.Zero, Color.White);
			ScreenManager.Instance.SpriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
