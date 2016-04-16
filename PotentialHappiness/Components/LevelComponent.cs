using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PotentialHappiness.GameObjects;

namespace PotentialHappiness.Components
{
	public class LevelComponent : Component
	{
		int _currentLevel;
		public int CurrentLevel
		{
			get
			{
				return _currentLevel;
			}
			set
			{
				if (value > MaxLevel)
				{
					_currentLevel = MaxLevel;
					OnMaxLevel?.Invoke(this, EventArgs.Empty);
				}
				else if (value < MinLevel)
				{
					_currentLevel = MinLevel;
					OnMinLevel?.Invoke(this, EventArgs.Empty);
				}
				else
				{
					_currentLevel = value;
					OnChangeLevel?.Invoke(this, EventArgs.Empty);
				}
			}
		}
		public int MinLevel = 0;
		public int MaxLevel = 100;

		public EventHandler OnMinLevel;
		public EventHandler OnMaxLevel;
		public EventHandler OnChangeLevel;

		public string Name;

		public LevelComponent(string name, GameObject parent) : base(parent)
		{
			Name = name;
		}
	}
}
