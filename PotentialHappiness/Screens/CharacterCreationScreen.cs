using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PotentialHappiness.Characters;
using PotentialHappiness.Components;
using PotentialHappiness.GameObjects;
using PotentialHappiness.Interface;
using PotentialHappiness.Map;

namespace PotentialHappiness.Screens
{
	public class CharacterCreationScreen : GameScreen
	{
		public CharacterCreationScreen() : base()
		{
			BackgroundColor = Color.Black;

			TextBox titleText = new TextBox();
			titleText.Area = ScreenManager.Instance.VirtualScreen.VirtualArea;
			titleText.Text = "What is your name?";
			titleText.Alignment = Extensions.GraphicsExtensions.Alignment.Top;
			GameObjects.Add(titleText);

			EditableTextBox nameText = new EditableTextBox();
			int spacing = ScreenManager.Instance.Fonts[nameText.Font].LineSpacing * 3;
			Rectangle nameBox = ScreenManager.Instance.VirtualScreen.VirtualArea;
			nameBox.Y += spacing;
			nameBox.Height -= spacing;
			nameText.Area = nameBox;
			nameText.Text = "callym";
			GameObjects.Add(nameText);

			GameObject input = new GameObject();
			InputComponent ic = new InputComponent(input);
			ic.AddEvent(Keys.Enter, Input.Pressed, (o, e) =>
			{
				MapScreen map = new MapScreen();
				PlayableCharacter player = new PlayableCharacter(nameText.Text, Color.Black);
				CharacterManager.Instance.CurrentCharacter = player;
				ScreenManager.Instance.ChangeScreens(this, map);
			});
			GameObjects.Add(input);
		}
	}
}
