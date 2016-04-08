using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PotentialHappiness.Extensions;

namespace PotentialHappiness.Screens
{
	public class TitleScreen : GameScreen
	{
		public TitleScreen() : base()
		{
			BackgroundColor = Color.Black;
		}

		public override void Draw(GameTime gameTime)
		{
			ScreenManager.Instance.SpriteBatch.Begin();
			string title = "Potential\nHappiness";
			ScreenManager.Instance.SpriteBatch.DrawString(ScreenManager.Instance.Fonts["handy-font"],
															title,
															ScreenManager.Instance.VirtualScreen.VirtualArea,
															GraphicsExtensions.Alignment.Center,
															Color.White);
			ScreenManager.Instance.SpriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
