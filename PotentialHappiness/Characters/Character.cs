using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PotentialHappiness.GameObjects;

namespace PotentialHappiness.Characters
{
	public class Character : PixelGameObject
	{
		public string Name { get; private set; }
		protected int Speed = 1;

		public Character(string name, Color characterColor) : base(characterColor)
		{
			Name = name;

			CharacterManager.Instance.Characters.Add(this);
		}
	}
}
