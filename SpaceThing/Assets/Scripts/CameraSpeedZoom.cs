using UnityEngine;
using System.Collections;

public class CameraSpeedZoom : MonoBehaviour {

	private Rigidbody2D player;

	public float fovNarrow = 10f;
	public float fovWide = 30f;
	public float fovResizeSmooth = 0.35f;
	public float zoomFactor = 3f;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").rigidbody2D;
	}
	
	// Update is called once per frame
	void Update () {
		if(player != null){
			zoomWithPlayer();
		}
	}

	private void zoomWithPlayer(){
		float currentFOV = -transform.position.z;
		float propsedView;
		float newView = 0;
		if(player.velocity.magnitude != 0){   	  
			propsedView = Mathf.Abs(player.velocity.magnitude*zoomFactor);
		}
		else{ 	
			propsedView = fovNarrow;
		}
		if(propsedView < fovNarrow){
			propsedView=fovNarrow;
		}
		else if(propsedView > fovWide){
			propsedView=fovWide;
		}	
		if(Mathf.Abs(propsedView-currentFOV)<fovResizeSmooth){
			newView=propsedView;
		}
		else{
			if(propsedView>currentFOV){
				newView= currentFOV+fovResizeSmooth; 
			}
			else if(propsedView<currentFOV){
				newView=currentFOV-fovResizeSmooth; 
			}
		}
		transform.position = new Vector3 (transform.position.x,transform.position.y, -newView);
	}

}
