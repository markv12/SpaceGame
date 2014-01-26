using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace BenchyInject
{
	public class Profiler
	{
		public static void Enter (string methodName, object caller)
		{
			try
			{
				string className = new StackFrame (1, false).GetMethod ().DeclaringType.Name;
				if (caller == null)
					Benchy.BeginInjected (methodName, className);
				else
					Benchy.BeginInjected (methodName, System.String.Format("{0}",caller.GetType()));
			}
			catch {}
		}
		
		public static void Exit (string methodName, object caller)
		{
			try
			{
				string className = new StackFrame (1, false).GetMethod ().DeclaringType.Name;
				if (caller == null)
					Benchy.EndInjected (methodName, className);
				else
					Benchy.EndInjected (methodName, System.String.Format("{0}",caller.GetType()));
			}
			catch {}
		}
	
	}
}