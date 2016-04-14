using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotentialHappiness.AStar
{
	public class MapSearchNode
	{
		public NodePosition position;
		AStarPathfinder pathfinder = null;
		Map map;

		public MapSearchNode(AStarPathfinder pathfinder, Map map)
		{
			position = new NodePosition(0, 0);
			this.pathfinder = pathfinder;
			this.map = map;
		}

		public MapSearchNode(NodePosition pos, AStarPathfinder pathfinder)
		{
			position = new NodePosition(pos.x, pos.y);
			this.pathfinder = pathfinder;
		}

		// Here's the heuristic function that estimates the distance
		// from a Node to the Goal.
		public float GoalDistanceEstimate(MapSearchNode nodeGoal)
		{
			double X = (double)position.x - (double)nodeGoal.position.x;
			double Y = (double)position.y - (double)nodeGoal.position.y;

			// pythagoras theorem
			return (float)Math.Sqrt((X * X) + (Y * Y));
		}

		public bool IsGoal(MapSearchNode nodeGoal)
		{
			return (position.x == nodeGoal.position.x &&
					position.y == nodeGoal.position.y);
		}

		public bool ValidNeighbour(int xOffset, int yOffset)
		{
			// Return true if the node is navigable and within grid bounds
			return (map.GetMap(position.x + xOffset, position.y + yOffset) < 9);
		}

		void AddNeighbourNode(int xOffset, int yOffset, NodePosition parentPos, AStarPathfinder aStarSearch)
		{
			if (ValidNeighbour(xOffset, yOffset) &&
					!(parentPos.x == position.x + xOffset &&
					parentPos.y == position.y + yOffset))
			{
				NodePosition neighbourPos = new NodePosition(position.x + xOffset, position.y + yOffset);
				MapSearchNode newNode = pathfinder.AllocateMapSearchNode(neighbourPos);
				aStarSearch.AddSuccessor(newNode);
			}
		}

		// This generates the successors to the given Node. It uses a helper function called
		// AddSuccessor to give the successors to the AStar class. The A* specific initialisation
		// is done for each node internally, so here you just set the state information that
		// is specific to the application
		public bool GetSuccessors(AStarPathfinder aStarSearch, MapSearchNode parentNode)
		{
			NodePosition parentPos = new NodePosition(0, 0);

			if (parentNode != null)
			{
				parentPos = parentNode.position;
			}

			// push each possible move except allowing the search to go backwards
			AddNeighbourNode(-1, 0, parentPos, aStarSearch);
			AddNeighbourNode(0, -1, parentPos, aStarSearch);
			AddNeighbourNode(1, 0, parentPos, aStarSearch);
			AddNeighbourNode(0, 1, parentPos, aStarSearch);

			return true;
		}

		public float GetCost(MapSearchNode successor) => map.GetMap(successor.position.x, successor.position.y);

		public bool IsSameState(MapSearchNode rhs) => (position.x == rhs.position.x && position.y == rhs.position.y);
	}
}
