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

			TextBox titleText = new TextBox(this);
			titleText.Area = ScreenManager.Instance.VirtualScreen.VirtualArea;
			titleText.Text = "potential\nhappiness";

			TextBox enterText = new TextBox(this);
			enterText.Area = ScreenManager.Instance.VirtualScreen.VirtualArea;
			enterText.Text = "[enter]";
			enterText.Alignment = Extensions.GraphicsExtensions.Alignment.Bottom;
			enterText.Color = Color.Gray;

			GameObject input = new GameObject();
			InputComponent ic = new InputComponent(input);
			ic.AddEvent(Keys.Enter, Input.Pressed, (o, e) => { ScreenManager.Instance.ChangeScreens(this, new IntroScreen()); });
			GameObjects.Add(input);
		}
	}
}
