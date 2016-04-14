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

		public int Next() => Random.Next();
		public int Next(int max) => Random.Next(max);
		public int Next(int min, int max) => Random.Next(min, max);

		private static readonly Lazy<RandomManager> lazy = new Lazy<RandomManager>(() => new RandomManager());

		public static RandomManager Instance => lazy.Value;
	}
}
