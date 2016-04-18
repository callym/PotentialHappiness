using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using PotentialHappiness.Characters;
using PotentialHappiness.Components;
using PotentialHappiness.Interface;
using PotentialHappiness.Map;

namespace PotentialHappiness.GameObjects
{
	public class GemObject : PixelGameObject
	{
		static bool first = true;
		int HealthBonus = 100;
		public GemObject(TileMap map) : base(Color.PaleGoldenrod, map)
		{

		}

		public GemObject(int x, int y, TileMap map) : base(Color.PaleGoldenrod, x, y, map)
		{

		}

		protected override void Init()
		{
			base.Init();

			CollisionComponent collision = new CollisionComponent(this);
			collision.AddEvent(typeof(Character), false, (o, e) =>
			{
				if (first)
				{
					PopupTextBox ptb = new PopupTextBox(Map.Screen);
					ptb.Text = "oh ! this gem is small and shiny, i feel better for picking it up !";
					first = false;
				}
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
