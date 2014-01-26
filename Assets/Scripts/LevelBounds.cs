using UnityEngine;
using System.Collections;

public class LevelBounds : MonoBehaviour {

	void OnTriggerExit2D(Collider2D collider)
	{
		if(collider.gameObject.tag == "Player"){
			GameState.Instance.outOfBounds = true;
			//Debug.Log ("Left Space");
		}
	}
	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.tag == "Player"){
			GameState.Instance.outOfBounds = false;
			//Debug.Log ("Entered Space");
		}

	}
}
