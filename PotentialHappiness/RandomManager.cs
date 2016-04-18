using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PotentialHappiness.Extensions;
using static PotentialHappiness.Extensions.GraphicsExtensions;

namespace PotentialHappiness
{
	public sealed class RandomManager
	{
		public Random Random;

		private RandomManager()
		{
			Random = new Random(/*12*/);
		}

		public int Next() => Random.Next();
		public int Next(int max) => Random.Next(max);
		public int Next(int min, int max) => Random.Next(min, max);
		public Color Color(ColorTypes type) => Random.RandomColor(type);

		private static readonly Lazy<RandomManager> lazy = new Lazy<RandomManager>(() => new RandomManager());

		public static RandomManager Instance => lazy.Value;
	}
}
