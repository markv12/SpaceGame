using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

	public int checkPointNumber= 0;

	void OnTriggerExit2D(Collider2D collider)
	{
		GameState.Instance.enterOpenSpace ();
	}
	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.rigidbody2D !=null){
			GameState.Instance.lastCheckPointNumber = checkPointNumber;
			if(GameState.Instance.InOpenSpace){
				collider.rigidbody2D.velocity/=2.5f;
			}
			GameState.Instance.exitOpenSpace ();
		}
	}
}
