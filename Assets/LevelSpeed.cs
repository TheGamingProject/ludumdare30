using UnityEngine;
using System.Collections;

public class LevelSpeed : MonoBehaviour {
	public float baseSpeed = 1.5f;
	private float speedModifier = 1.0f;
	
	public float incrementalSpeedRate = .015f; //per second
	private float incrementedSpeed = 0.0f;
	
	private bool gameOver = false;
	
	public float Speed {
		get {
			return baseSpeed * speedModifier + incrementedSpeed;
		}
	}
	
	void Update () {
		if (!gameOver) {
			incrementedSpeed += Time.deltaTime * incrementalSpeedRate;
			var metersTraveled = Time.deltaTime * Speed * .15f;
			//GameObject.Find("GUI").GetComponentInChildren<Meters>().addMeters(metersTraveled);
			//GameObject.Find("Debug").GetComponent<GUIText>().text = "Debug: " + Speed;
		}
	}
	
	public void resetSpeedModifier () {
		if (gameOver) return; 
		
		speedModifier = 1.0f;
	}
	
	public void setSpeedModifier (float f) {
		if (gameOver) return; 
		
		speedModifier = f;
	}
	
	public void setGameOver () {
		gameOver = true;
		speedModifier = 0.0f;
		incrementedSpeed = 0.0f;
	}
	
	public float NonPlayerSpeedDifference {
		get {
			return Speed - (baseSpeed + incrementedSpeed);
		}
	}
	
	public float NonPlayerSpeedRatio {
		get {
			return Speed / (baseSpeed + incrementedSpeed);
		}
	}
}