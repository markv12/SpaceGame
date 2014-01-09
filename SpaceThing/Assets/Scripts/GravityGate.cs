using UnityEngine;
using System.Collections;

public class GravityGate : MonoBehaviour {

	void OnTriggerExit2D(Collider2D collider)
	{
		PlanetGravity.gravityOn = true;
	}
}

