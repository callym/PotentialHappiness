using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using PotentialHappiness.Map;

namespace PotentialHappiness
{
	/// <summary>
	/// This is the main type for your game.
	/// </summary>
	public class PotentialHappinessGame : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		VirtualScreen virtualScreen;

		public int VirtualScreenSize { get; }
		public int ScreenSize { get; }

		private PotentialHappinessGame()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";

			VirtualScreenSize = 64;
			ScreenSize = VirtualScreenSize * 10;

			graphics.PreferredBackBufferHeight = ScreenSize;
			graphics.PreferredBackBufferWidth = ScreenSize;
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			virtualScreen = new VirtualScreen(VirtualScreenSize, VirtualScreenSize, GraphicsDevice);
			Window.ClientSizeChanged += new EventHandler<EventArgs>(Window_ClientSizeChanged);
			Window.AllowUserResizing = true;

			IsMouseVisible = true;
			IsFixedTimeStep = true;
			TargetElapsedTime = TimeSpan.FromSeconds(1 / 30.0f);

			MapManager.Instance.CurrentMap = new TileMap();

			base.Initialize();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			// TODO: use this.Content to load your game content here
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// game-specific content.
		/// </summary>
		protected override void UnloadContent()
		{
			// TODO: Unload any non ContentManager content here
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
			{
				Exit();
			}

			Camera.Instance.Update(gameTime);

			virtualScreen.Update();

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			virtualScreen.BeginCapture();
			GraphicsDevice.Clear(Color.Black);
			// TODO: Add your drawing code here
			MapManager.Instance.Draw(spriteBatch);

			virtualScreen.EndCapture();

			GraphicsDevice.Clear(Color.White);
			spriteBatch.Begin(samplerState: SamplerState.PointClamp);
			virtualScreen.Draw(spriteBatch);
			spriteBatch.End();

			base.Draw(gameTime);
		}

		void Window_ClientSizeChanged(object sender, EventArgs e) => virtualScreen.PhysicalResolutionChanged();

		private static readonly Lazy<PotentialHappinessGame> lazy = new Lazy<PotentialHappinessGame>(() => new PotentialHappinessGame());
		public static PotentialHappinessGame Instance => lazy.Value;
	}
}
