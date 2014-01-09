using UnityEngine;
using System.Collections;

public class PlanetGravity : MonoBehaviour {
	private const float AVERAGEFRAMERATE = 0.02f;

	private Rigidbody2D player;

	public float gravityFactor = 8000f;

	public static bool gravityOn = true;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").rigidbody2D;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(player != null){
			Vector3 frameAcceleration = calculateGravity (player.transform.position);
			player.velocity += new Vector2 (frameAcceleration.x,frameAcceleration.y);
		}
	}

	public Vector3 calculateGravity(Vector3 objectPosition){
		Vector3 gravitationalAcceleration;
		if(gravityOn){
			Vector3 pos = objectPosition;
			Vector3 acc = Vector3.zero;
			Vector3 direction = (transform.position - pos);
			acc += (gravityFactor*direction)/ direction.sqrMagnitude; 
			gravitationalAcceleration = acc * (Time.fixedDeltaTime / AVERAGEFRAMERATE);
		}
		else{
			gravitationalAcceleration = Vector3.zero;
		}

		return gravitationalAcceleration;
	}
}
