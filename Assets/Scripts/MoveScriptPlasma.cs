using UnityEngine;
using System.Collections;

public class MoveScriptPlasma : MonoBehaviour {

	public Vector3 directionUnitVector;
	public float speed = 12.0f;
	public GameObject parentWhoShot;

	private Vector2 movement;


	/* Values get changed in update, but actual movement and positioning
	 * should be done in FixedUpdate */
	void Update() {
		movement = new Vector2 (
			directionUnitVector.x * speed,
			directionUnitVector.y * speed);
	}

	void FixedUpdate() { 
		// We don't move gameObject, we move its RigidBody2D
		GetComponent<Rigidbody2D>().velocity = movement;
	}
}
