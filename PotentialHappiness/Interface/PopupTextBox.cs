using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PotentialHappiness.Extensions;
using PotentialHappiness.Screens;

namespace PotentialHappiness.Interface
{
	public class PopupTextBox : TextBox
	{
		public PopupTextBox(GameScreen screen) : base(screen)
		{
			int numLines = 2;
			int height = (ScreenManager.Instance.Font.LineSpacing * numLines);
			Area = new Rectangle(0, 63 - height, 63, height);
			Color = Color.White;
			Alignment = GraphicsExtensions.Alignment.Top;
		}

		public PopupTextBox(string text, GameScreen screen) : this(screen)
		{
			Text = text;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			ScreenManager.Instance.SpriteBatch.DrawRectangle(Area, Color.LimeGreen, true, Color.White.ToAlpha(0.5f));
			ScreenManager.Instance.SpriteBatch.DrawString(ScreenManager.Instance.Font,
															Text,
															Area,
															Alignment,
															Color);
		}
	}
}
