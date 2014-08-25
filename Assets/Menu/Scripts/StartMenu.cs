using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {
	
	// Use this for initialization
	public GUISkin menu;

	public string button1Text;

	public float button1Y;
	public float button1W;
	public float button1H;

	public float timescaleSpeedup = .05f;
	bool started = false, canContinue = false;

	SpriteRenderer startOverlay;

	void Start() {
		transform.FindChild("BodyCount").GetComponent<GUIText>().enabled = false;
		transform.FindChild("Labels").FindChild("Bodycount").GetComponent<GUIText>().enabled = false;
		Camera.main.transform.FindChild("healthDisplay").GetComponent<HealthDisplay>().setHide(false);

		Time.timeScale = 0.0f;
		startOverlay = transform.FindChild("startScreen").GetComponent<SpriteRenderer>();
	}

	void Update() {
		if (started) return;

		if (canContinue) {
			Time.timeScale += timescaleSpeedup;
			updateFade();
		}
		if (canContinue && Time.timeScale >= 1.0f) {
			startGame();
		}

		if ( (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2"))) {
			startStarting();
		}
	}

	void OnGUI() {
		if (started) return;
		
		GUI.skin = menu;

		if ( 
		    GUI.Button(
			new Rect(
			Screen.width/2  - button1W/2,
			button1Y * Screen.height - button1H/2,
			button1W,
			button1H
			),
			button1Text
			) 
		    ) 
		{
			startStarting();
		}
	}

	void updateFade () {
		float alpha = startOverlay.color.a - timescaleSpeedup;
		Color newColor = new Color(startOverlay.color.r, startOverlay.color.b, startOverlay.color.g, alpha);
		startOverlay.color = newColor;
	}

	void startStarting () {
		if (canContinue) return;
		canContinue = true;
	}

	void startGame () {
		Time.timeScale = 1.0f;
		startOverlay.enabled = false;
		started = true;

		transform.FindChild("BodyCount").GetComponent<GUIText>().enabled = true;
		transform.FindChild("Labels").FindChild("Bodycount").GetComponent<GUIText>().enabled = true;
		Camera.main.transform.FindChild("healthDisplay").GetComponent<HealthDisplay>().setHide(true);
	}
}
