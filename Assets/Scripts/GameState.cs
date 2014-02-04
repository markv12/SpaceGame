using UnityEngine;
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
	private AudioSource aSource;
	private AudioSource audioSource{
		get{
			if(aSource == null){
				aSource = gameObject.AddComponent<AudioSource>();
				aSource.clip = Resources.Load("Progenibeat") as AudioClip;
				aSource.loop = true;
				DontDestroyOnLoad (aSource);
			}
			return aSource;
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
		Application.LoadLevel (0);
	}
	public void loadNextLevel(){
		Application.LoadLevel (Application.loadedLevel+1);
		SaveState ();
	}

	void Start(){
		LoadCheckPoints ();
		Application.targetFrameRate = 60;	
	}
	
	private void SaveState(){
		PlayerPrefs.SetInt (LEVELKEY, Application.loadedLevel);
		PlayerPrefs.SetInt (FUELKEY, fuelUsed);
	}

	public void loadState(){
		if(!PlayerPrefs.HasKey(LEVELKEY) || PlayerPrefs.GetInt(LEVELKEY)==0){
			PlayerPrefs.SetInt(LEVELKEY, 1);
		}
		Application.LoadLevel(PlayerPrefs.GetInt(LEVELKEY));

		if(!PlayerPrefs.HasKey(FUELKEY)){
			PlayerPrefs.SetInt(FUELKEY, 0);
		}
		fuelUsed = PlayerPrefs.GetInt (FUELKEY);

	}

	void OnGUI () {
		GUI.Label(new Rect (10, 60, 100, 20), "Fuel Used: " +fuelUsed);
	}
	
	void OnLevelWasLoaded(){
		LoadCheckPoints ();
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

	public void playMusic(){
		if(!audioSource.isPlaying){
			audioSource.Stop ();
			audioSource.Play ();
		}
	}

	public void pauseMusic(){
		audioSource.Pause ();
	}

	public void resumeMusic(){
		if(!audioSource.isPlaying){
			audioSource.Play ();
		}
	}

	public bool musicPlaying{
		get{
			return audioSource.isPlaying;
		}
	}

	public void enterOpenSpace(){
		Instance.inOpenSpace = true;
		PlanetGravity.globalGravityOn = true;
	}
	public void exitOpenSpace(){
		Instance.inOpenSpace = false;
		PlanetGravity.globalGravityOn = false;
	}
}
