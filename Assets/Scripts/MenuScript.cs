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

	public GameObject progressBar;
	public GameObject progressBarCover;
	public GameObject startMessage;

	private float buttonPosX;
	private float buttonPosY;

	void Start()
	{
		buttonPosX = Screen.width / 2 - (buttonWidth / 2);
		buttonPosY = (Screen.height * 0.75f) - (buttonHeight / 2);

		skin = Resources.Load("StartButton") as GUISkin;
		DontDestroyOnLoad(GameState.Instance);
		GameState.Instance.startState();


		float scaleFactor = 0.3f;
		float pWidth = progressBar.transform.localScale.x;
		float pHeight = progressBar.transform.localScale.y;
		float coverWidth = progressBarCover.transform.localScale.x;
		float coverHeight = progressBarCover.transform.localScale.y;
		float xPosition = Screen.width / 2 - (pWidth / 2);
		float yPosition = (Screen.height * 0.75f) - (pHeight / 2);
	}

	void Update(){
		if(Application.CanStreamedLevelBeLoaded(1)){
			if(progressBar.transform.localScale.x !=0){
				progressBar.transform.localScale = new Vector3(0,0,0);
				startMessage.transform.localScale = new Vector3(0.7f,0.7f,1f);
			}
		}
		else{
			Vector3 currentScale = progressBarCover.transform.localScale;
			progressBarCover.transform.localScale = new Vector3(1f-Application.GetStreamProgressForLevel (1), currentScale.y, currentScale.z);
		}

		if(Input.GetKeyDown ("x")){
			GameState.Instance.LoadLevel(1);
		}
		
	}
	
	/*void OnGUI(){
		GUI.skin = skin;
		if (Application.CanStreamedLevelBeLoaded(1)){

			// Draw a button to start the game
			if (
				GUI.Button(
					new Rect(
					Screen.width / 2 - (buttonWidth / 2),
					(Screen.height * 0.75f) - (buttonHeight / 2),
					buttonWidth,
					buttonHeight
					),
					""
				)
			){
				// On Click, load the first level.
				GameState.Instance.LoadLevel(1);
			}
		}
		else{

			float scaleFactor = 0.3f;
			float progress = Application.GetStreamProgressForLevel(1);
			float pWidth = progressBar.width*scaleFactor;
			float pHeight = progressBar.height*scaleFactor;
			float coverWidth = progressBarCover.width*scaleFactor;
			float coverHeight = progressBarCover.height*scaleFactor;
			float xPosition = Screen.width / 2 - (pWidth / 2);
			float yPosition = (Screen.height*0.75f) - (pHeight / 2);

			GUI.DrawTexture(new Rect(xPosition, 
			                         yPosition, 
			                         pWidth, 
			                         pHeight),
			                progressBar);

			GUI.DrawTexture(new Rect(xPosition+5f*scaleFactor+(coverWidth*progress),
			                         yPosition+5f*scaleFactor,
			                         coverWidth - (coverWidth*progress),
			                         coverHeight),
			                progressBarCover);

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
	}*/
}
