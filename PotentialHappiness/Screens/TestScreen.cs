﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace PotentialHappiness.Screens
{
	public class TestScreen : GameScreen
	{
		private readonly Matrix _projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 480f, 0.01f, 100f);
		private readonly Matrix _view = Matrix.CreateLookAt(new Vector3(0, 0, 3), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
		private readonly Matrix _world = Matrix.CreateTranslation(0, 0, 0);
		private BasicEffect _basicEffect;
		private VertexBuffer _vertexBuffer;

		public override void LoadAssets()
		{
			_basicEffect = new BasicEffect(ScreenManager.Instance.GraphicsDeviceManager.GraphicsDevice);

			var vertices = new VertexPositionColor[3];
			vertices[0] = new VertexPositionColor(new Vector3(0, 1, 0), Color.Red);
			vertices[1] = new VertexPositionColor(new Vector3(+0.5f, 0, 0), Color.Green);
			vertices[2] = new VertexPositionColor(new Vector3(-0.5f, 0, 0), Color.Blue);

			_vertexBuffer = new VertexBuffer(ScreenManager.Instance.GraphicsDeviceManager.GraphicsDevice, typeof(VertexPositionColor), 3, BufferUsage.WriteOnly);
			_vertexBuffer.SetData(vertices);
		}

		public override void Draw(GameTime gameTime)
		{
			_basicEffect.World = _world;
			_basicEffect.View = _view;
			_basicEffect.Projection = _projection;
			_basicEffect.VertexColorEnabled = true;

			ScreenManager.Instance.GraphicsDeviceManager.GraphicsDevice.SetVertexBuffer(_vertexBuffer);

			var rasterizerState = new RasterizerState { CullMode = CullMode.None };
			ScreenManager.Instance.GraphicsDeviceManager.GraphicsDevice.RasterizerState = rasterizerState;

			foreach (var pass in _basicEffect.CurrentTechnique.Passes)
			{
				pass.Apply();
				ScreenManager.Instance.GraphicsDeviceManager.GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 1);
			}
		}

	}
}
