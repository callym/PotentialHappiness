﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PotentialHappiness.Components;
using PotentialHappiness.Extensions;
using PotentialHappiness.Screens;

namespace PotentialHappiness.Interface
{
	public class PopupTextBox : TextBox
	{
		public PopupTextBox Next = null;
		public override string Text
		{
			get
			{
				return base.Text;
			}

			set
			{
				string wrapped = ScreenManager.Instance.Font.WrapText(value, Area.Width);
				List<string> wrappedLines = wrapped.SplitAtNewLines().ToList();
				string forthis = "";
				string fornext = "";
				for (int i = 0; i < wrappedLines.Count; i++)
				{
					if (i < numLines)
					{
						forthis += wrappedLines[i];
					}
					else
					{
						fornext += wrappedLines[i];
					}
				}
				base.Text = forthis;
				if (fornext != "")
				{
					if (Next == null)
					{
						Next = new PopupTextBox(Screen, key);
						Next.Visible = false;
						Next.Enabled = false;
					}
					Next.Text = fornext.Replace("\r\n", "");
				}
			}
		}

		Keys key;
		int numLines = 2;

		Color BackgroundColor;
		Rectangle textArea;

		public PopupTextBox(GameScreen screen, Keys nextKey = Keys.Enter) : base(screen)
		{
			int height = (ScreenManager.Instance.Font.LineSpacing * numLines) + 2;
			Area = new Rectangle(0, 63 - height, 63, height);
			textArea = Area;
			textArea.Inflate(-2, -2);
			textArea.Y--;
			Color = Color.White;
			BackgroundColor = Color.Black.ToAlpha(0.5f);
			Alignment = GraphicsExtensions.Alignment.Top | GraphicsExtensions.Alignment.Left;
			key = nextKey;

			InputComponent ic = new InputComponent(this);
			ic.AddEvent(key, Input.Pressed, (o, e) =>
			{
				this.unload = true;
				if (this.Next != null)
				{
					this.Next.Visible = true;
					this.Next.Enabled = true;
				}
			});
		}

		public PopupTextBox(string text, GameScreen screen) : this(screen)
		{
			Text = text;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			ScreenManager.Instance.SpriteBatch.DrawRectangle(Area, Color.ToAlpha(0.5f), true, BackgroundColor);
			ScreenManager.Instance.SpriteBatch.DrawString(ScreenManager.Instance.Font,
															Text,
															textArea,
															Alignment,
															Color);
			if (Next != null)
			{
				for (int i = 0; i < 3; i++)
				{
					ScreenManager.Instance.SpriteBatch.Draw(ScreenManager.Instance.PixelTexture,
															new Rectangle((Area.X + Area.Width - 2) - (i * 2), Area.Y + Area.Height - 2, 1, 1),
															Color);
				}
			}
		}
	}
}
