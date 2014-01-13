using UnityEngine;
using System.Collections;

public class OutofBoundsKill : MonoBehaviour {

	private Camera miniMap;
	private Transform player;
	private Transform foreground;

	public GameObject antibody;

	private int numAntibodies;
	private int maxAntibodies = 170;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		foreground = GameObject.FindGameObjectWithTag("Foreground").transform;
	}
	
	void FixedUpdate () {
		if (GameState.Instance.outOfBounds) {
			ChasePlayer.isChasing = true;
			if(numAntibodies <= maxAntibodies){
				GameObject antibodyInstance = (GameObject)Instantiate (antibody);
				antibodyInstance.transform.parent = foreground;

				Vector3 spawnPosition = new Vector3(player.position.x,player.position.y,800);
				Vector2 playerVelocity = player.rigidbody2D.velocity;
				spawnPosition -= new Vector3(playerVelocity.x, playerVelocity.y, 0);

				antibodyInstance.transform.position = spawnPosition;

				numAntibodies++;
			}
		}
		else{
			ChasePlayer.isChasing=false;
		}
	}
}
