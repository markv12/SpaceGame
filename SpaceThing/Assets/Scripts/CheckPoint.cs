using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

	public int checkPointNumber= 0;

	public Rigidbody2D currentObject;

	void OnTriggerExit2D(Collider2D collider)
	{
		GameState.Instance.enterOpenSpace ();
	}
	void OnTriggerEnter2D(Collider2D collider)
	{
		Rigidbody2D collidedRigidbody = collider.rigidbody2D;
		if(collidedRigidbody !=null){
			GameState.Instance.lastCheckPointNumber = checkPointNumber;
			if(GameState.Instance.InOpenSpace){
				collidedRigidbody.velocity/=4f;
			}
			GameState.Instance.exitOpenSpace ();
			currentObject = collidedRigidbody;
		}
	}
	void FixedUpdate(){
		if(!GameState.Instance.InOpenSpace 
		   && GameState.Instance.lastCheckPointNumber == checkPointNumber 
		   && currentObject != null
		   && GameState.Instance.playerActive
		   && Input.GetButton("Auto")){
			currentObject.AddForce(transform.up*60f);
		}
	}
}
