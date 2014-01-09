using UnityEngine;
using System.Collections;

/// <summary>
/// Title screen script
/// </summary>
public class MenuScript : MonoBehaviour
{
	private GUISkin skin;

	const int buttonWidth = 100;
	const int buttonHeight = 60;

	
	void Start()
	{
		// Load a skin for the buttons
		skin = Resources.Load("StartButton") as GUISkin;
	}
	
	void OnGUI()
	{
		GUI.skin = skin;
		if (Application.CanStreamedLevelBeLoaded("GravityGame")){

			// Draw a button to start the game
			if (
					GUI.Button(
						new Rect(
						Screen.width / 2 - (buttonWidth / 2),
						(Screen.height*0.75f) - (buttonHeight / 2),
						buttonWidth,
						buttonHeight
						),
						"Start!"
					)
				)
			{
				// On Click, load the first level.
				Application.LoadLevel("GravityGame");
			}
		}
		else{
			GUI.Label(
					new Rect(
					Screen.width / 2 - (buttonWidth / 2),
					(Screen.height*0.75f) - (buttonHeight / 2),
					buttonWidth* Application.GetStreamProgressForLevel("GravityGame"),
					buttonHeight
					),
					"Loading..."
			);
		}
	}
}
