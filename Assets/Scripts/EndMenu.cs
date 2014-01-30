using UnityEngine;
using System.Collections;

public class EndMenu : MonoBehaviour {

	void Update(){
		if(Input.GetKeyDown ("x")){
			CameraFade.StartAlphaFade( Color.black, false, 1f, 0f, () => { GameState.Instance.LoadLevel(0); });
		}
	}
}
