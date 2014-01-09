using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	public float xSmooth = 22f;		// How smoothly the camera catches up with it's target movement in the x axis.
	public float ySmooth = 22f;		// How smoothly the camera catches up with it's target movement in the y axis.
	
	private Transform player;		// Reference to the player's transform.


	void Awake ()
	{
		// Setting up the reference.
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}


	void FixedUpdate ()
	{
		TrackPlayer();
	}
	
	
	void TrackPlayer ()
	{
		// By default the target x and y coordinates of the camera are it's current x and y coordinates.
		float targetX = transform.position.x;
		float targetY = transform.position.y;

		// ... the target x coordinate should be a Lerp between the camera's current x position and the player's current x position.
		targetX = Mathf.Lerp(transform.position.x, player.position.x, xSmooth * Time.deltaTime);

		// ... the target y coordinate should be a Lerp between the camera's current y position and the player's current y position.
		targetY = Mathf.Lerp(transform.position.y, player.position.y, ySmooth * Time.deltaTime);

		transform.position = new Vector3(targetX, targetY, transform.position.z);
	}
}
