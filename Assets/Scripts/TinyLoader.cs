using UnityEngine;
using System.Collections;

public class TinyLoader : MonoBehaviour {

	public GameObject tinyBar;
	private bool fadeOutStarted;
	private bool fadeInStarted;
	private bool fadeInCompleted;

	// Use this for initialization
	void Start () {
		fadeOutStarted = false;
		fadeInStarted = false;
		fadeInCompleted = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(!fadeInStarted){
			CameraFade.StartAlphaFade( Color.black, true, 2f, 0f, () => { fadeInCompleted = true; } );
			fadeInStarted = true;
		}
		else{
			if (Application.CanStreamedLevelBeLoaded (1) && fadeInCompleted) {
				if(!fadeOutStarted){
					CameraFade.StartAlphaFade( Color.black, false, 2f, 0f, () => { GameState.Instance.loadStartScreen(); });
					fadeOutStarted = true;
				}
				tinyBar.transform.localScale = new Vector3(1, 1, 1);
			}
			else{
				Vector3 progTransform = new Vector3(Application.GetStreamProgressForLevel(1), 1, 1);
				tinyBar.transform.localScale = progTransform;
			}
		}
	}
}
