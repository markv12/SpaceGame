using UnityEngine;
using System.Collections;

public class Crashing : MonoBehaviour {

	private ShipControls ship;

	void Start(){
		ship = GetComponent<ShipControls> ();
	}

	private void activateAtLastCheckpoint() {
		ship.moveToLastCheckpoint ();
		ship.activateShip ();
	}

	void OnCollisionEnter2D(Collision2D collision){
		if(Mathf.Abs(transform.position.z - collision.transform.position.z)<3f && GameState.Instance.playerActive){
			ship.die();
			Vector2 collisionPoint = collision.contacts[0].point;

			SpecialEffectsHelper.Instance.ShipExplosion (new Vector3(collisionPoint.x, collisionPoint.y));
			GameState.Instance.WaitThenCall(0.6f, () => { 
				activateAtLastCheckpoint();
			});
			CameraFade.StartAlphaFade(new Color(0.2f,0,0,1), false, 0.8f, 0.3f);

		}
	}
}
