﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PotentialHappiness.GameObjects;

namespace PotentialHappiness.Components
{
	public class InputComponent : Component
	{
		public Dictionary<Keys, EventHandler> KeysHeld;
		public Dictionary<Keys, EventHandler> KeysPressed;
		public Dictionary<Keys, EventHandler> KeysReleased;

		public InputComponent(GameObject parent) : base(parent)
		{
			KeysHeld = new Dictionary<Keys, EventHandler>();
			KeysPressed = new Dictionary<Keys, EventHandler>();
			KeysReleased = new Dictionary<Keys, EventHandler>();

			InputManager.Instance.InputComponents.Add(this);
		}

		public override void Unload()
		{
			InputManager.Instance.InputComponents.Remove(this);

			base.Unload();
		}

		public void AddEvent(Keys key, Input type, EventHandler e)
		{
			Dictionary<Keys, EventHandler> d = null;

			switch (type)
			{
				case Input.Held:
					d = KeysHeld;
					break;
				case Input.Pressed:
					d = KeysPressed;
					break;
				case Input.Released:
					d = KeysReleased;
					break;
				default:
					return;
			}

			if (d == null)
			{
				return;
			}

			if (!d.ContainsKey(key))
			{
				d.Add(key, e);
			}
			else
			{
				d[key] += e;
			}
		}

		public void AddEvent(Keys[] keys, Input type, EventHandler e) => Array.ForEach(keys, k => AddEvent(k, type, e));
	}
}
