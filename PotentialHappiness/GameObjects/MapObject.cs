using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PotentialHappiness.Map;

namespace PotentialHappiness.GameObjects
{
	public class MapObject : GameObject
	{
		TileMap _map = null;
		public TileMap Map
		{
			get
			{
				return _map;
			}
			set
			{
				_map?.GameObjects.Remove(this);
				_map = value;
				_map?.GameObjects.Add(this);
			}
		}
		public MapObject(TileMap map) : base()
		{
			Map = map;
		}

		public override void Unload()
		{
			base.Unload();

			Map.GameObjects.Remove(this);
		}
	}
}
