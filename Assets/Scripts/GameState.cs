using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


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

	public delegate void ChangedEventHandler(object sender, EventArgs e);
	public event ChangedEventHandler ActiveCheckpointChanged;

	private int lastCheckPointNumber = -1;
	public int LastCheckPointNumber {
		get{
			return lastCheckPointNumber;
		}
		set{
			//Debug.Log(value + " " + lastCheckPointNumber);
			if (value != lastCheckPointNumber){
				lastCheckPointNumber = value;
				if(ActiveCheckpointChanged != null){
					ActiveCheckpointChanged(this, EventArgs.Empty);
				}
			}
		}
	}

	private Dictionary<int, CheckPoint>checkPoints;


	public CheckPoint getLastCheckPoint(){
		return checkPoints[lastCheckPointNumber];
	}

	public void startState(){
		Instance.exitOpenSpace ();
		Instance.inOpenSpace = false;
		Instance.outOfBounds = false;
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

	void Start(){
		LoadCheckPoints ();
		Application.targetFrameRate = 50;
		
	}
	
	void OnLevelWasLoaded(){
		LastCheckPointNumber = -1;
		LoadCheckPoints ();
		Application.targetFrameRate = 50;
	}

	private void LoadCheckPoints(){
		Instance.checkPoints = new Dictionary<int, CheckPoint>();
		GameObject[] checkPointObjects = GameObject.FindGameObjectsWithTag("Checkpoint");
		foreach(GameObject checkpointObject in checkPointObjects){
			CheckPoint checkPoint = checkpointObject.GetComponent<CheckPoint>();
			Instance.checkPoints.Add(checkPoint.checkPointNumber, checkPoint);
		}
	}

	public void enterOpenSpace(){
		Instance.inOpenSpace = true;
		PlanetGravity.gravityOn = true;
	}
	public void exitOpenSpace(){
		Instance.inOpenSpace = false;
		PlanetGravity.gravityOn = false;
	}
}
