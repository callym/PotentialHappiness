using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PotentialHappiness.Characters;
using PotentialHappiness.Components;
using PotentialHappiness.Map;

namespace PotentialHappiness.GameObjects
{
	public class GemObject : PixelGameObject
	{
		int HealthBonus = 100;
		public GemObject(Color color, TileMap map) : base(color, map)
		{

		}

		public GemObject(Color color, int x, int y, TileMap map) : base(color, x, y, map)
		{

		}

		protected override void Init()
		{
			base.Init();

			CollisionComponent collision = new CollisionComponent(this);
			collision.AddEvent(typeof(Character), false, (o, e) =>
			{
				List<Component> findHealth = (o as Character).GetComponents(typeof(LevelComponent));
				findHealth.ForEach((comp) =>
				{
					LevelComponent c = comp as LevelComponent;
					if (c.Name == "Health")
					{
						c.CurrentLevel += HealthBonus;
					}
				});
				this.unload = true;
			});
		}
	}
}
