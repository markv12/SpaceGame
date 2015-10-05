using UnityEngine;
using System.Collections;

public class Spinning : MonoBehaviour {

	public float speed = -30f;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D>().angularVelocity = speed;
	}
}
