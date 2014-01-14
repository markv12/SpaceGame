using UnityEngine;
using System.Collections;

public class ManualTestSharedCallsB : MonoBehaviour {

	void Start () {
	
	}
	
	[NeverProfileMethod]
	void Update () {
		Benchy.Begin();
		ManualTestSharedCallsA classA = gameObject.GetComponent<ManualTestSharedCallsA>();
		classA.ThisIsASharedMethod();
		Benchy.End();
	}
}
