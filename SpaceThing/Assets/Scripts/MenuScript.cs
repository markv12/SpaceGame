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

	public Texture2D progressBar;
	public Texture2D progressBarCover;
	
	void Start()
	{
		skin = Resources.Load("StartButton") as GUISkin;
		DontDestroyOnLoad(GameState.Instance);
		GameState.Instance.startState();
	}
	
	void OnGUI()
	{
		GUI.skin = skin;
		if (Application.CanStreamedLevelBeLoaded(1)){

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
				GameState.Instance.LoadLevel(1);
			}
		}
		else{
			float scaleFactor = 0.25f;
			float progress = Application.GetStreamProgressForLevel(1);
			float pWidth = progressBar.width*scaleFactor;
			float pHeight = progressBar.height*scaleFactor;
			float xPosition = Screen.width / 2 - (pWidth / 2);
			float yPosition = (Screen.height*0.75f) - (pHeight / 2);

			GUI.DrawTexture(new Rect(xPosition, 
			                         yPosition, 
			                         pWidth, 
			                         pHeight),
			                progressBar);

			GUI.DrawTexture(new Rect(xPosition+5f*scaleFactor+(500*scaleFactor*progress),
			                         yPosition+5f*scaleFactor,
			                         500*scaleFactor - (500*progress),
			                         150*scaleFactor), progressBarCover);
			GUI.Label(
				new Rect(
				Screen.width / 2 - (buttonWidth / 2),
				(Screen.height*0.75f) - (buttonHeight / 2),
				buttonWidth,
				buttonHeight
				),
				"Loading..."
			);
		}
	}
}
