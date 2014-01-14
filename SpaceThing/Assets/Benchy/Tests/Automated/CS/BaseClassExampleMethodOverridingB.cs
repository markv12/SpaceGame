using UnityEngine;
using System.Collections;

public class BaseClassExampleMethodOverridingB : BaseClassExampleMethodOverridingA {
	protected override void Update () {
		base.Update();
	}
}
