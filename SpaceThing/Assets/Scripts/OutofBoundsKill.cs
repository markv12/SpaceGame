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
		miniMap = GameObject.FindGameObjectWithTag ("MiniMap").GetComponent<Camera>();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		foreground = GameObject.FindGameObjectWithTag("Foreground").transform;
	}
	
	void FixedUpdate () {
		Vector3 pos = miniMap.WorldToViewportPoint(player.position);
		if (pos.x < -0.1f || pos.x > 1.1f || pos.y < -0.1f || pos.y > 1.1f) {
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
