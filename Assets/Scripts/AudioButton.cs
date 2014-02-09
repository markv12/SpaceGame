using UnityEngine;
using System.Collections;

public class AudioButton : MonoBehaviour {

	public Texture2D onIcon;
	public Texture2D offIcon;
	
	void OnGUI () {
		if(CentralAudio.Instance.musicPlaying){
			if (GUI.Button (new Rect (10,10,45,45), onIcon)){
				CentralAudio.Instance.pauseMusic();
			}
		}
		else{
			if (GUI.Button (new Rect (10,10,45,45), offIcon)){
				CentralAudio.Instance.resumeMusic();
			}
		}

	}
}
