using UnityEngine;
using System.Collections;

public class ShipGravity : MonoBehaviour {
	private const float AVERAGEFRAMERATE = 0.02f;

	private GameObject player;

	public float gravityFactor = 8000f;
	
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 pos = player.transform.position;
		Vector3 acc = Vector3.zero;
		Vector3 direction = (transform.position - pos);
		acc += (gravityFactor*direction)/ direction.sqrMagnitude; 
		Vector3 adjustedAcceleration = acc * (Time.fixedDeltaTime / AVERAGEFRAMERATE);
		//Debug.Log (adjustedAcceleration.ToString());
		player.rigidbody2D.velocity += new Vector2 (adjustedAcceleration.x,adjustedAcceleration.y);
	}
}
