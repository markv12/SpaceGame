using UnityEngine;
using System.Collections;

public class RangeIndicatorScript : MonoBehaviour {

	public float distance = 40;
	private Vector3 rangeIndicatorPosition;
	private Vector3 rangeIndicatorAngle;
	private Vector3 rangeIndicatorSize;
	private SpriteRenderer spriteRenderer;
	private Color spriteColor;


	void Start() {
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		spriteColor = spriteRenderer.color;

		/* We make it transparent for now, we want to display it 1/20 of a second after mouse has been down,
		 * because some people don't want the indicator if they just fire away like mad */
		spriteRenderer.color = new Color (spriteColor.r, spriteColor.g, spriteColor.b, 0f);
		StartCoroutine (MakeVisible ());
	}

	IEnumerator MakeVisible() {
		yield return new WaitForSeconds(0.05f);
		spriteRenderer.color = new Color (spriteColor.r, spriteColor.g, spriteColor.b, 255f);
	}

	void Update() {
		DisplayIndicator ();
	}

	void FixedUpdate() {
		gameObject.transform.position = rangeIndicatorPosition;
		gameObject.transform.localEulerAngles = rangeIndicatorAngle;
		gameObject.transform.localScale = rangeIndicatorSize;
	}

	public void DisplayIndicator() {
		Vector3 shipPosition = GameObject.Find ("Player").transform.position;
		Vector3 shipSize = renderer.bounds.size;
		
		// Actual mouse position, finally, turns out it's heavily influenced by camera's distance from player:
		Vector3 mousePosition = MouseWorldPosition ();
		
		/* Making ship a "circle" starting "hitbox" of it's smallest size component
		 * so that projectile distance is always equivalent regardless of 
		 * ship's nose direction.
		 */
		Vector3 shipToMouseVector = mousePosition - shipPosition;
		
		//Final values!
		float myMagnitude = Mathf.Sqrt (
			Mathf.Pow (shipToMouseVector.x, 2) + Mathf.Pow (shipToMouseVector.y, 2));

		// We'll want (since arrow is facing down, 270) 90 + angle of shipToMouseVector:
		float angleOfShipToMouseVector = Mathf.Atan (shipToMouseVector.y / shipToMouseVector.x);
		if (shipToMouseVector.x >= 0f && shipToMouseVector.y < 0f) {
			angleOfShipToMouseVector += 2f * Mathf.PI;
		}
		else if (shipToMouseVector.x < 0f) {
			angleOfShipToMouseVector += Mathf.PI;
		}
		angleOfShipToMouseVector *= Mathf.Rad2Deg;

		rangeIndicatorPosition = shipPosition + (shipToMouseVector * (1.0f / 2.0f));
		rangeIndicatorAngle = new Vector3 (0f, 0f, angleOfShipToMouseVector);

		float sizeXRangeIndicator = Mathf.Sqrt (
			Mathf.Pow(shipToMouseVector.x, 2) + Mathf.Pow (shipToMouseVector.y, 2))/5f;
		rangeIndicatorSize = new Vector3 (sizeXRangeIndicator, 0.25f, 0.5f);
	}

	public Vector3 MouseWorldPosition() {
		Vector3 cameraPosition = Camera.main.transform.position;
		Vector3 playerPosition = GameObject.Find ("Player").transform.position;
		
		float cameraToShipDistance = Mathf.Abs (cameraPosition.z - playerPosition.z);

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    
		Vector3 point = ray.origin + (ray.direction * cameraToShipDistance);
		return point;
	}
}