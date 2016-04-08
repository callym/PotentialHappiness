using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PotentialHappiness.GameObjects;
using PotentialHappiness.Screens;
using PotentialHappiness.Extensions;

namespace PotentialHappiness.Interface
{
	public class TextBox : GameObject
	{
		public string Font { get; set; } = "handy-font";
		public string Text { get; set; }
		public Rectangle Area { get; set; }
		public GraphicsExtensions.Alignment Alignment { get; set; } = GraphicsExtensions.Alignment.Center;

		public override void Draw(SpriteBatch spriteBatch)
		{
			string title = "Potential\nHappiness";
			ScreenManager.Instance.SpriteBatch.DrawString(ScreenManager.Instance.Fonts[Font],
															Text,
															Area,
															Alignment,
															Color.White);
			base.Draw(spriteBatch);
		}
	}
}
