using UnityEngine;
using System.Collections;

public class MiniMapFollow : MonoBehaviour {

	private Transform player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		/*
		float jumpAmount = camera.orthographicSize * 2;

		Vector3 playerRelPosition = camera.WorldToViewportPoint(player.position);
		Vector3 currentPosition = camera.transform.position;
		Vector3 newPosition = Vector3.zero;
		if (playerRelPosition.x < 0f){
			newPosition = new Vector3(currentPosition.x-jumpAmount, currentPosition.y, currentPosition.z);
		}
		else if(playerRelPosition.x > 1f){
			newPosition = new Vector3(currentPosition.x+jumpAmount, currentPosition.y, currentPosition.z);
		}
		else if(playerRelPosition.y < 0f){
			newPosition = new Vector3(currentPosition.x, currentPosition.y-jumpAmount, currentPosition.z);
		}
		else if(playerRelPosition.y > 1f){
			newPosition = new Vector3(currentPosition.x, currentPosition.y+jumpAmount, currentPosition.z);
		}

		if(newPosition != Vector3.zero){
			camera.transform.position = newPosition;
		}
		*/
		GetComponent<Camera>().transform.position = new Vector3 (player.position.x, player.position.y, GetComponent<Camera>().transform.position.z);
	}
}
