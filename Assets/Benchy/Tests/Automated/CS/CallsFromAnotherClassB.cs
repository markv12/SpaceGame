using UnityEngine;
using System.Collections;

public class CallsFromAnotherClassB : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
		CallsFromAnotherClassA classA = gameObject.GetComponent<CallsFromAnotherClassA>();
		classA.ThisIsASharedMethod();
	}
}
