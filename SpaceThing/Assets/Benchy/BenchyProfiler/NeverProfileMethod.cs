using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.AttributeUsage(System.AttributeTargets.Method)]
	public class NeverProfileMethod : System.Attribute
{
	public NeverProfileMethod ()
	{
	}
}