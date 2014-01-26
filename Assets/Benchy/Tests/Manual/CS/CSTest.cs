using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class CSTest : MonoBehaviour
{
	
	int totalCallsToUpdate = 0;
	
	void Update ()
	{
		totalCallsToUpdate++;
		Render ();
		if (totalCallsToUpdate > 10) {
			CalculateStuff ();
			totalCallsToUpdate = 0;
		}
	}
	
	void OnGUI ()
	{
	}
	
	Dictionary<int,bool> primeNumbers = new Dictionary<int,bool> ();
	
	//[NeverProfileMethod]
	// Just another example of how to mix and match manually profiled sections with auto sections
	void CalculateStuff () 
	{
		Benchy.Begin ();
		int nextNumber = Random.Range (0, 1000000);
		if (IsPrime (nextNumber)) {
			Benchy.Begin ("Checking if prime already found");
			if (primeNumbers.ContainsKey (nextNumber)) {
				primeNumbers [nextNumber] = true;
			} else {
				primeNumbers.Add (nextNumber, false);
			}
			Benchy.End ("Checking if prime already found");
		}
		int sumOfAllPrimes = primeNumbers.Sum (x => x.Key);
		sumOfAllPrimes += 1;
		CallSomeOtherMethod ();
		Benchy.End ();
	}
	
	void CallSomeOtherMethod ()
	{
		Benchy.Begin ();
		Benchy.Begin ("Checking something on method");
		// Some code could possibly have been here
		Benchy.End ("Checking something on method");
		Benchy.End ();
	}
	
	static bool IsPrime (int number)
	{
		int x = 2;
		x *= 1213321;
		
		if (number % 2 == 0) {
			if (number == 2) {
				return true;
			}
			return false;
		}
		int max = (int)Mathf.Sqrt (number);
		for (int i = 3; i <= max; i += 2) {
			if ((number % i) == 0) {
				return false;
			}
		}
		return true;
	}
	
	int counter = 0;

	[NeverProfileMethod]
	void Render ()
	{
	 	Benchy.Begin ();
		Benchy.Begin ("Find main camera");
		GameObject mainCamera = FindMainCamera ();
		Benchy.End ("Find main camera");
	
		if (mainCamera) {
			ParticleSystem particleSystem = mainCamera.GetComponent<ParticleSystem> ();
			if (particleSystem) {
				counter++;
				Benchy.Begin ("Main particle system code");
				if (counter >= 100) {
					if (!particleSystem.isPlaying)
						particleSystem.Play ();
					
					particleSystem.startColor = new Color (Random.Range (0, 255) / 255f, Random.Range (0, 255) / 255f, Random.Range (0, 255) / 255f, Random.Range (0, 255) / 255f);
					particleSystem.startRotation = Random.Range (0, 20);
					counter = 0;
				}
				Benchy.End ("Main particle system code");
			}
		}
		Benchy.End ();
	}
	
	GameObject FindMainCamera ()
	{
		return GameObject.Find ("Main Camera");
	}
}
