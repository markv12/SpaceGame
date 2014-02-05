using UnityEngine;
using System.Collections;

public class TinyLoader : MonoBehaviour {

	public GameObject tinyBar;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.CanStreamedLevelBeLoaded (1)) {
			GameState.Instance.loadStartScreen();
		}
		else{
			Vector3 progTransform = new Vector3(Application.GetStreamProgressForLevel(1), 1, 1);
			tinyBar.transform.localScale = progTransform;
		}
	}
}
