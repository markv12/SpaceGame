using UnityEngine;
using System.Collections;

public class PlasmaShotScript : MonoBehaviour {

	public int plasmaShotMass = 1;

	void Start() {
		// Destroy plasma shot after 5 seconds
		Destroy (gameObject, 5);
	}
}
