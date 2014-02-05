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
		fadeLogoWithStreamingLoad();
	}

	private void fadeLogoWithStreamingLoad(){
		if (Application.GetStreamProgressForLevel(1) >= 1f) {
			if(!fadeOutStarted){
				CameraFade.StartAlphaFade( Color.black, false, 2f, 0.5f, () => { GameState.Instance.loadStartScreen(); });
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
