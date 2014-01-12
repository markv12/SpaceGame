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
		skin = Resources.Load("StartButton") as GUISkin;
		DontDestroyOnLoad(GameState.Instance);
		GameState.Instance.startState();
	}
	
	void OnGUI()
	{
		GUI.skin = skin;
		if (Application.CanStreamedLevelBeLoaded("Level1")){

			// Draw a button to start the game
			if (
					GUI.Button(
						new Rect(
						Screen.width / 2 - (buttonWidth / 2),
						(Screen.height*0.75f) - (buttonHeight / 2),
						buttonWidth,
						buttonHeight
						),
						""
					)
				)
			{
				// On Click, load the first level.
				GameState.Instance.LoadLevel("Level1");
			}
		}
		else{
			GUI.Label(
					new Rect(
					Screen.width / 2 - (buttonWidth / 2),
					(Screen.height*0.75f) - (buttonHeight / 2),
					buttonWidth* Application.GetStreamProgressForLevel("Level1"),
					buttonHeight
					),
					"Loading..."
			);
		}
	}
}
