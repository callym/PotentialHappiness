using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
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

		HashSet<GameObject> collidedWithThisUpdate = new HashSet<GameObject>();

		public CollisionComponent(GameObject parent) : base(parent)
		{
			CellCollide = new Dictionary<CellType, EventHandler>();
			ObjectCollide = new Dictionary<Type, EventHandler>();

			CellBlockMovement = new List<CellType>();
			ObjectBlockMovement = new List<Type>();
		}

		public override void Update(GameTime gameTime)
		{
			collidedWithThisUpdate.Clear();

			base.Update(gameTime);
		}

		public void OnCollide(GameObject collideWith)
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
				if (!collidedWithThisUpdate.Contains(collideWith))
				{
					collidedWithThisUpdate.Add(collideWith);
					foreach (KeyValuePair<Type, EventHandler> e in ObjectCollide)
					{
						if (e.Key.IsAssignableFrom(collideWith.GetType()))
						{
							e.Value?.Invoke(collideWith, EventArgs.Empty);
						}
					}

					collideWith.GetComponents(typeof(CollisionComponent)).ForEach((comp) =>
					{
						CollisionComponent c = comp as CollisionComponent;
						c.OnCollide(this.Parent);
					});
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
