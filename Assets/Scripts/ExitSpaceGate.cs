using UnityEngine;
using System.Collections;

public class ExitSpaceGate : MonoBehaviour {

	void OnTriggerExit2D(Collider2D collider)
	{
		GameState.Instance.exitOpenSpace();
	}
}
