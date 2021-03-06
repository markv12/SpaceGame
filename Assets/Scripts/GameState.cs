﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class GameState : MonoBehaviour {
	private const String LEVELKEY = "currentLevel";
	private const String FUELKEY = "fuelUsage";

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

	private int lastCheckPointNumber = 0;
	public int LastCheckPointNumber {
		get{
			return lastCheckPointNumber;
		}
		set{
			//Debug.Log(value);
			if (value != lastCheckPointNumber){
				lastCheckPointNumber = value;
				if(ActiveCheckpointChanged != null){
					ActiveCheckpointChanged(this, EventArgs.Empty);
				}
			}
		}
	}

	public int fuelUsed;
	public int fuelUsedLastCheckpoint;

	private Dictionary<int, CheckPoint>checkPoints;

	public CheckPoint getLastCheckPoint(){
		if(checkPoints.ContainsKey(LastCheckPointNumber)){
			return checkPoints[lastCheckPointNumber];
		}
		else{
			return checkPoints[0];
		}
	}

	void Update(){
		if (Input.GetKeyDown ("r")) {
			Application.LoadLevelAdditive("PauseMenuScene");
		}
		if(Input.GetKeyDown("e")){
			GameObject pMenu = GameObject.FindGameObjectWithTag("PauseMenu");
			if(pMenu != null){
				Destroy(pMenu);
			}
		}
	}

	public void startState(){
		Instance.exitOpenSpace ();
		Instance.inOpenSpace = false;
		Instance.outOfBounds = false;

		fuelUsed = 0;
		fuelUsedLastCheckpoint = 0;
	}

	public void OnApplicationQuit(){
		instance = null;
	}

	public void loadStartScreen(){
		Application.LoadLevel (1);
	}

	public int firstLevel{
		get{
			return 2;
		}
	}

	public void loadNextLevel(){
		Application.LoadLevel (Application.loadedLevel+1);
	}

	void Start(){
		LoadCheckPoints ();
		Application.targetFrameRate = 60;	
	}
	
	private void SaveState(){
		if(Application.loadedLevel>1){
			PlayerPrefs.SetInt (LEVELKEY, Application.loadedLevel);
			PlayerPrefs.SetInt (FUELKEY, fuelUsed);
		}
		else{
			PlayerPrefs.SetInt (LEVELKEY, 2);
			PlayerPrefs.SetInt (FUELKEY, 0);
		}
	}

	public void loadState(){
		if(!PlayerPrefs.HasKey(LEVELKEY)){
			PlayerPrefs.SetInt(LEVELKEY, firstLevel);
		}
		Application.LoadLevel(PlayerPrefs.GetInt(LEVELKEY));

		if(!PlayerPrefs.HasKey(FUELKEY)){
			PlayerPrefs.SetInt(FUELKEY, 0);
		}
		fuelUsed = PlayerPrefs.GetInt (FUELKEY);

	}

	/*void OnGUI () {
		if(fuelUsed >0){
			GUI.Label(new Rect (10, 60, 100, 20), "Fuel Used: " +fuelUsed);
		}
	}*/
	
	void OnLevelWasLoaded(){
		LoadCheckPoints ();
		SaveState ();

		Application.targetFrameRate = 50;
		CameraFade.StartAlphaFade( Color.black, true, 1.2f, 0f, () => { });
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
		GravityObject.globalGravityOn = true;
	}
	public void exitOpenSpace(){
		Instance.inOpenSpace = false;
		GravityObject.globalGravityOn = false;
	}

	public  void WaitThenCall(float seconds, Action toCall) {
		StartCoroutine(WaitThenCallYield(seconds, toCall));
	}
	private IEnumerator WaitThenCallYield(float seconds, Action toCall){
		yield return new WaitForSeconds(seconds);
		if( toCall != null ){
			toCall();
		}
	}
}
