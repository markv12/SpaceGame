using UnityEngine;
using System.Collections;
using System;


public class PlanetGravity : MonoBehaviour {
	private Rigidbody2D player;

	public float gravityFactor = 8000f;

	public static bool gravityOn = true;

	public int checkPointGroupNumber = 0;

	private SpriteRenderer gravityRing;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").rigidbody2D;
		GameState.Instance.ActiveCheckpointChanged += new GameState.ChangedEventHandler (ActiveCheckpointChanged);

		foreach (Transform child in transform) {
			SpriteRenderer renderer = child.GetComponent<SpriteRenderer> ();
			if(renderer != null){
				this.gravityRing = renderer;
				break;
			}
		}

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(player != null){
			Vector3 frameAcceleration = calculateGravity (player.transform.position);
			player.AddForce(new Vector2 (frameAcceleration.x,frameAcceleration.y));
		}
	}

	public Vector3 calculateGravity(Vector3 objectPosition){
		Vector3 gravitationalAcceleration;
		if(gravityOn && checkPointGroupNumber == GameState.Instance.LastCheckPointNumber){
			Vector3 direction = (transform.position - objectPosition);
			gravitationalAcceleration = (gravityFactor*direction*1f)/ direction.sqrMagnitude;
		}
		else{
			gravitationalAcceleration = Vector3.zero;
		}
		return gravitationalAcceleration;
	}

	private void ActiveCheckpointChanged(object sender, EventArgs e){
		if(checkPointGroupNumber == GameState.Instance.LastCheckPointNumber){
			gravityRing.enabled=true;
		}
		else{
			gravityRing.enabled=false;
		}
	}
}
