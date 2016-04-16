using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PotentialHappiness.GameObjects;
using PotentialHappiness.Map.Cells;

namespace PotentialHappiness.Components
{
	public class CollisionComponent : Component
	{
		public Dictionary<CellType, EventHandler> CellCollide;
		public Dictionary<Type, EventHandler> ObjectCollide;

		public List<CellType> CellBlockMovement;
		public List<Type> ObjectBlockMovement;

		public CollisionComponent(GameObject parent) : base(parent)
		{
			CellCollide = new Dictionary<CellType, EventHandler>();
			ObjectCollide = new Dictionary<Type, EventHandler>();

			CellBlockMovement = new List<CellType>();
			ObjectBlockMovement = new List<Type>();
		}

		public void OnCollide(MapObject collideWith)
		{
			if (collideWith is MapCell)
			{
				MapCell cell = collideWith as MapCell;
				foreach (KeyValuePair<CellType, EventHandler> e in CellCollide)
				{
					if (e.Key == cell.Type)
					{
						e.Value?.Invoke(cell, EventArgs.Empty);
					}
				}
			}
			else
			{
				foreach (KeyValuePair<Type, EventHandler> e in ObjectCollide)
				{
					if (collideWith.GetType().IsAssignableFrom(e.Key))
					{
						e.Value?.Invoke(collideWith, EventArgs.Empty);
					}
				}
			}
		}

		public void OnCollide(List<MapObject> collideWith) => collideWith.ForEach((o) => OnCollide(o));

		public bool CanMove(CellType type) => !CellBlockMovement.Contains(type);
		public bool CanMove(Type type) => !ObjectBlockMovement.Contains(type);

		public void AddEvent(CellType type, bool blockMovement, EventHandler e = null)
		{
			if (e != null)
			{
				if (!CellCollide.ContainsKey(type))
				{
					CellCollide.Add(type, e);
				}
				else
				{
					CellCollide[type] += e;
				}
			}

			if (blockMovement)
			{
				CellBlockMovement.Add(type);
			}
		}

		public void AddEvent(Type type, bool blockMovement, EventHandler e = null)
		{
			if (e != null)
			{
				if (!ObjectCollide.ContainsKey(type))
				{
					ObjectCollide.Add(type, e);
				}
				else
				{
					ObjectCollide[type] += e;
				}
			}

			if (blockMovement)
			{
				ObjectBlockMovement.Add(type);
			}
		}
	}
}
