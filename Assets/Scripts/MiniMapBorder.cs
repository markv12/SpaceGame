using UnityEngine;
using System.Collections;

public class MiniMapBorder : MonoBehaviour {
	
	public Texture2D textureImage;
	
	void OnGUI(){
		GUI.DrawTexture(new Rect(Screen.width*0.8f,0,Screen.width*0.2f,Screen.height*0.4f), textureImage);
	}
	
}