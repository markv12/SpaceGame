using UnityEngine;
using System.Collections;

public class MouseInformationDisplay : MonoBehaviour {

	public string text;
	public bool display = false;

	private string positionString;
	private string angleString;
	private string shipToMouseString;
	private Random random = new Random ();

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<GUIText>().text = "0.0";
	}
	
	// Update is called once per frame
	void Update () {
		//float currentValue = float.Parse (gameObject.guiText.text);
		//currentValue += 0.1f;
		//gameObject.guiText.text = currentValue.ToString ("R");

		//int randomNumber = random.next(
		//To update string values:
		PositionAndAngleInfo ();

		gameObject.GetComponent<GUIText>().text = positionString + "\n" + shipToMouseString + "\n" + angleString;
	}

	public Vector3 MouseWorldPosition() {
		Vector3 cameraPosition = Camera.main.transform.position;
		Vector3 playerPosition = GameObject.Find ("Player").transform.position;
		
		float cameraToShipDistance = Mathf.Abs (cameraPosition.z - playerPosition.z);
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    
		Vector3 point = ray.origin + (ray.direction * cameraToShipDistance);
		return point;
	}	
	public void PositionAndAngleInfo() {
		GameObject player = GameObject.Find ("Player");
		Vector3 shipPosition = player.transform.position;//transform.position;
		Vector3 shipSize = player.GetComponent<Renderer>().bounds.size;
		
		// Actual mouse position, finally, turns out it's heavily influenced by camera's distance from player:
		Vector3 mousePosition = MouseWorldPosition ();
		
		/* Making ship a "circle" starting "hitbox" of it's smallest size component
		 * so that projectile distance is always equivalent regardless of 
		 * ship's nose direction.
		 */
		Vector3 shipToMouseVector = mousePosition - shipPosition;
		
		//Final values!
		Vector3 rangeIndicatorPosition = shipPosition + (shipToMouseVector * (1.0f / 2.0f));
		
		float angleOfShipToMouseVector = Mathf.Atan (shipToMouseVector.y / shipToMouseVector.x);
		if (shipToMouseVector.x >= 0f && shipToMouseVector.y < 0f) {
			angleOfShipToMouseVector += 2f * Mathf.PI;
		}
		else if (shipToMouseVector.x < 0f) {
			angleOfShipToMouseVector += Mathf.PI;
		}
		angleOfShipToMouseVector *= Mathf.Rad2Deg;

		string pos = rangeIndicatorPosition.ToString("R");
		string stmString = shipToMouseVector.ToString ("R");
		string ang = angleOfShipToMouseVector.ToString("R");
		float sizeXRangeIndicator = Mathf.Sqrt (
			Mathf.Pow(shipToMouseVector.x, 2) + Mathf.Pow (shipToMouseVector.y, 2));
		positionString = "Position: " + pos;
		shipToMouseString = "ShipToMouse: " + stmString + "\nDistance: " + sizeXRangeIndicator;
		angleString = "Angle: " + ang;
	}
}
