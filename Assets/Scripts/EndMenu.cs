using UnityEngine;
using System.Collections;

public class EndMenu : MonoBehaviour {

	void Update(){
		if(Input.GetKeyDown ("x")){
			GameState.Instance.LoadLevel(0);
		}
	}
}
