using UnityEngine;
using System.Collections;
using System;


public class PlanetGravity : MonoBehaviour {
	private Rigidbody2D player;

	public float gravityFactor = 8000f;

	public static bool globalGravityOn = true;

	public int checkPointGroupNumber = 0;

	private GameState.ChangedEventHandler handler;
	private bool isShuttingDown = false;

	private bool gravityOn = false;
	private Transform gravityRing;
	private const float maxRingDiameter = 2f;
	private float currentRingDiameter = 0;

	void Start () {
		handler = new GameState.ChangedEventHandler (ActiveCheckpointChanged);
		player = GameObject.FindGameObjectWithTag ("Player").rigidbody2D;
		GameState.Instance.ActiveCheckpointChanged += handler;
		foreach (Transform child in transform) {
			this.gravityRing = child;
			break;
		}

		ActiveCheckpointChanged (null, null);
	}

	void OnDestroy () {
		if(!isShuttingDown){
			GameState.Instance.ActiveCheckpointChanged -= handler;
		}
	}

	public void OnApplicationQuit(){
		isShuttingDown = true;
	}

	void Update(){
		if(gravityOn){
			growRing();
		}
		else{
			shrinkRing();
		}
	}

	private void growRing(){
		if(currentRingDiameter <= maxRingDiameter){
			currentRingDiameter += 0.1f;
			gravityRing.localScale = new Vector3(currentRingDiameter,currentRingDiameter,1);
		}
	}

	private void shrinkRing(){
		if(currentRingDiameter > 0){
			currentRingDiameter -= 0.1f;
			gravityRing.localScale = new Vector3(currentRingDiameter,currentRingDiameter,1);
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(player != null && globalGravityOn && gravityOn){
			player.AddForce(calculateGravity (player.transform.position));
		}
	}

	public Vector3 calculateGravity(Vector3 objectPosition){
		Vector3 direction = (transform.position - objectPosition);
		return (gravityFactor*direction)/ direction.sqrMagnitude;
	}

	private void ActiveCheckpointChanged(object sender, EventArgs e){
		if(checkPointGroupNumber == GameState.Instance.LastCheckPointNumber){
			gravityOn = true;
		}
		else{
			gravityOn = false;
		}
	}
}
