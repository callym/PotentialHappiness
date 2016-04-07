using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PotentialHappiness.Map
{
	/*
	singleton pattern from: http://csharpindepth.com/Articles/General/Singleton.aspx
	*/
	public sealed class MapManager
	{
		public HashSet<TileMap> Maps { get; private set; }
		private TileMap _currentMap = null;
		public TileMap CurrentMap
		{
			get
			{
				return _currentMap;
			}
			set
			{
				Maps.Add(value);
				_currentMap = value;
			}
		}

		public void Update(GameTime gameTime) => CurrentMap?.Update(gameTime);

		public void Draw(SpriteBatch spriteBatch) => CurrentMap?.Draw(spriteBatch);

		/*
			EVERYTHING BELOW HERE IS FOR SINGLETON
		*/

		private static readonly Lazy<MapManager> lazy = new Lazy<MapManager>(() => new MapManager());

		public static MapManager Instance => lazy.Value;

		private MapManager()
		{
			Maps = new HashSet<TileMap>();
		}
	}
}
