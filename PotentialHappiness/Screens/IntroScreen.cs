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
	public class IntroScreen : GameScreen
	{
		List<string> Messages = new List<string>()
		{
			"LEGEND has it, hundreds of years ago, a great wizard",
			$"forged {GoalManager.Instance.Aim} powerful gems",
			"full with the pure emotional energy of :",
			"over the years, they have become lost in the",
			"caves of sorrow",
			"where only those most desperate for the gems dare go.",
			"bringing together the  gems of emotional power",
			"will grant you the power to conquer your demons",
			"& perhaps restore the fragments of your broken soul !",
			"how rude of me, I didn't even ask your name"
		};

		TextBox introText;

		public IntroScreen() : base()
		{
			Messages.InsertRange(3, Enum.GetNames(typeof(GoalManager.Types)));
			for (int i = 0; i < Messages.Count; i++)
			{
				Messages[i] = Messages[i].ToLower();
			}
			BackgroundColor = Color.Black;

			introText = new TextBox(this);
			introText.Area = ScreenManager.Instance.VirtualScreen.VirtualArea;
			GetMessage();

			GameObject input = new GameObject();
			InputComponent ic = new InputComponent(input);
			ic.AddEvent(Keys.Enter, Input.Pressed, (o, e) =>
			{
				if (!GetMessage())
				{
					ScreenManager.Instance.ChangeScreens(this, new CharacterCreationScreen());
				}
			});
			GameObjects.Add(input);
		}

		bool GetMessage()
		{
			if (Messages.Count > 0)
			{
				string s = Messages.First();
				Messages.RemoveAt(0);
				introText.Text = s;
				return true;
			}
			return false;
		}
	}
}
