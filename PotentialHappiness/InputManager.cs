using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PotentialHappiness.Components;

namespace PotentialHappiness
{
	public enum Input
	{
		Held,
		Pressed,
		Released
	}

	public sealed class InputManager
	{
		public HashSet<InputComponent> InputComponents;
		HashSet<InputComponent> componentsToAdd;
		HashSet<InputComponent> componentsToRemove;
		KeyboardState previousState;

		private InputManager()
		{
			InputComponents = new HashSet<InputComponent>();
			componentsToAdd = new HashSet<InputComponent>();
			componentsToRemove = new HashSet<InputComponent>();
			previousState = Keyboard.GetState();
		}

		public void AddComponent(InputComponent inputComponent)
		{
			componentsToAdd.Add(inputComponent);
		}

		public void RemoveComponent(InputComponent inputComponent)
		{
			componentsToRemove.Add(inputComponent);
		}

		public void Update(GameTime gameTime)
		{
			KeyboardState currentState = Keyboard.GetState();
			Keys[] currentPressed = currentState.GetPressedKeys();
			Keys[] previousPressed = previousState.GetPressedKeys();

			foreach (Keys k in currentPressed.Intersect(previousPressed))
			{
				// still pressed
				foreach (InputComponent i in InputComponents)
				{
					if (i.KeysHeld.ContainsKey(k))
					{
						i.KeysHeld[k].Invoke(this, EventArgs.Empty);
					}
				}
			}

			foreach (Keys k in currentPressed.Except(previousPressed))
			{
				// new pressed
				foreach (InputComponent i in InputComponents)
				{
					if (i.KeysPressed.ContainsKey(k))
					{
						i.KeysPressed[k].Invoke(this, EventArgs.Empty);
					}
				}
			}

			foreach (Keys k in previousPressed.Except(currentPressed))
			{
				// new released
				foreach (InputComponent i in InputComponents)
				{
					if (i.KeysReleased.ContainsKey(k))
					{
						i.KeysReleased[k].Invoke(this, EventArgs.Empty);
					}
				}
			}

			previousState = currentState;

			foreach (InputComponent i in componentsToAdd)
			{
				InputComponents.Add(i);
			}
			componentsToAdd.Clear();

			foreach (InputComponent i in componentsToRemove)
			{
				InputComponents.Remove(i);
			}
			componentsToRemove.Clear();
		}

		private static readonly Lazy<InputManager> lazy = new Lazy<InputManager>(() => new InputManager());

		public static InputManager Instance => lazy.Value;
	}
}
