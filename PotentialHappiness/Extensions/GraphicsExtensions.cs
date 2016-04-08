using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PotentialHappiness.Extensions
{
	public static class GraphicsExtensions
	{
		[Flags]
		public enum Alignment { Center = 0, Left = 1, Right = 2, Top = 4, Bottom = 8 }

		public static string[] NewLines = new string[]
		{
			"\r\n",
			"\n"
		};

		public static void DrawString(this SpriteBatch spriteBatch, SpriteFont font, string text, Rectangle bounds, Alignment align, Color color)
		{
			if (NewLines.Any(s => text.Contains(s)))
			{
				Rectangle newBounds = bounds;
				string[] lines = text.Split(NewLines, StringSplitOptions.None);
				for (int i = 0; i < lines.Length; i++)
				{
					bounds.Y = (font.LineSpacing * i) - ((font.LineSpacing / 2) * (lines.Length - 1));
					spriteBatch.DrawString(font, lines[i], bounds, align, color);
				}
			}
			else
			{
				Vector2 size = font.MeasureString(text);
				Vector2 pos = bounds.Center.ToVector2();
				Vector2 origin = size * 0.5f;

				// convert to int for pixel-perfect
				origin.X = (int)origin.X;
				origin.Y = (int)origin.Y;

				if (align.HasFlag(Alignment.Left))
					origin.X += bounds.Width / 2 - size.X / 2;

				if (align.HasFlag(Alignment.Right))
					origin.X -= bounds.Width / 2 - size.X / 2;

				if (align.HasFlag(Alignment.Top))
					origin.Y += bounds.Height / 2 - size.Y / 2;

				if (align.HasFlag(Alignment.Bottom))
					origin.Y -= bounds.Height / 2 - size.Y / 2;

				spriteBatch.DrawString(font, text, pos, color, 0, origin, 1, SpriteEffects.None, 0);
			}
		}
	}
}
