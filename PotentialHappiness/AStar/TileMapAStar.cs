using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PotentialHappiness.Map;
using PotentialHappiness.Map.Cells;

namespace PotentialHappiness.AStar
{
	public sealed class TileMapAStar
	{
		AStarPathfinder Pathfinder;

		private TileMapAStar()
		{
			Pathfinder = new AStarPathfinder();
		}

		public List<MapCell> Pathfind(MapCell start, MapCell goal, TileMap map)
		{
			Pathfinder.Initialise(new Map(map));
			Pathfinder.InitiatePathfind();

			NodePosition startPosition = new NodePosition(start.X, start.Y);
			NodePosition goalPosition = new NodePosition(goal.X, goal.Y);

			MapSearchNode nodeStart = Pathfinder.AllocateMapSearchNode(startPosition);
			MapSearchNode nodeGoal = Pathfinder.AllocateMapSearchNode(goalPosition);

			Pathfinder.SetStartAndGoalStates(nodeStart, nodeGoal);

			SearchState searchState = SearchState.Searching;
			int searchSteps = 0;

			do
			{
				searchState = Pathfinder.SearchStep();
				searchSteps++;
			}
			while (searchState == SearchState.Searching);

			bool succeeded = (searchState == SearchState.Succeeded);

			List<NodePosition> newPath = new List<NodePosition>();
			if (succeeded)
			{
				int numSolutionNodes = 0; // don't count starting cell in length!

				MapSearchNode node = Pathfinder.GetSolutionStart();
				newPath.Add(node.position);
				numSolutionNodes++;

				for (;;)
				{
					node = Pathfinder.GetSolutionNext();

					if (node == null)
					{
						break;
					}

					numSolutionNodes++;
					newPath.Add(node.position);
				}

				Pathfinder.FreeSolutionNodes();
			}
			else if (searchState == SearchState.Failed)
			{
				// failed!!!
			}

			return ToMapCells(newPath, map);
		}

		public List<MapCell> ToMapCells(List<NodePosition> path, TileMap map)
		{
			List<MapCell> cells = new List<MapCell>();
			path.ForEach((np) =>
			{
				cells.Add(map[np.x, np.y]);
			});
			return cells;
		}

		private static readonly Lazy<TileMapAStar> lazy = new Lazy<TileMapAStar>(() => new TileMapAStar());

		public static TileMapAStar Instance => lazy.Value;
	}
}