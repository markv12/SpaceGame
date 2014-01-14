using UnityEngine;
using System.Collections;

public class BaseClassExample : MonoBehaviour {

	protected virtual void Update () {
		// Complete garbage example but good enough to slow the method down a little for example sake
		float y = Mathf.Sqrt(46);
		y += y * Mathf.Sqrt(68);
		y = y + y.GetHashCode();
	}
}
