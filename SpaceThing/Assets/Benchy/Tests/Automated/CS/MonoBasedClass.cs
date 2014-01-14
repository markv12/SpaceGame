using UnityEngine;
using System.Collections;

public class MonoBasedClass : MonoBehaviour {
	
	public TestMethod link;
	private NotMonoBased noMono;

	void Start() {
		TestAMethodFromUpdate();	
		noMono = new NotMonoBased();
	}
	
	// Update is called once per frame
	void Update () {
		TestAMethodFromUpdate();
		if (link != null) link.PublicMethod();
		noMono.AMethod();
	}
	
	void TestAMethodFromUpdate() {
		int x = 1;
		x = x*222;
	}
	
}

