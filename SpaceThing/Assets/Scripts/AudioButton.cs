using UnityEngine;
using System.Collections;

public class AudioButton : MonoBehaviour {

	public Texture2D onIcon;
	public Texture2D offIcon;

	private static bool isAudioOn = true;
	private System.Collections.Generic.List<GameObject> musicSources;

	void OnGUI () {
		if(isAudioOn){
			if (GUI.Button (new Rect (10,10,45,45), onIcon)){
				musicSources.ForEach(audioOff);
				isAudioOn = false;
			}
		}
		else{
			if (GUI.Button (new Rect (10,10,45,45), offIcon)){
				musicSources.ForEach(audioOn);
				isAudioOn = true;
			}
		}

	}

	// Use this for initialization
	void Start () {
		musicSources = new System.Collections.Generic.List<GameObject>();
		musicSources.AddRange (GameObject.FindGameObjectsWithTag("MusicSource"));
		if(isAudioOn){
			musicSources.ForEach(audioOn);
		}
		else{
			musicSources.ForEach(audioOff);
		}
	}

	private static void audioOff(GameObject toTurnOff){
		toTurnOff.audio.Pause ();
	}
	private static void audioOn(GameObject toTurnOn){
		toTurnOn.audio.Play();
	}
}
