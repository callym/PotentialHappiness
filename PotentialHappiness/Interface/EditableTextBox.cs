using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PotentialHappiness.Extensions;
using PotentialHappiness.Screens;

namespace PotentialHappiness.Interface
{
	public class EditableTextBox : TextBox
	{
		EventHandler<TextInputEventArgs> onTextEntered;
		KeyboardState _prevKeyState;
		int maxLength = -1;

		public EditableTextBox(GameScreen screen) : base(screen)
		{
			ScreenManager.Instance.Window.TextInput += TextEntered;
			onTextEntered += HandleInput;
			OnEnable += (e, o) => CalculateMaxLength();
		}

		public EditableTextBox(string text, GameScreen screen) : this(screen)
		{
			Text = text;
		}

		private void CalculateMaxLength()
		{
			SpriteFont font = ScreenManager.Instance.Font;
			int i = 0;
			// '@' is the widest character
			while (font.MeasureString(new string('@', i)).X < Area.Width)
			{
				i++;
			}
			maxLength = i - 1;
		}

		private void TextEntered(object sender, TextInputEventArgs e)
		{
			if (onTextEntered != null)
			{
				onTextEntered.Invoke(sender, e);
			}
		}

		private void HandleInput(object sender, TextInputEventArgs e)
		{
			if (Enabled)
			{
				char charEntered = e.Character;

				if (Text.Length < maxLength && Char.IsLetter(charEntered))
				{
					Text += charEntered.ToString().ToLower();
				}
				else if (Text.Length > 0 && charEntered == '\b')
				{
					string s = Text.Remove(Text.Length - 1);
					Text = s;
				}
			}
		}

		public override void Update(GameTime gameTime)
		{
			if (maxLength < 0)
			{
				CalculateMaxLength();
			}

			KeyboardState keyState = Keyboard.GetState();

			if (keyState.IsKeyDown(Keys.Back) && _prevKeyState.IsKeyUp(Keys.Back))
			{
				onTextEntered.Invoke(this, new TextInputEventArgs('\b'));
			}

			_prevKeyState = keyState;

			base.Update(gameTime);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			string fullText = Text;
			while (fullText.Length < maxLength)
			{
				fullText += "_";
			}
			fullText = fullText.ToUpper();
			ScreenManager.Instance.SpriteBatch.DrawString(ScreenManager.Instance.Font,
															fullText,
															Area,
															Alignment,
															Color);
		}
	}
}
