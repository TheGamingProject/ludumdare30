using System;
using TouchScript.Gestures;
using TouchScript.Hit;
using UnityEngine;
using Random = UnityEngine.Random;

public class TouchButton2 : MonoBehaviour {
	// https://github.com/InteractiveLab/TouchScript/wiki/Gestures

	void Start() {
		if (Application.platform != RuntimePlatform.Android || Application.platform != RuntimePlatform.Android) {
			this.enabled = false;
			GetComponent<SpriteRenderer>().enabled = false;
		}
	}

	private void OnEnable() {
		GetComponent<ReleaseGesture>().Released += tapHandler;
	}
	
	private void OnDisable() {
		GetComponent<ReleaseGesture>().Released -= tapHandler;
	}
	
	private void tapHandler(object sender, EventArgs e) {
		GameObject.Find("Player").GetComponent<PlayerControls>().trySpecialAttack();
	}
	
}