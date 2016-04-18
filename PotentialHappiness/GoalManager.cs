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
			Anger		= 1,
			Anxiety		= 2,
			Depression	= 4
		}
		public Types? GoalsPlaced;

		public int Aim { get; } = Enum.GetNames(typeof(Types)).Length;
		public int Current = 0;

		private GoalManager()
		{

		}

		public void Reload()
		{
			Current = 0;
			GoalsPlaced = null;
		}

		public void MakeGoal(int x, int y, TileMap map)
		{
			if (AllGoalsPlaced())
			{
				return;
			}

			GoalObject goal = new GoalObject(Color.LightPink, x, y, map);
			
			if (!GoalsPlaced.HasValue || !GoalsPlaced.Value.HasFlag(Types.Anger))
			{
				goal.Message = "anger";
				if (!GoalsPlaced.HasValue)
				{
					GoalsPlaced = Types.Anger;
				}
				else
				{
					GoalsPlaced |= Types.Anger;
				}
			}
			else if (!GoalsPlaced.Value.HasFlag(Types.Anxiety))
			{
				goal.Message = "anxiety";
				GoalsPlaced |= Types.Anxiety;
			}
			else if (!GoalsPlaced.Value.HasFlag(Types.Depression))
			{
				goal.Message = "depression";
				GoalsPlaced |= Types.Depression;
			}
		}

		public bool AllGoalsPlaced()
		{
			if (!GoalsPlaced.HasValue)
			{
				return false;
			}
			return GoalsPlaced.Value.HasFlag(Types.Anger) &&
					GoalsPlaced.Value.HasFlag(Types.Anxiety) &&
					GoalsPlaced.Value.HasFlag(Types.Depression);
		}

		private static readonly Lazy<GoalManager> lazy = new Lazy<GoalManager>(() => new GoalManager());
		public static GoalManager Instance => lazy.Value;
	}
}
