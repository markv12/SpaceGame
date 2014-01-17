using UnityEngine;
using System.Collections;

public class EndMenu : MonoBehaviour {

	private CameraSpeedZoom cameraZoom;

	const int buttonWidth = 70;
	const int buttonHeight = 40;

	private float buttonPosX;
	private float buttonPosY;

	// Use this for initialization
	void Start () {
		cameraZoom = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraSpeedZoom> ();
		buttonPosX = Screen.width / 2 - (buttonWidth / 2);
		buttonPosY = (Screen.height * 0.86f) - (buttonHeight / 2);
	}
	
	// Update is called once per frame
	void OnGUI () {
		if(cameraZoom.gameObject.transform.position.z == -cameraZoom.fovNarrow){
			// Draw a button to start the game
			if (
				GUI.Button(
				new Rect(
				buttonPosX,
				buttonPosY,
				buttonWidth,
				buttonHeight
				),
				"Restart"
				)
			){
				// On Click, load the first level.
				GameState.Instance.LoadLevel(0);
			}
		}
	}
}
