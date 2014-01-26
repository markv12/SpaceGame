using UnityEngine;
using System.Collections;

public class ManualTestSharedCallsC : MonoBehaviour {

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
