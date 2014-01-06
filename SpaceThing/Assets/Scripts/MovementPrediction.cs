using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovementPrediction : MonoBehaviour {

	public GameObject dotSprite;
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
		Vector2 pVelocity = player.rigidbody2D.velocity/3.5f;
		Vector3 pVelocity3D = new Vector3 (pVelocity.x, pVelocity.y,0);

		List<Vector3> predictions = calculateFuturePositions(player.transform.position, pVelocity3D, dots.Count);

		for (int i=0; i < dots.Count; i++){
			dots[i].transform.position = predictions[i];
		}
	}

	/* Attempting to draw curves between movement prediction sprites. 
	void OnGUI(){
		float width = HandleUtility.GetHandleSize(Vector3.zero) * 1f;
		for (int i=0; i < dots.Count-1; i++){
			Handles.DrawBezier (dots[i].transform.position, 
			                    dots[i+1].transform.position, 
			                    Vector3.up, 
			                    -Vector3.up,
			                    Color.red, 
			                    null,
			                    width);
		}

	}
	*/

	private List<Vector3> calculateFuturePositions(Vector3 position, Vector3 velocity, int stepNum){
		return calculateFuturePositions (position,velocity,stepNum,new List<Vector3>());
	}
	private List<Vector3> calculateFuturePositions(Vector3 position, Vector3 velocity, int stepNum, List<Vector3>futurePositions){
		if(stepNum == 0){
			return futurePositions;
		}
		else{
			List<Vector3> gravityEffects = new List<Vector3> ();
			foreach(PlanetGravity gravity in gravityScripts){
				gravityEffects.Add(gravity.calculateGravity(position));
			}
			Vector3 newVelocity = velocity  + averageVectors(gravityEffects);
			Vector3 newPosition = position+ newVelocity;
			newPosition.z=1f;
			futurePositions.Add(newPosition);

			return calculateFuturePositions(newPosition, newVelocity,stepNum-1, futurePositions);
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
