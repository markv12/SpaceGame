using UnityEngine;
using System.Collections;

public class AudioButton : MonoBehaviour {

	public Texture2D onIcon;
	public Texture2D offIcon;
	
	void OnGUI () {
		if(GameState.Instance.musicPlaying){
			if (GUI.Button (new Rect (10,10,45,45), onIcon)){
				GameState.Instance.pauseMusic();
			}
		}
		else{
			if (GUI.Button (new Rect (10,10,45,45), offIcon)){
				GameState.Instance.resumeMusic();
			}
		}

	}
}
