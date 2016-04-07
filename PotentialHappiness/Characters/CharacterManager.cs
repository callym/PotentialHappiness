using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PotentialHappiness.Characters
{
	/*
	singleton pattern from: http://csharpindepth.com/Articles/General/Singleton.aspx
	*/
	public sealed class CharacterManager
	{
		public HashSet<Character> Characters { get; private set; }

		private Character _currentCharacter = null;
		public Character CurrentCharacter
		{
			get
			{
				return _currentCharacter;
			}
			set
			{
				Characters.Add(value);
				_currentCharacter = value;
			}
		}

		public void Update(GameTime gameTime)
		{
			foreach (Character c in Characters)
			{
				c.Update(gameTime);
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Begin(transformMatrix: Camera.Instance.ScaleMatrix);
			foreach (Character c in Characters)
			{
				c.Draw(spriteBatch);
			}
			spriteBatch.End();
		}

		/*
			EVERYTHING BELOW HERE IS FOR SINGLETON
		*/

		private static readonly Lazy<CharacterManager> lazy = new Lazy<CharacterManager>(() => new CharacterManager());

		public static CharacterManager Instance => lazy.Value;

		private CharacterManager()
		{
			Characters = new HashSet<Character>();
		}
	}
}
