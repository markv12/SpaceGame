using UnityEngine;
using System.Collections;

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
	public bool isLevelStarted{
		get{
			return PlanetGravity.gravityOn;
		}
	}
	
	public void startState(){
		Debug.Log ("Game State Started");
		instance.turnGravityOff ();
	}

	public void OnApplicationQuit(){
		instance = null;
	}

	public void LoadLevel (string levelName){
		turnGravityOff ();
		Application.LoadLevel (levelName);
	}

	public void LoadLevel (int levelNumber){
		turnGravityOff ();
		Application.LoadLevel (levelNumber);
	}

	public void turnGravityOn(){
		PlanetGravity.gravityOn = true;
	}
	public void turnGravityOff(){
		PlanetGravity.gravityOn = false;
	}
	public void toggleGravity(){
		PlanetGravity.gravityOn = !PlanetGravity.gravityOn;
	}
}
