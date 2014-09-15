using System;
using TouchScript.Gestures;
using TouchScript.Hit;
using UnityEngine;
using Random = UnityEngine.Random;

public class TouchMovementControls : MonoBehaviour {
	// https://github.com/InteractiveLab/TouchScript/wiki/Gestures
	
	public float forceRatio = .15f;
	
	private PlayerControls player;
	
	void Start() {
		if (Application.platform != RuntimePlatform.Android || Application.platform != RuntimePlatform.Android) {
			this.enabled = false;
			GetComponent<SpriteRenderer>().enabled = false;
		}

		player = GameObject.Find("Player").GetComponent<PlayerControls>();
	}
	
	private void OnEnable() {
		GetComponent<PanGesture>().PanStarted += panStartedHandler;
		GetComponent<PanGesture>().Panned += pannedHandler;
		GetComponent<PanGesture>().PanCompleted += panStoppedHandler;
	}
	
	private void OnDisable() {
		GetComponent<PanGesture>().PanStarted -= panStartedHandler;
		GetComponent<PanGesture>().Panned -= pannedHandler;
		GetComponent<PanGesture>().PanCompleted -= panStoppedHandler;
	}
	
	Vector2 lastPos; 
	
	private void panStartedHandler(object sender, EventArgs e) {
		var gesture = sender as PanGesture;
		ITouchHit hit;
		gesture.GetTargetHitResult(out hit);
		var hit3d = hit as ITouchHit3D;
		if (hit3d == null) return;
		
//		var dir = hit3d.Point.x > 0 ? 1 : -1;
		updatePan(hit3d.Point - transform.parent.parent.position);
		//Debug.Log("started");
		//Debug.Log(hit3d.Point);
		//playerDonut.GetComponent<Controls>().press(dir);
		//GameObject.Find("Player").GetComponent<PlayerControls>().tryAttack1();
	}
	
	private void pannedHandler(object sender, EventArgs e) {
		var gesture = sender as PanGesture;
		ITouchHit hit;
		gesture.GetTargetHitResult(out hit);
		var hit3d = hit as ITouchHit3D;
		if (hit3d == null) return;

		updatePan(hit3d.Point - transform.parent.parent.position);
		//Debug.Log("panned");
		//Debug.Log(hit3d.Point - transform.parent.parent.position);
		//playerDonut.GetComponent<Controls>().press(dir);
		//GameObject.Find("Player").GetComponent<PlayerControls>().tryAttack1();
	}
	
	private void panStoppedHandler(object sender, EventArgs e) {
		var gesture = sender as PanGesture;
		ITouchHit hit;
		gesture.GetTargetHitResult(out hit);
		var hit3d = hit as ITouchHit3D;
		if (hit3d == null) return;

		updatePan(hit3d.Point - transform.parent.parent.position);
		//Debug.Log("stopped");
		//Debug.Log(hit3d.Point);
		//playerDonut.GetComponent<Controls>().press(dir);
		//GameObject.Find("Player").GetComponent<PlayerControls>().tryAttack1();
	}
	private void updatePan(Vector3 position) {
		Vector2 pos = new Vector2(position.x, position.y);
		
		if (pos != lastPos) {
			Vector2 direction = pos - lastPos;
			Debug.Log(lastPos);
			Debug.Log(pos);
			Debug.Log(direction);
			
			Vector2 force = new Vector2(direction.normalized.x * player.speed.x * forceRatio, direction.normalized.y * player.speed.y * forceRatio);
			Debug.Log(force);
			player.rigidbody2D.AddRelativeForce(force, ForceMode2D.Impulse);
			
			lastPos = pos;
		}
		
	}
}