using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	public GUISkin menu;

	//public Texture backgroundTexture;

	public string button1Text;

	public float button1Y;
	public float button1W;
	public float button1H;

	public float lockTime = 2.0f;
	Cooldown continueCooldown;
	private bool canContinue = false;

	public int baseColorS, baseColorV;
	SpriteRenderer overlayRenderer;

	void Start() {
		continueCooldown = new Cooldown(lockTime);
		overlayRenderer = transform.FindChild("overlay").GetComponent<SpriteRenderer>();
	}

	private float counter = 0;
	void Update () {
		if (counter < 360) {
			setColor(counter++);
		}

		continueCooldown.updateCooldown();
		if (continueCooldown.isCooldownUp()) {
			canContinue = true;
		}

		if (canContinue && (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Fire2"))) {
			continuePlz();
		}
	}

	void OnGUI(){

		GUI.skin = menu;

		//GUI.DrawTexture (new Rect (((Screen.width/2)-((Screen.height*16)/9)/2), 0, (Screen.height*16)/9, Screen.height), backgroundTexture);

		if (canContinue) {
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
				continuePlz();
			}
		}
	}

	void continuePlz () {
		Application.LoadLevel("startmenu");
	}

	void setColor (float hValue) {
		Color c = ColorUtils.HSVToRGB(hValue / 360.0f, baseColorS / 255.0f, baseColorV / 255.0f);
		overlayRenderer.color = c; 
	}
}
