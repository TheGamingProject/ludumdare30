using UnityEngine;
using System.Collections;

public class InGameMenu : MonoBehaviour {
	
	// Use this for initialization
	public GUISkin menu;

	public string button1Text;

	public float button1Y;
	public float button1W;
	public float button1H;

	public float timescaleSpeedup = .05f;
	bool started = false, canContinue = false, ended = false;

	SpriteRenderer startOverlay, endOverlay;

	[Header("EndGame menu")]
	public float endButton1X;
	public float endButton1Y;
	public float endButton1W;
	public float endButton1H;
	
	public float endButton2X;
	public float endButton2Y;
	public float endButton2W;
	public float endButton2H;
	
	public float endButton3X;
	public float endButton3Y;
	public float endButton3W;
	public float endButton3H;
	
	public float endButton4X;
	public float endButton4Y;
	public float endButton4W;
	public float endButton4H;

	public int killCount;

	public float bodyCountOffset = -1.0f;

	void Start() {
		transform.FindChild("BodyCount").GetComponent<GUIText>().enabled = false;
		transform.FindChild("Labels").FindChild("Bodycount").GetComponent<GUIText>().enabled = false;
		Camera.main.transform.FindChild("healthDisplay").GetComponent<HealthDisplay>().setHide(false);

		Time.timeScale = 0.0f;
		startOverlay = transform.FindChild("startScreen").GetComponent<SpriteRenderer>();
		endOverlay = Camera.main.transform.FindChild("endScreen").GetComponent<SpriteRenderer>();
	}

	void Update() {
		if (ended && canContinue) {
			Time.timeScale += timescaleSpeedup;
			if (Time.timeScale >= 0) {
				Time.timeScale = 0;
				canContinue = false; // dumb
			}
			updateEndFade();
		}

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
		if (started && !ended) return;
		
		GUI.skin = menu;

		if (ended) {
			if ( 
			    GUI.Button(
				new Rect(
				endButton1X * Screen.width,
				endButton1Y * Screen.height,
				endButton1W * Screen.width,
				endButton1H * Screen.height
				),
				" "
				) 
			    ) 
			{
				openLink("http://www.facebook.com/sharer/sharer.php?u=http%3A%2F%2Fwww.tgp.io%2Fplay%2Fludumdare30&t=I+struck+down+"+killCount+"+foes+with+my+Planetary+Kung+Fu.+Think+you+can+best+me?");
			}
			
			if ( 
			    GUI.Button(
				new Rect(
				endButton2X * Screen.width,
				endButton2Y * Screen.height,
				endButton2W * Screen.width,
				endButton2H * Screen.height
				),
				" "
				) 
			    ) 
			{
				string text = "I struck down "+killCount+" foes with my Planetary Kung Fu. Think you can best me";
				openLink("https://twitter.com/intent/tweet?text=" + text + "?&hashtags=ShowMeYourMoves,PlanetKungFu&url=http%3A%2F%2Fwww.tgp.io%2Fplay%2Fludumdare30");
			}
			
			if ( 
			    GUI.Button(
				new Rect(
				endButton3X * Screen.width,
				endButton3Y * Screen.height,
				endButton3W * Screen.width,
				endButton3H * Screen.height
				),
				" "
				) 
			    ) 
			{
				Application.LoadLevel("scene1");
			}
			
			if ( 
			    GUI.Button(
				new Rect(
				endButton4X * Screen.width,
				endButton4Y * Screen.height,
				endButton4W * Screen.width,
				endButton4H * Screen.height
				),
				" "
				) 
			    ) 
			{
				Application.LoadLevel("mainmenu");
			}
			return;
		}

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

	void openLink(string url) {
		/*if (Application.isWebPlayer) {
			Application.ExternalEval("window.open('" + url + "','_blank')");
		} else {*/
			Application.OpenURL(url);
		//}
	}

	void updateFade () {
		float alpha = startOverlay.color.a - timescaleSpeedup;
		Color newColor = new Color(startOverlay.color.r, startOverlay.color.b, startOverlay.color.g, alpha);
		startOverlay.color = newColor;
	}

	void updateEndFade () {
		float alpha = endOverlay.color.a + timescaleSpeedup;
		Color newColor = new Color(endOverlay.color.r, endOverlay.color.b, endOverlay.color.g, alpha);
		endOverlay.color = newColor;
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

	public void endGame () {
		//Time.timeScale = 0.0f;
		endOverlay.enabled = true;
		ended = true;

		Transform bc = transform.FindChild("BodyCount");
		killCount = bc.GetComponent<BodyCount>().getCount();
		bc.position = new Vector3(bc.position.x, bc.position.y + bodyCountOffset, bc.position.z);
		
		transform.FindChild("Labels").FindChild("Bodycount").GetComponent<GUIText>().enabled = false;
		Camera.main.transform.FindChild("healthDisplay").GetComponent<HealthDisplay>().setHide(true);
		Camera.main.transform.FindChild("cooldownDisplay").GetComponent<CooldownDisplay>().setHide(true);
	}
}
