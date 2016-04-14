using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotentialHappiness
{
	public sealed class RandomManager
	{
		public Random Random;

		private RandomManager()
		{
			Random = new Random();
		}

		private static readonly Lazy<RandomManager> lazy = new Lazy<RandomManager>(() => new RandomManager());

		public static RandomManager Instance => lazy.Value;
	}
}
