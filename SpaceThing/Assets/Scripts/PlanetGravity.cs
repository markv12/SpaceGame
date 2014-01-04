using UnityEngine;
using System.Collections;

public class PlanetGravity : MonoBehaviour {
	private const float AVERAGEFRAMERATE = 0.02f;

	private GameObject player;

	public float gravityFactor = 8000f;
	
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 frameAcceleration = calculateGravity (player.transform.position);
		//Debug.Log (adjustedAcceleration.ToString());
		player.rigidbody2D.velocity += new Vector2 (frameAcceleration.x,frameAcceleration.y);
	}

	public Vector3 calculateGravity(Vector3 objectPosition){
		Vector3 pos = objectPosition;
		Vector3 acc = Vector3.zero;
		Vector3 direction = (transform.position - pos);
		acc += (gravityFactor*direction)/ direction.sqrMagnitude; 
		Vector3 adjustedAcceleration = acc * (Time.fixedDeltaTime / AVERAGEFRAMERATE);
		return new Vector3 (adjustedAcceleration.x,adjustedAcceleration.y,0);
	}
}
