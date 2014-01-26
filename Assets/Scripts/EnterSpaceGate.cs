using UnityEngine;
using System.Collections;

public class EnterSpaceGate : MonoBehaviour {

	void OnTriggerExit2D(Collider2D collider)
	{
		GameState.Instance.enterOpenSpace();
	}
}

