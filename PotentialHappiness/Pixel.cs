using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PotentialHappiness
{
	public class Pixel
	{
		public Texture2D Texture;
		private Color _color;
		public Color Color
		{
			get
			{
				return _color;
			}

			set
			{
				_color = value;
				Texture.SetData(new Color[1] { _color });
			}
		}

		public Pixel(Color color)
		{
			Texture = new Texture2D(PotentialHappinessGame.Instance.GraphicsDevice, 1, 1);
			Color = color;
		}
	}
}
