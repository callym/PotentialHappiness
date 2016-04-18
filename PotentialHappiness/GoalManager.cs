using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PotentialHappiness.GameObjects;
using PotentialHappiness.Map;

namespace PotentialHappiness
{
	public sealed class GoalManager
	{
		[Flags]
		public enum Types
		{
			Rage		= 1,
			Despair		= 2,
			Anxiety		= 4,
			Joy			= 8
		}
		public int GoalsPlaced;

		public int Aim { get; } = Enum.GetNames(typeof(Types)).Length;
		public int Current = 0;

		private GoalManager()
		{

		}

		public void Reload()
		{
			Current = 0;
			GoalsPlaced = 0;
		}

		public void MakeGoal(int x, int y, TileMap map)
		{
			if (AllGoalsPlaced())
			{
				return;
			}

			GoalObject goal = new GoalObject(Color.MediumVioletRed, x, y, map);
			GoalsPlaced++;
		}

		public Tuple<string, string> GetGoalMessage()
		{
			switch (Current)
			{
				case 1: // rage
					return new Tuple<string, string>("you approach the gem quietly, waves of anger seem to be radiating from it.",
													"rage");
				case 2: // despair
					return new Tuple<string, string>("the world starts to swim grey before your eyes, but you know that you have to pick the gem up",
													"despair");
				case 3: // anxiety
					return new Tuple<string, string>("the gem is vibrating with an anxious energy, you could feel it from across the room",
													"anxiety");
				case 4: // joy
					return new Tuple<string, string>("this gem seems unlike the others, its surface looks soft and worn, as if it had been held a lot",
													"joy");
				default:
					return new Tuple<string, string>("", "");
			}
		}

		public bool AllGoalsPlaced()
		{
			return (GoalsPlaced >= Aim);
		}

		private static readonly Lazy<GoalManager> lazy = new Lazy<GoalManager>(() => new GoalManager());
		public static GoalManager Instance => lazy.Value;
	}
}
