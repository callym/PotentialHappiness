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
		public EndGameScreen(bool win) : base()
		{
			BackgroundColor = Color.Black;

			TextBox titleText = new TextBox(this);
			titleText.Area = ScreenManager.Instance.VirtualScreen.VirtualArea;
			if (win)
			{
				titleText.Text = "Against all odds, you have become happy.";
			}
			else
			{
				titleText.Text = "You have died.";
			}
			titleText.Alignment = Extensions.GraphicsExtensions.Alignment.Top;

			GameObject input = new GameObject();
			InputComponent ic = new InputComponent(input);
			ic.AddEvent(Keys.Enter, Input.Pressed, (o, e) =>
			{
				ScreenManager.Instance.ChangeScreens(this, new TitleScreen());
			});
			GameObjects.Add(input);
		}
	}
}
