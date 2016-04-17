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
	public class TextBox : InterfaceObject
	{
		public virtual string Text { get; set; }
		public Rectangle Area { get; set; }
		public Color Color { get; set; } = Color.White;
		public GraphicsExtensions.Alignment Alignment { get; set; } = GraphicsExtensions.Alignment.Center;

		public TextBox(GameScreen screen) : base(screen)
		{

		}

		public TextBox(string text, GameScreen screen) : this(screen)
		{
			Text = text;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			ScreenManager.Instance.SpriteBatch.DrawString(Text, Area, Alignment, Color);
			base.Draw(spriteBatch);
		}
	}
}
