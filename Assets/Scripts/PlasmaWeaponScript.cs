using UnityEngine;
using System.Collections;

public class PlasmaWeaponScript : MonoBehaviour {

	// Need to allow us to pick which sprite/particleEffect we use as a prefab for display
	public Transform plasmaShotPrefab;
	public Transform rangeIndicatorPrefab;

	public float shootingRate = 0.25f;

	private float shootCooldown;

	public bool canTransfer;
	public GameObject objectToTransferWith;
	// We can instantiate it once, since it will always be something that exists, we just make it
	// visible or invisible depending on if it's shown
	private Transform rangeIndicatorTransform;
	private RangeIndicatorScript rangeIndicatorScript;
	void Start() {
		objectToTransferWith = null;
		shootCooldown = 0f;
		canTransfer = false;
	}

	void Update() {
		if (shootCooldown > 0.0f) {
			shootCooldown -= Time.deltaTime;
		}
	}

	public void AimProjectile() {
		if (objectToTransferWith == null) {
			rangeIndicatorTransform = Instantiate (rangeIndicatorPrefab) as Transform;
			rangeIndicatorScript = rangeIndicatorTransform.gameObject.GetComponent<RangeIndicatorScript> ();
		}
	}

	// Now, we instantiate a new plasmaShotPrefab if possible
	public void Attack() {
		if (rangeIndicatorTransform != null) {
			Destroy (rangeIndicatorTransform.gameObject);
		}
		// First, we need to check if player can transfer, if so, they can't fire more plasma
		if(objectToTransferWith != null) {
			// Now for some Captain Ginyu shit:
			Vector3 playerPosition = transform.position;

			transform.position = objectToTransferWith.transform.position;
			objectToTransferWith.transform.position = playerPosition;

			objectToTransferWith.GetComponent<PlasmaInteractionScript>().ResetSettings();
			objectToTransferWith = null;
		}
		else if (CanAttack) {
			shootCooldown = shootingRate;

			/* Now we instantiate a new shot and position it according to the 
			 * PlasmaWeaponScript's current position */
			var plasmaShotTransform = Instantiate (plasmaShotPrefab) as Transform;

			plasmaShotTransform.position = transform.position;

			//Make weapon shoot in direction of mouse
			MoveScriptPlasma moveScript = plasmaShotTransform.gameObject.GetComponent<MoveScriptPlasma>();
			moveScript.parentWhoShot = transform.gameObject;

			if(moveScript != null) {
				/* Must use ScreenToWorldPoint, since Input.mousePosition gets the mouse's actual position on the
				 * computer screen, so if your game window is half the size, in the middle, you'll get all kinds of 
				 * messed up values */
				Vector3 worldMousePosition = Camera.main.ScreenPointToRay (Input.mousePosition).direction;
				// Turns out .magnitude is always returning 1... So I'll have to compute myself
				float computedMagnitude = Mathf.Sqrt(
					Mathf.Pow(worldMousePosition.x, 2.0f) + Mathf.Pow(worldMousePosition.y, 2.0f));

				Vector3 mouseDirectionUnit = worldMousePosition * (1.0f / computedMagnitude);

				moveScript.directionUnitVector = mouseDirectionUnit;
			}
		}
	}

	public bool CanAttack {
		get {
			return shootCooldown <= 0f;
		}
	}
}
