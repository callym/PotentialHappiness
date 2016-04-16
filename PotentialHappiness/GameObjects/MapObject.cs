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
		public MapObject(TileMap map) : base()
		{
			map.GameObjects.Add(this);
		}
	}
}
