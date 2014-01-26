using UnityEngine;
using System.Collections;

public class ManualTestSharedCallsA : MonoBehaviour {

	void Start () {
	
	}
	
	int counter = 0;
	[NeverProfileMethod]
	void Update () {
		Benchy.Begin();
		if (counter > 100)
		{
			counter = 0;
			ThisIsASharedMethod();
		}
		counter++; 
		Benchy.End();
	}
	
	[NeverProfileMethod]
	public void ThisIsASharedMethod()
	{
		Benchy.Begin();
		// A method that's as about as useful as the rest of the class :)
		float x = 3*2.3938384f;
		double y = Mathf.Sqrt(x);
		y = y + .99f;
		Benchy.End();
	}
	
}
