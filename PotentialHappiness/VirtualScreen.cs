using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame;

namespace PotentialHappiness
{
	class VirtualScreen
	{
		public int VirtualWidth { get; }
		public int VirtualHeight { get; }
		public float VirtualAspectRatio { get; }

		private GraphicsDevice graphicsDevice;
		private RenderTarget2D screen;

		private Rectangle area;
		private bool isDirty = true;

		public VirtualScreen(int vWidth, int vHeight, GraphicsDevice gDevice)
		{
			VirtualWidth = vWidth;
			VirtualHeight = vHeight;
			VirtualAspectRatio = (float)VirtualWidth / (float)VirtualHeight;

			graphicsDevice = gDevice;
			screen = new RenderTarget2D(graphicsDevice,
										VirtualWidth,
										VirtualHeight,
										false,
										graphicsDevice.PresentationParameters.BackBufferFormat,
										graphicsDevice.PresentationParameters.DepthStencilFormat,
										graphicsDevice.PresentationParameters.MultiSampleCount,
										RenderTargetUsage.DiscardContents);
		}

		public void PhysicalResolutionChanged() => isDirty = true;

		public void Update()
		{
			if (!isDirty)
			{
				return;
			}

			isDirty = false;

			int physicalWidth = graphicsDevice.Viewport.Width;
			int physicalHeight = graphicsDevice.Viewport.Height;
			float physicalAspectRatio = graphicsDevice.Viewport.AspectRatio;

			const int SCALE = 10;

			if ((int)(physicalAspectRatio * SCALE) == (int)(VirtualAspectRatio * SCALE))
			{
				area = new Rectangle(0, 0, physicalWidth, physicalHeight);
				return;
			}

			if (VirtualAspectRatio > physicalAspectRatio)
			{
				float scaling = (float)physicalWidth / (float)VirtualWidth;
				float width = (float)VirtualWidth * scaling;
				float height = (float)VirtualHeight * scaling;
				int borderSize = (int)((physicalHeight - height) / 2);
				area = new Rectangle(0, borderSize, (int)width, (int)height);
			}
			else
			{
				float scaling = (float)physicalHeight / (float)VirtualHeight;
				float width = (float)VirtualWidth * scaling;
				float height = (float)VirtualHeight * scaling;
				int borderSize = (int)((physicalWidth - width) / 2);
				area = new Rectangle(borderSize, 0, (int)width, (int)height);
			}
		}

		public void BeginCapture() => graphicsDevice.SetRenderTarget(screen);
		public void EndCapture() => graphicsDevice.SetRenderTarget(null);
		public void Draw(SpriteBatch spriteBatch) => spriteBatch.Draw(screen, area, Color.White);
	}
}
