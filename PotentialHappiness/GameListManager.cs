using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PotentialHappiness
{
	public sealed class GameListManager
	{
		public EventHandler updateList;
		private GameListManager()
		{

		}

		public void Update(GameTime gameTime)
		{
			updateList?.Invoke(gameTime, EventArgs.Empty);
		}

		private static readonly Lazy<GameListManager> lazy = new Lazy<GameListManager>(() => new GameListManager());

		public static GameListManager Instance => lazy.Value;
	}
}
