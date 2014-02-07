using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

	public int checkPointNumber= 0;
	
	void OnTriggerExit2D(Collider2D collider)
	{
		Vector3 colliderPosition = collider.transform.position;
		Vector3 relativePosition = getRelativePosition (transform, colliderPosition);

		if(relativePosition.y>0){
			GameState.Instance.LastCheckPointNumber = checkPointNumber;
		}
		else{
			if(GameState.Instance.LastCheckPointNumber>=1){
				GameState.Instance.LastCheckPointNumber = checkPointNumber-1;
			}
		}

		GameState.Instance.enterOpenSpace ();
	}
	void OnTriggerEnter2D(Collider2D collider)
	{
		Rigidbody2D collidedRigidbody = collider.rigidbody2D;
		if(collidedRigidbody !=null){
			Vector3 colliderPosition = collider.transform.position;
			Vector3 relativePosition = getRelativePosition (transform, colliderPosition);
			if(relativePosition.y>0){
				if(GameState.Instance.LastCheckPointNumber>=1){
					GameState.Instance.LastCheckPointNumber = checkPointNumber-1;
				}
			}
			else{
				GameState.Instance.LastCheckPointNumber = checkPointNumber;
			}

			if(GameState.Instance.InOpenSpace){
				collidedRigidbody.velocity/=4f;
			}
			GameState.Instance.exitOpenSpace ();
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
