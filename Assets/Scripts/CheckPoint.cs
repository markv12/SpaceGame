using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

	public int checkPointNumber= 0;

	public Rigidbody2D currentObject;

	void OnTriggerExit2D(Collider2D collider)
	{
		Vector3 colliderPosition = collider.transform.position;
		Vector3 relativePosition = getRelativePosition (transform, colliderPosition);

		if(relativePosition.y>0){

		}

		GameState.Instance.enterOpenSpace ();
	}
	void OnTriggerEnter2D(Collider2D collider)
	{
		Rigidbody2D collidedRigidbody = collider.rigidbody2D;
		if(collidedRigidbody !=null){
			GameState.Instance.LastCheckPointNumber = checkPointNumber;
			if(GameState.Instance.InOpenSpace){
				collidedRigidbody.velocity/=4f;
			}
			GameState.Instance.exitOpenSpace ();
			currentObject = collidedRigidbody;
		}
	}
	void FixedUpdate(){
		if(!GameState.Instance.InOpenSpace 
		   && GameState.Instance.LastCheckPointNumber == checkPointNumber 
		   && currentObject != null
		   && GameState.Instance.playerActive
		   && Input.GetButton("Auto")){
			currentObject.AddForce(transform.up*60f);
		}
	}

	public static Vector3 getRelativePosition(Transform origin, Vector3 position) {
		Vector3 distance = position - origin.position;
		Vector3 relativePosition = Vector3.zero;
		relativePosition.x = Vector3.Dot(distance, origin.right.normalized);
		relativePosition.y = Vector3.Dot(distance, origin.up.normalized);
		relativePosition.z = Vector3.Dot(distance, origin.forward.normalized);
		
		return relativePosition;
	}
}
