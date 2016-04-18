using System;
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
		PopupTextBox _next = null;
		public PopupTextBox Next
		{
			get
			{
				return _next;
			}
			set
			{
				_next = value;
				_next.OnClose = OnClose;
				_next.Color = Color;
			}
		}
		public override string Text
		{
			get
			{
				return base.Text;
			}

			set
			{
				string wrapped = ScreenManager.Instance.Font.WrapText(value, textArea.Width);
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
					}
					Next.Visible = false;
					Next.Enabled = false;
					Next.Text = fornext.Replace("\r\n", "");
				}
			}
		}

		Keys key;
		int numLines = 2;

		Color BackgroundColor;
		Rectangle textArea;

		EventHandler _onClose;
		public EventHandler OnClose
		{
			get
			{
				return _onClose;
			}
			set
			{
				_onClose = value;
				if (Next != null)
				{
					Next.OnClose = value;
				}
			}
		}

		public override bool Visible
		{
			get
			{
				return base.Visible;
			}

			set
			{
				base.Visible = value;
				if (base.Visible)
				{
					Screen.NumberOfPopups++;
				}
				else
				{
					Screen.NumberOfPopups--;
				}
			}
		}

		public PopupTextBox(GameScreen screen, Keys nextKey = Keys.Enter) : base(screen)
		{
			Visible = true;
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
				if (this.Next != null)
				{
					this.Next.Visible = true;
					this.Next.Enabled = true;
				}
				else
				{
					OnClose?.Invoke(this, EventArgs.Empty);
				}
				this.unload = true;
			});
		}

		public PopupTextBox(string text, GameScreen screen) : this(screen)
		{
			Text = text;
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.DrawRectangle(Area, BackgroundColor, Color);
			spriteBatch.DrawString(Text, textArea, Alignment, Color);
			if (Next != null)
			{
				for (int i = 0; i < 3; i++)
				{
					spriteBatch.DrawRectangle(new Rectangle((Area.X + Area.Width - 2) - (i * 2), Area.Y + Area.Height - 2, 1, 1), Color);
				}
			}
		}
	}
}
