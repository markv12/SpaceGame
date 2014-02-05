using UnityEngine;
using System.Collections;

public class TinyLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.CanStreamedLevelBeLoaded (2)) {
			GameState.Instance.loadStartScreen();
		}
	}
}
