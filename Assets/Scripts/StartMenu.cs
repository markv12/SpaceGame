using UnityEngine;
using System.Collections;

/// <summary>
/// Title screen script
/// </summary>
public class StartMenu : MonoBehaviour
{
	private const int buttonWidth = 100;
	private const int buttonHeight = 60;
	private enum Options { Start, Resume };

	public GameObject progressBar;
	public GameObject progressBarCover;
	public GameObject startMessage;
	public GameObject resumeMessage;
	public GameObject selectionGraphic;
	
	private Options selectedItem = Options.Start;

	void Start()
	{
		DontDestroyOnLoad(GameState.Instance);
		GameState.Instance.startState();
	}

	void Update(){
		if(Application.CanStreamedLevelBeLoaded(GameState.Instance.firstLevel)){
			if(progressBar.transform.localScale.x !=0){
				progressBar.transform.localScale = new Vector3(0,0,0);
				startMessage.transform.localScale = new Vector3(1f,1f,1f);
				resumeMessage.transform.localScale = new Vector3(1f,1f,1f);
				selectionGraphic.transform.localScale = new Vector3(1f,1f,1f);
			}
		}
		else{
			Vector3 currentScale = progressBarCover.transform.localScale;
			progressBarCover.transform.localScale = new Vector3(1f-Application.GetStreamProgressForLevel (GameState.Instance.firstLevel), currentScale.y, currentScale.z);
		}

		if(Input.GetButtonDown ("Enter")){
			CentralAudio.Instance.playMusic();

			if(selectedItem == Options.Start){
				CameraFade.StartAlphaFade( Color.black, false, 2f, 0f, () => { GameState.Instance.loadNextLevel(); });
			}
			else if(selectedItem == Options.Resume){
				CameraFade.StartAlphaFade( Color.black, false, 2f, 0f, () => { GameState.Instance.loadState(); });
			}
		}
		else if(Input.GetButtonDown ("Brake")){
			if(selectedItem != Options.Resume){
				selectionGraphic.transform.position = resumeMessage.transform.position;
				selectedItem = Options.Resume;
				CentralAudio.Instance.playClick();
			}

		}
		else if(Input.GetButtonDown ("Thrust")){
			if(selectedItem != Options.Start){
				selectionGraphic.transform.position = startMessage.transform.position;	
				selectedItem = Options.Start;
				CentralAudio.Instance.playClick();
			}
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
