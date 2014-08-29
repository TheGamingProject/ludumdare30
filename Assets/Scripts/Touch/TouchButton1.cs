using System;
using TouchScript.Gestures;
using TouchScript.Hit;
using UnityEngine;
using Random = UnityEngine.Random;

public class TouchButton1 : MonoBehaviour {
	// https://github.com/InteractiveLab/TouchScript/wiki/Gestures
	
	private void OnEnable() {
		GetComponent<ReleaseGesture>().Released += tapHandler;
	}
	
	private void OnDisable() {
		GetComponent<ReleaseGesture>().Released -= tapHandler;
	}
	
	private void tapHandler(object sender, EventArgs e) {
		/*var gesture = sender as ReleaseGesture;
		ITouchHit hit;
		gesture.GetTargetHitResult(out hit);
		var hit3d = hit as ITouchHit3D;
		if (hit3d == null) return;

		var dir = hit3d.Point.x > 0 ? 1 : -1;

		Debug.Log(hit3d.Point);*/
		//playerDonut.GetComponent<Controls>().press(dir);
		GameObject.Find("Player").GetComponent<PlayerControls>().tryAttack1();
	}
	
}