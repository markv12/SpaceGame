using UnityEngine;
using System.Collections;

public class BeginningGravity : MonoBehaviour {

	// Use this for initialization
	public bool gravityOn;

	void Start(){
		PlanetGravity.gravityOn = this.gravityOn;
	}
}
