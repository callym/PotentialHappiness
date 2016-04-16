using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace PotentialHappiness
{
	public class GameList<T> : List<T>
	{
		public List<T> ToAdd;
		public List<T> ToRemove;

		public EventHandler OnAddEach;
		public EventHandler OnRemoveEach;

		public EventHandler OnAdd;
		public EventHandler OnRemove;

		public GameList()
		{
			ToAdd = new List<T>();
			ToRemove = new List<T>();

			GameListManager.Instance.updateList += (e, o) => Update(e as GameTime);
		}

		public void Update(GameTime gameTime)
		{
			ToAdd.ForEach((o) => OnAddEach?.Invoke(o, EventArgs.Empty));
			OnAdd?.Invoke(this, EventArgs.Empty);
			ToAdd.ForEach((o) => base.Add(o));
			ToAdd.Clear();

			ToRemove.ForEach((o) => OnRemoveEach?.Invoke(o, EventArgs.Empty));
			OnRemove?.Invoke(this, EventArgs.Empty);
			ToRemove.ForEach((o) => base.Remove(o));
			ToRemove.Clear();
		}

		public new void Add(T item)
		{
			ToAdd.Add(item);
		}

		public new void Remove(T item)
		{
			ToRemove.Add(item);
		}
	}
}
