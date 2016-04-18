using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PotentialHappiness.Characters;
using PotentialHappiness.Components;
using PotentialHappiness.GameObjects;
using PotentialHappiness.Interface;
using PotentialHappiness.Map;
using PotentialHappiness.Map.Areas;

namespace PotentialHappiness.Screens
{
	public class CharacterCreationScreen : GameScreen
	{
		public CharacterCreationScreen() : base()
		{
			BackgroundColor = Color.Black;

			TextBox titleText = new TextBox(this);
			titleText.Area = ScreenManager.Instance.VirtualScreen.VirtualArea;
			titleText.Text = "my name is";
			titleText.Alignment = Extensions.GraphicsExtensions.Alignment.Top;

			EditableTextBox nameText = new EditableTextBox(this);
			int spacing = ScreenManager.Instance.Font.LineSpacing * 3;
			Rectangle nameBox = ScreenManager.Instance.VirtualScreen.VirtualArea;
			nameBox.Y += spacing;
			nameBox.Height -= spacing;
			nameText.Area = nameBox;
			nameText.Text = "LUNA";

			GameObject input = new GameObject();
			InputComponent ic = new InputComponent(input);
			ic.AddEvent(Keys.Enter, Input.Pressed, (o, e) =>
			{
				GoalManager.Instance.Reload();

				MapScreen map = new MapScreen();
				PlayableCharacter player = new PlayableCharacter(nameText.Text, Color.Black, MapManager.Instance.CurrentMap);
				CharacterManager.Instance.CurrentCharacter = player;

				Room startingRoom = MapManager.Instance.CurrentMap.Features.Find((f) => f is Room) as Room;
				player.SetPosition(startingRoom.Bounds.Center.X, startingRoom.Bounds.Center.Y);

				PopupTextBox ptb = new PopupTextBox(map);
				ptb.Text = "phew ! after three whole days travelling, let's go see whether these magic gems are as powerful as i was told";
				ptb.OnClose += (oo, ee) =>
				{
					PopupTextBox infoBox = new PopupTextBox(map);
					infoBox.Color = Color.Gray;
					infoBox.Text = "move around using the [arrow] keys. collect the regular gems that are on the floor to keep yourself from fading away";
				};

				ScreenManager.Instance.ChangeScreens(this, map);
			});
			GameObjects.Add(input);
		}
	}
}
