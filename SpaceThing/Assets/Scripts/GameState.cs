using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState : MonoBehaviour {

	private static GameState instance;
	public static GameState Instance{
		get{
			if(instance == null){
				instance = new GameObject("GameState").AddComponent<GameState>();
			}	
			return instance;
		}
	}

	public bool playerActive{
		get; set;
	}

	public bool isLevelStarted{
		get{
			return PlanetGravity.gravityOn;
		}
	}

	private bool inOpenSpace;
	public bool InOpenSpace {
		get{
			return inOpenSpace;
		}
	}

	public bool outOfBounds{
		get; set;
	}

	public int lastCheckPointNumber {
		get; set;
	}

	private Dictionary<int, CheckPoint>checkPoints;

	public CheckPoint getLastCheckPoint(){
		return checkPoints[lastCheckPointNumber];
	}

	public void startState(){
		instance.exitOpenSpace ();
		instance.inOpenSpace = false;
		instance.outOfBounds = false;
	}

	public void OnApplicationQuit(){
		instance = null;
	}

	public void LoadLevel (string levelName){
		exitOpenSpace ();
		Application.LoadLevel (levelName);
	}

	public void LoadLevel (int levelNumber){
		exitOpenSpace ();
		Application.LoadLevel (levelNumber);
	}

	private void LoadCheckPoints(){
		instance.checkPoints = new Dictionary<int, CheckPoint>();
		GameObject[] checkPointObjects = GameObject.FindGameObjectsWithTag("Checkpoint");
		foreach(GameObject checkpointObject in checkPointObjects){
			CheckPoint checkPoint = checkpointObject.GetComponent<CheckPoint>();
			instance.checkPoints.Add(checkPoint.checkPointNumber, checkPoint);
		}
		//Debug.Log ("Loaded " + checkPointObjects.Length + " points");
	}

	void Start(){
		LoadCheckPoints ();
	}
	void OnLevelWasLoaded(){
		LoadCheckPoints ();
	}

	public void enterOpenSpace(){
		instance.inOpenSpace = true;
		PlanetGravity.gravityOn = true;
	}
	public void exitOpenSpace(){
		instance.inOpenSpace = false;
		PlanetGravity.gravityOn = false;
	}
}
