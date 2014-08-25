using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {
	
	// Use this for initialization
	public GUISkin menu;
	
	public Texture backgroundTexture;
	
	public string button1Text;
	
	public float button1X;
	public float button1Y;
	public float button1W;
	public float button1H;
	
	void OnGUI(){
		
		GUI.skin = menu;
		
		GUI.DrawTexture (new Rect (((Screen.width/2)-((Screen.height*16)/9)/2), 0, (Screen.height*16)/9, Screen.height), backgroundTexture);
		
		if ( 
		    GUI.Button(
			new Rect(
			button1X,
			button1Y,
			button1W,
			button1H
			),
			button1Text
			) 
		    ) 
		{
			Application.LoadLevel("scene1");
		}
	}
}
