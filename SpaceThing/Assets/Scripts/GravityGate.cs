using UnityEngine;
using System.Collections;

public class GravityGate : MonoBehaviour {

	public bool onGate=true;

	void OnTriggerExit2D(Collider2D collider)
	{
		if(onGate){
			GameState.Instance.turnGravityOn();
		}
		else{
			GameState.Instance.turnGravityOff();
		}
	}
}

