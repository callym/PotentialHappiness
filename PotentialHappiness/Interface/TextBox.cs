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
		public Color Color { get; set; } = Color.White;
		public GraphicsExtensions.Alignment Alignment { get; set; } = GraphicsExtensions.Alignment.Center;

		public TextBox() : base()
		{

		}

		public TextBox(string text) : this()
		{
			Text = text;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			ScreenManager.Instance.SpriteBatch.DrawString(ScreenManager.Instance.Fonts[Font],
															Text,
															Area,
															Alignment,
															Color);
			base.Draw(spriteBatch);
		}
	}
}
