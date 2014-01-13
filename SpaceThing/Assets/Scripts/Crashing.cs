using UnityEngine;
using System.Collections;

public class Crashing : MonoBehaviour {

	private ShipControls ship;

	void Start(){
		ship = GetComponent<ShipControls> ();
	}

	private IEnumerator WaitThenRestart(float seconds) {
		yield return new WaitForSeconds(seconds);
		CheckPoint lastPoint = GameState.Instance.getLastCheckPoint();
		Vector3 checkPointPosition = lastPoint.transform.position;
		ship.transform.position = new Vector3 (checkPointPosition.x, checkPointPosition.y, ship.transform.position.z);
		ship.rigidbody2D.angularVelocity = 0f;
		ship.transform.rotation = lastPoint.transform.rotation;
		ship.transform.Rotate (0,0,90);
		ship.activateShip ();
	}

	void OnCollisionEnter2D(Collision2D collision){
		if(Mathf.Abs(transform.position.z - collision.transform.position.z)<3f && GameState.Instance.playerActive){
			if(collision.gameObject.GetComponent<GoalPortal>()!=null){
				ship.deactivateShip ();
			}
			else{
				ship.die();
			}

			Vector2 collisionPoint = collision.contacts[0].point;

			SpecialEffectsHelper.Instance.ShipExplosion (new Vector3(collisionPoint.x, collisionPoint.y));
			StartCoroutine(WaitThenRestart (1f));
		}
	}
}
