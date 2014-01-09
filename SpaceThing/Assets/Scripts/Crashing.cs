using UnityEngine;
using System.Collections;

public class Crashing : MonoBehaviour {

	private IEnumerator WaitThenRestart(float seconds) {
		yield return new WaitForSeconds(seconds);
		Application.LoadLevel(Application.loadedLevel);
	}


	void OnCollisionEnter2D(Collision2D collision){
		GetComponent<ShipControls> ().destroyShip ();

		Vector2 collisionPoint = collision.contacts[0].point;

		SpecialEffectsHelper.Instance.ShipExplosion (new Vector3(collisionPoint.x, collisionPoint.y));
		StartCoroutine(WaitThenRestart (1f));
	}
}
