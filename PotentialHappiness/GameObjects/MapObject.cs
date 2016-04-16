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
		TileMap Map;
		public MapObject(TileMap map) : base()
		{
			Map = map;
			Map.GameObjects.Add(this);
		}

		public override void Unload()
		{
			base.Unload();

			Map.GameObjects.Remove(this);
		}
	}
}
