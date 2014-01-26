using UnityEngine;
using System.Collections;

public class TestMethod : MonoBehaviour {
	
	public bool callOtherMethod;
	 
	void Awake(){
		
	}
	
	void Start () {
		PublicMethod();
		if (callOtherMethod) OtherMethod();
	}
	
	void Update() {
		AnotherMethod(2);
		BlankMethodsAreNeverProfiled();
	}

	void BlankMethodsAreNeverProfiled(){
		// These are not profiled
	}
	
	public void PublicMethod(){
		GetInstanceID();
		AnotherMethod(2);
	}
		
	public void AnotherMethod(int x){
	}
	
	public void OtherMethod(){
		
	}
	
	private const KeyCode rightKeyCode        = KeyCode.D;
	
	void FixedUpdate(){
		Ray ray = Camera.mainCamera.ScreenPointToRay(Vector3.zero);
		Physics.Raycast(ray);
	}
}
