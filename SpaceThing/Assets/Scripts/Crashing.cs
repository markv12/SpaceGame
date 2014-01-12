using UnityEngine;
using System.Collections;

public class Crashing : MonoBehaviour {

	private ShipControls ship;

	void Start(){
		ship = GetComponent<ShipControls> ();
	}

	private IEnumerator WaitThenRestart(float seconds) {
		yield return new WaitForSeconds(seconds);
		GameState.Instance.LoadLevel(Application.loadedLevel);
	}

	void OnCollisionEnter2D(Collision2D collision){
		if(Mathf.Abs(transform.position.z - collision.transform.position.z)<3f){
			if(collision.gameObject.GetComponent<GoalPortal>()!=null){
				ship.destroyShip ();
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
