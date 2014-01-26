using UnityEngine;
using System.Collections;

public class FightingControlsScript : MonoBehaviour {

	// For now just look for firing input
	void Update () {
		/* Because we use GetButtonDown, to show the rangeIndicator, so you can aim, then fire as soon as you let go
		 * But this also makes it easy to just click down->up in a short time so you can fire quicker. */
		bool shoot = Input.GetButtonUp ("Fire1");
		bool aim = Input.GetButtonDown ("Fire1");

		PlasmaWeaponScript weaponScript = GetComponent<PlasmaWeaponScript>();
		if (aim) {
			if(weaponScript != null) {
				weaponScript.AimProjectile();
			}			
		}
		if (shoot) {
			if(weaponScript != null) {
				weaponScript.Attack();
			}
		}
	}
}
