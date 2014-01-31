using UnityEngine;
using System.Collections;

public class Crashing : MonoBehaviour {

	private ShipControls ship;

	void Start(){
		ship = GetComponent<ShipControls> ();
	}

	private IEnumerator WaitThenRestart(float seconds) {
		yield return new WaitForSeconds(seconds);
		ship.moveToLastCheckpoint ();
		ship.activateShip ();
	}

	void OnCollisionEnter2D(Collision2D collision){
		if(Mathf.Abs(transform.position.z - collision.transform.position.z)<3f && GameState.Instance.playerActive){
			ship.die();

			Vector2 collisionPoint = collision.contacts[0].point;

			SpecialEffectsHelper.Instance.ShipExplosion (new Vector3(collisionPoint.x, collisionPoint.y));
			StartCoroutine(WaitThenRestart (1f));
		}
	}
}
