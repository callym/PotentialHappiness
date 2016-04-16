using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static PotentialHappiness.Extensions.GraphicsExtensions;

namespace PotentialHappiness.Screens
{
	public sealed class ScreenManager : Game
	{
		public GraphicsDeviceManager GraphicsDeviceManager;
		public SpriteBatch SpriteBatch;
		public Texture2D PixelTexture;
		public SpriteFont Font;
		public GameList<GameScreen> ScreenList;
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
			ScreenList = new GameList<GameScreen>();
			ScreenList.OnAddEach += (o, e) =>
			{
				if (o is GameScreen)
				{
					((GameScreen)o).LoadAssets();
				}
			};

			ScreenList.OnRemoveEach += (o, e) =>
			{
				if (o is GameScreen)
				{
					((GameScreen)o).UnloadAssets();
				}
			};

			ScreenList.OnRemove += (o, e) =>
			{
				if ((ScreenList.Count - ScreenList.ToRemove.Count + ScreenList.ToAdd.Count) < 1)
				{
					ScreenList.Add(new TestScreen());
				}
			};

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
			Font = Content.Load<SpriteFont>("handy-font");
			Font.Spacing -= 2;
			Font.LineSpacing += 2;

			PixelTexture = new Texture2D(GraphicsDevice, 1, 1);
			PixelTexture.SetData(new Color[1] { Color.White });

			ScreenList.Add(new TitleScreen());

			base.LoadContent();
		}

		protected override void UnloadContent()
		{
			ScreenList.ForEach(s =>
			{
				s.UnloadAssets();
			});
			ScreenList.Clear();
			Content.Unload();

			base.UnloadContent();
		}

		protected override void Update(GameTime gameTime)
		{
			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
			{
				Exit();
			}

			if (ScreenList.Count > 0)
			{
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
			}

			GameListManager.Instance.Update(gameTime);

			VirtualScreen.Update();

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			VirtualScreen.BeginCapture();

			if (ScreenList.Count > 0)
			{
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
			}

			VirtualScreen.EndCapture();

			GraphicsDevice.Clear(Color.White);
			SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
			VirtualScreen.Draw(SpriteBatch);

#if DEBUG_GRID
			Color lineColor = Color.Gray;
			for (int i = 0; i <= ScreenSize; i = i + (ScreenSize / VirtualScreen.VirtualWidth))
			{
				SpriteBatch.DrawLine(new Vector2(0, i), new Vector2(ScreenSize, i), lineColor);
				SpriteBatch.DrawLine(new Vector2(i, 0), new Vector2(i, ScreenSize), lineColor);
			}
#endif
			SpriteBatch.End();

			base.Draw(gameTime);
		}

		public void ChangeScreens(GameScreen currentScreen, GameScreen targetScreen)
		{
			ScreenList.Add(targetScreen);
			ScreenList.Remove(currentScreen);
		}

		void Window_ClientSizeChanged(object sender, EventArgs e) => VirtualScreen.PhysicalResolutionChanged();

		private static readonly Lazy<ScreenManager> lazy = new Lazy<ScreenManager>(() => new ScreenManager());
		public static ScreenManager Instance => lazy.Value;
	}
}
