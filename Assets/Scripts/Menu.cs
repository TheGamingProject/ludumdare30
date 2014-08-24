using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void OnGUI () {
		if ( 
		    GUI.Button(
			new Rect(
			0,
			(1 * Screen.height / 3) - 100,
			Screen.width,
			200
			),
			"press to play"
			) 
		    ) 
		{
			Application.LoadLevel("scene1");
		}
	}
}