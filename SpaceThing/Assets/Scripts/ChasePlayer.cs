using UnityEngine;
using System.Collections;

public class ChasePlayer : MonoBehaviour {

	private Transform player;

	public static bool isChasing;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(isChasing){
			Vector3 differenceVector = transform.position - player.position;
			differenceVector = (differenceVector / differenceVector.magnitude) * -2f;

			transform.position += differenceVector;
		}
	}
}
