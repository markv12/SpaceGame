using UnityEngine;
using System.Collections;

public class CallsFromAnotherClassA : MonoBehaviour {

	void Start () {
	
	}
	
	int counter = 0;
	void Update () {
		if (counter > 100)
		{
			counter = 0;
			ThisIsASharedMethod();
		}
		counter++; 
	}
	
	public void ThisIsASharedMethod()
	{
		// A method that's as about as useful as the rest of the class :)
		float x = 3*2.3938384f;
		double y = Mathf.Sqrt(x);
		y = y + .99f;
	}
}
