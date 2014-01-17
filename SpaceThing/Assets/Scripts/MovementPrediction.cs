using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovementPrediction : MonoBehaviour {

	public GameObject dotSprite;
	public int numDots = 10;

	private List<GameObject> primaryDots;
	private List<GameObject> averageDots;
	private Transform player;
	private List<PlanetGravity> gravityScripts; 
	private Transform foreground;
	
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		foreground = GameObject.FindGameObjectWithTag("Foreground").transform;

		generateDots();

		getPlanetGravityScripts ();
	}
	
	void FixedUpdate () {
		if(GameState.Instance.playerActive){
			makePredictions();
		}
	}

	private void generateDots(){
		primaryDots = new List<GameObject>();
		averageDots = new List<GameObject>();
		for (int i=0; i < numDots; i++)
		{
			GameObject newDot = (GameObject)Instantiate (dotSprite);
			float dotScale = 1f-((1f/numDots)*i);
			newDot.transform.localScale = new Vector3(dotScale,dotScale,1);
			newDot.transform.parent=foreground;
			primaryDots.Add(newDot);
		}
		for (int i=0; i < numDots-1; i++)
		{
			GameObject newDot = (GameObject)Instantiate (dotSprite);
			newDot.transform.localScale = primaryDots[i].transform.lossyScale;
			newDot.transform.parent=foreground;
			averageDots.Add(newDot);
		}
	}

	private void makePredictions(){
		Vector2 pVelocity = player.rigidbody2D.velocity/3.5f;
		Vector3 pVelocity3D = new Vector3 (pVelocity.x, pVelocity.y,0);

		//Benchy.Begin("Whole Calculate Predictions");
		List<Vector3> predictions = calculateFuturePositions(player.transform.position, pVelocity3D, primaryDots.Count);
		//Benchy.End("Whole Calculate Predictions");
		//Benchy.Begin ("Apply Main Predictions");
		for (int i=0; i < primaryDots.Count; i++){
			primaryDots[i].transform.position = predictions[i];
		}
		//Benchy.End ("Apply Main Predictions");
		//Benchy.Begin ("Apply Average Predictions");
		for (int i=0; i < primaryDots.Count-1; i++){
			averageDots[i].transform.position = (predictions[i]+predictions[i+1])/2f;
		}
		//Benchy.End ("Apply Average Predictions");
	}

	private void getPlanetGravityScripts(){
		gravityScripts = new List<PlanetGravity> ();
		foreach (GameObject planet in GameObject.FindGameObjectsWithTag("Planet")) {
			gravityScripts.Add(planet.GetComponent<PlanetGravity>());
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
			//Benchy.Begin("Calculate Gravity Part");
			foreach(PlanetGravity gravity in gravityScripts){
				gravityEffects.Add(gravity.calculateGravity(position));
			}
			//Benchy.End("Calculate Gravity Part");
			//Benchy.Begin ("Adjust for Next Calculation");
			Vector3 newVelocity = velocity  + averageVectors(gravityEffects);
			Vector3 newPosition = position+ newVelocity;
			newPosition.z=1f;
			futurePositions.Add(newPosition);
			//Benchy.End ("Adjust for Next Calculation");
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
