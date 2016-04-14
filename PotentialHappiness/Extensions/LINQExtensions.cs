using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotentialHappiness.Extensions
{
	public static class LINQExtensions
	{
		public static void ForEachIndex<T>(this IEnumerable<T> ie, Action<T, int> action)
		{
			for (int i = 0; i < ie.Count(); i++)
			{
				action(ie.ElementAt(i), i);
			}
		}
	}
}
