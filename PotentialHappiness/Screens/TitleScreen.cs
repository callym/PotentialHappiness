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
	public class TitleScreen : GameScreen
	{
		public TitleScreen() : base()
		{
			BackgroundColor = Color.Black;

			TextBox titleText = new TextBox();
			titleText.Area = ScreenManager.Instance.VirtualScreen.VirtualArea;
			titleText.Text = "Potential\nHappiness";
			GameObjects.Add(titleText);

			GameObject input = new GameObject();
			InputComponent ic = new InputComponent(input);
			ic.AddEvent(Keys.Space, Input.Pressed, (o, e) => { ScreenManager.Instance.ChangeScreens(this, new CharacterCreationScreen()); });
			GameObjects.Add(input);
		}
	}
}
