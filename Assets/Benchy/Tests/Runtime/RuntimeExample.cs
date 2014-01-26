using UnityEngine;
using System.Collections;

public class RuntimeExample : MonoBehaviour {

	// Use this for initialization
	[NeverProfileMethod]
	void Start () {
	
	}
	
	// Update is called once per frame
	[NeverProfileMethod]
	void Update () {
	
	}
	
	bool RealtimeRender;
	[NeverProfileMethod]
	void OnGUI() {
		RealtimeRender = GUILayout.Toggle (RealtimeRender, "Benchy Runtime - Render Statistics in game");
		if (RealtimeRender) {
			RuntimeRender ();
		}
	}
	
	[NeverProfileMethod]
	void RuntimeRender()
	{
		float xOffset = 10;

		BenchyRuntime.DisplayCurrentFPS (xOffset, 20, 600, 200, "Current FPS:");
		BenchyRuntime.DisplayLowestFPS (xOffset, 40, 600, 200, "Lowest FPS:");
		BenchyRuntime.DisplayHighesttFPS (xOffset, 60, 600, 200, "Highest FPS:");
		BenchyRuntime.DisplayMemoryUsage (xOffset, 80, 600, 200, "Current Memory Usage:");
		BenchyRuntime.DisplayPeakMemoryUsage (xOffset, 100, 600, 200, "Peak Memory Usage:");
		BenchyRuntime.DisplayMostExpensiveMethodCall (xOffset, 120, 600, 200, "Most Expensive Call:");
		BenchyRuntime.DisplayMostExecutedMethodCall (xOffset, 140, 600, 200, "Most Executed Call:");
		
		// Widgets are still work in progress and will see development time in future
		BenchyRuntime.RenderWidget(BenchyRuntime.Location.UpperRight, BenchyRuntime.WidgetType.Overview);
		BenchyRuntime.RenderWidget(BenchyRuntime.Location.LowerLeft, BenchyRuntime.WidgetType.ExpensiveMethodsMS);
	}
}
