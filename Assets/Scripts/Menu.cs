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
			120,
			400,
			60
			),
			"C L I C K  T O  S T A R T"
			) 
		    ) 
		{
			Application.LoadLevel("scene1");
		}
	}
}