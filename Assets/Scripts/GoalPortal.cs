using UnityEngine;
using System.Collections;

public class GoalPortal : MonoBehaviour {
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		ShipControls ship = collider.gameObject.GetComponent<ShipControls>();
		if(ship != null){
			ship.deactivateShip();
			GameState.Instance.LastCheckPointNumber = 0;
			CameraFade.StartAlphaFade( Color.black, false, 1.25f, 0.4f, () => { GameState.Instance.loadNextLevel(); });
		}
	}
}
