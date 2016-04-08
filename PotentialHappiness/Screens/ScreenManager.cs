﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PotentialHappiness.Screens
{
	public class ScreenManager : Game
	{
		public GraphicsDeviceManager GraphicsDeviceManager;
		public SpriteBatch SpriteBatch;
		public Dictionary<string, Texture2D> Textures2D;
		public Dictionary<string, SpriteFont> Fonts;
		public List<GameScreen> ScreenList;
		public ContentManager ContentManager;
		public VirtualScreen VirtualScreen;

		public int VirtualScreenSize { get; }
		public int ScreenSize { get; }

		private ScreenManager()
		{
			VirtualScreenSize = 64;
			ScreenSize = VirtualScreenSize * 10;

			GraphicsDeviceManager = new GraphicsDeviceManager(this);
			GraphicsDeviceManager.PreferredBackBufferHeight = ScreenSize;
			GraphicsDeviceManager.PreferredBackBufferWidth = ScreenSize;

			Content.RootDirectory = "Content";
		}

		protected override void Initialize()
		{
			Textures2D = new Dictionary<string, Texture2D>();
			Fonts = new Dictionary<string, SpriteFont>();
			ScreenList = new List<GameScreen>();

			VirtualScreen = new VirtualScreen(VirtualScreenSize, VirtualScreenSize, GraphicsDevice);
			Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);
			Window.AllowUserResizing = true;

			GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;

			IsMouseVisible = true;
			IsFixedTimeStep = true;
			TargetElapsedTime = TimeSpan.FromSeconds(1 / 30.0f);

			base.Initialize();
		}

		protected override void LoadContent()
		{
			ContentManager = Content;
			SpriteBatch = new SpriteBatch(GraphicsDevice);

			// Load any full-game assets here!
			SpriteFont font = Content.Load<SpriteFont>("handy-font");
			font.Spacing -= 2;
			font.LineSpacing += 2;
			AddFont("handy-font", font);

			AddScreen(new TitleScreen());

			base.LoadContent();
		}

		protected override void UnloadContent()
		{
			ScreenList.ForEach(s => s.UnloadAssets());
			Textures2D.Clear();
			Fonts.Clear();
			ScreenList.Clear();
			Content.Unload();

			base.UnloadContent();
		}

		protected override void Update(GameTime gameTime)
		{
			try
			{
				if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				{
					Exit();
				}

				int startIndex = ScreenList.Count - 1;
				while (ScreenList[startIndex].IsPopup
						&& ScreenList[startIndex].IsActive)
				{
					startIndex--;
				}

				for (int i = startIndex; i < ScreenList.Count; i++)
				{
					ScreenList[i].Update(gameTime);
				}

				VirtualScreen.Update();
			}
			catch (Exception e)
			{
				throw e;
			}
			finally
			{
				base.Update(gameTime);
			}
		}

		protected override void Draw(GameTime gameTime)
		{
			VirtualScreen.BeginCapture();

			int startIndex = ScreenList.Count - 1;
			while (ScreenList[startIndex].IsPopup)
			{
				startIndex--;
			}

			GraphicsDevice.Clear(ScreenList[startIndex].BackgroundColor);
			GraphicsDeviceManager.GraphicsDevice.Clear(ScreenList[startIndex].BackgroundColor);

			for (int i = startIndex; i < ScreenList.Count; i++)
			{
				ScreenList[i].Draw(gameTime);
			}

			VirtualScreen.EndCapture();

			GraphicsDevice.Clear(Color.White);
			SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
			VirtualScreen.Draw(SpriteBatch);
			SpriteBatch.End();

			base.Draw(gameTime);
		}

		public void AddFont(string fontName, SpriteFont font = null)
		{
			if (!Fonts.ContainsKey(fontName))
			{
				Fonts.Add(fontName, font ?? ContentManager.Load<SpriteFont>(fontName));
			}
		}

		public void RemoveFont(string fontName)
		{
			if (Fonts.ContainsKey(fontName))
			{
				Fonts.Remove(fontName);
			}
		}

		public void AddTexture2D(string textureName, Texture2D texture = null)
		{
			if (!Textures2D.ContainsKey(textureName))
			{
				Textures2D.Add(textureName, texture ?? ContentManager.Load<Texture2D>(textureName));
			}
		}

		public void RemoveTexture2D(string textureName)
		{
			if (Fonts.ContainsKey(textureName))
			{
				Fonts.Remove(textureName);
			}
		}

		public void AddScreen(GameScreen gameScreen)
		{
			ScreenList.Add(gameScreen);
			gameScreen.LoadAssets();
		}

		public void RemoveScreen(GameScreen gameScreen)
		{
			gameScreen.UnloadAssets();
			ScreenList.Remove(gameScreen);

			if (ScreenList.Count < 1)
			{
				AddScreen(new TestScreen());
			}
		}

		void Window_ClientSizeChanged(object sender, EventArgs e) => VirtualScreen.PhysicalResolutionChanged();

		public void Main()
		{
			using (ScreenManager manager = ScreenManager.Instance)
			{
				manager.Run();
			}
		}

		private static readonly Lazy<ScreenManager> lazy = new Lazy<ScreenManager>(() => new ScreenManager());
		public static ScreenManager Instance => lazy.Value;
	}
}