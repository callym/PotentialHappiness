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
		public GameList<InputComponent> InputComponents;
		KeyboardState previousState;

		public int RepeatTime = 100;
		int previousTime = 0;

		private InputManager()
		{
			InputComponents = new GameList<InputComponent>();
			previousState = Keyboard.GetState();
		}

		public void Update(GameTime gameTime)
		{
			KeyboardState currentState = Keyboard.GetState();
			Keys[] currentPressed = currentState.GetPressedKeys();
			Keys[] previousPressed = previousState.GetPressedKeys();

			if (previousTime == 0)
			{
				previousTime = gameTime.TotalGameTime.Milliseconds;
			}

			if (Math.Abs(gameTime.TotalGameTime.Milliseconds - previousTime) > RepeatTime)
			{
				foreach (Keys k in currentPressed.Intersect(previousPressed))
				{
					// still pressed
					foreach (InputComponent i in InputComponents)
					{
						if (i.Enabled && i.Parent.Enabled && i.KeysHeld.ContainsKey(k))
						{
							i.KeysHeld[k]?.Invoke(this, EventArgs.Empty);
						}
					}
				}
				previousTime = gameTime.TotalGameTime.Milliseconds;
			}

			foreach (Keys k in currentPressed.Except(previousPressed))
			{
				// new pressed
				foreach (InputComponent i in InputComponents)
				{
					if (i.Enabled && i.Parent.Enabled && i.KeysPressed.ContainsKey(k))
					{
						i.KeysPressed[k]?.Invoke(this, EventArgs.Empty);
					}
				}
			}

			foreach (Keys k in previousPressed.Except(currentPressed))
			{
				// new released
				foreach (InputComponent i in InputComponents)
				{
					if (i.Enabled && i.Parent.Enabled && i.KeysReleased.ContainsKey(k))
					{
						i.KeysReleased[k]?.Invoke(this, EventArgs.Empty);
					}
				}
			}

			previousState = currentState;
		}

		private static readonly Lazy<InputManager> lazy = new Lazy<InputManager>(() => new InputManager());

		public static InputManager Instance => lazy.Value;
	}
}
