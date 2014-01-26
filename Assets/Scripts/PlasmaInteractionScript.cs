using UnityEngine;
using System.Collections;

public class PlasmaInteractionScript : MonoBehaviour {

	public int originalMassNeededForTransfer = 1;
	public bool canBeCoveredInPlasma = false;
	public Color originalColor;

	private int massNeededForTransfer;
	private SpriteRenderer spriteRenderer;

	void Start() {
		massNeededForTransfer = originalMassNeededForTransfer;
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		originalColor = spriteRenderer.color;
	}

	public void ResetSettings() {
		spriteRenderer.color = originalColor;
		massNeededForTransfer = originalMassNeededForTransfer;
	}
	void OnTriggerEnter2D(Collider2D collider) {
		PlasmaShotScript shotScript = collider.gameObject.GetComponent<PlasmaShotScript> ();
		if (shotScript != null) {
			if(canBeCoveredInPlasma) {
				MoveScriptPlasma moveScript = collider.gameObject.GetComponent<MoveScriptPlasma>();
				massNeededForTransfer -= shotScript.plasmaShotMass;

				/* BEFORE! Before we destroy the collider's game object, 
				 * have it talk to collider's -> Parent -> WeaponScript to set canTransferToTrue
				 * To: Prevent further shots from being fired, and to allow transfer (swap positions) */

				GameObject colliderParent = moveScript.parentWhoShot;
				PlasmaWeaponScript parentWeaponScript = colliderParent.GetComponent<PlasmaWeaponScript>();

				Destroy (shotScript.gameObject);

				if(massNeededForTransfer <= 0) {
					// Change color, stop movement, and allow transfer.
					parentWeaponScript.canTransfer = true;

					// Instead of ^boolean, just set objectToTransferWith, and in WeaponScript, check if that object is null
					parentWeaponScript.objectToTransferWith = gameObject;

					spriteRenderer.color = Color.green;
				}
			}
		}
	}
}
