using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PotentialHappiness.Components;
using PotentialHappiness.GameObjects;
using PotentialHappiness.Interface;

namespace PotentialHappiness.Screens
{
	public class EndGameScreen : GameScreen
	{
		bool Win = false;
		TextBox titleText;
		public EndGameScreen(bool win) : base()
		{
			Win = win;
			BackgroundColor = Color.Black;

			titleText = new TextBox(this);
			titleText.Area = ScreenManager.Instance.VirtualScreen.VirtualArea;
			GetMessage();

			GameObject input = new GameObject();
			InputComponent ic = new InputComponent(input);
			ic.AddEvent(Keys.Enter, Input.Pressed, (o, e) =>
			{
				if (!GetMessage())
				{
					ScreenManager.Instance.ChangeScreens(this, new TitleScreen());
				}
			});
			GameObjects.Add(input);
		}

		List<string> WinMessages = new List<string>()
		{
			"against all odds, you have collected the gems",
			"now you can start on the greater quest",
			"seeking happiness"
		};
		List<string> LoseMessages = new List<string>()
		{
			"the challenge was too great, you have faded away",
			"sunk into the pit of despair you find so comforting",
			"you have failed...",
			"yourself...",
			"everyone you know...",
			"...",
			"...\n...",
			"...\n...\n...",
			"is being a failure motivation enough to try again?"
		};

		bool GetMessage()
		{
			List<string> Messages = Win ? WinMessages : LoseMessages;
			if (Messages.Count > 0)
			{
				string s = Messages.First();
				Messages.RemoveAt(0);
				titleText.Text = s;
				return true;
			}
			return false;
		}
	}
}
