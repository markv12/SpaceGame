using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovementPrediction : MonoBehaviour {

	public Transform dotSprite;
	public int numDots = 16;

	private List<GameObject> dots;
	private Transform player;
	private List<PlanetGravity> gravityScripts; 
	private Transform foreground;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").transform;
		foreground = GameObject.FindGameObjectWithTag("Foreground").transform;

		dots = new List<GameObject>();

		for (int i=0; i < numDots; i++)
		{
			GameObject newDot = (GameObject)Instantiate (dotSprite);
			float dotScale = 1f-((1f/numDots)*i);
			newDot.transform.localScale = new Vector3(dotScale,dotScale,1);
			dots.Add(newDot);
		}
		foreach(GameObject dot in dots){
			dot.transform.parent = foreground;
		}


		gravityScripts = new List<PlanetGravity> ();
		foreach (GameObject planet in GameObject.FindGameObjectsWithTag("Planet")) {
			gravityScripts.Add(planet.GetComponent<PlanetGravity>());
		} 
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector2 pVelocity = player.rigidbody2D.velocity/14;
		Vector3 pVelocity3D = new Vector3 (pVelocity.x, pVelocity.y,0);
		for (int i=0; i < dots.Count; i++){
			dots[i].transform.position = calculateFuturePosition (player.transform.position, pVelocity3D, i+1);
		}
		//Debug.Log (calculateFuturePosition (player.transform.position, pVelocity3D, 10));
	}

	private Vector3 calculateFuturePosition(Vector3 position, Vector3 velocity, int stepNum){
		if(stepNum == 0){
			return position;
		}
		else{
			List<Vector3> gravityEffects = new List<Vector3> ();
			foreach(PlanetGravity gravity in gravityScripts){
				gravityEffects.Add(gravity.calculateGravity(position));
			}
			Vector3 newPosition = position+ velocity  + averageVectors(gravityEffects);

			return calculateFuturePosition(newPosition, velocity,stepNum-1);
		}
	}

	private Vector3 averageVectors(List<Vector3> vectors){
		Vector3 sum = Vector3.zero;
		foreach(Vector3 vector in vectors){
			sum += vector;
		}
		return sum;
	}
}
