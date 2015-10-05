using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class OutofBoundsKill : MonoBehaviour {

	private Camera miniMap;
	private GameObject player;
	private Transform foreground;

	public GameObject antibody;
	private List<GameObject> antibodies;

	private int maxAntibodies = 150;
	
	// Use this for initialization
	void Start () {
		antibodies = new List<GameObject>();

		player = GameObject.FindGameObjectWithTag ("Player");
		player.GetComponent<ShipControls> ().ShipActivated += new ShipControls.ChangedEventHandler (ShipActivated);

		foreground = GameObject.FindGameObjectWithTag("Foreground").transform;
	}
	
	void FixedUpdate () {
		if (GameState.Instance.outOfBounds) {
			ChasePlayer.isChasing = true;
			if(antibodies.Count <= maxAntibodies){
				Vector3 spawnPosition = new Vector3(player.transform.position.x,player.transform.position.y,800);
				Vector2 playerVelocity = player.GetComponent<Rigidbody2D>().velocity;
				spawnPosition -= new Vector3(playerVelocity.x, playerVelocity.y, 0);

				GameObject antibodyInstance = (GameObject)Instantiate (antibody);
				antibodyInstance.transform.parent = foreground;
				antibodyInstance.transform.position = spawnPosition;
				antibodies.Add(antibodyInstance);
			}
		}
		else{
			ChasePlayer.isChasing=false;
		}
	}

	private void ShipActivated(object sender, EventArgs e) 
	{
		antibodies.ForEach (Destroy);
		antibodies.Clear ();
	}
}
