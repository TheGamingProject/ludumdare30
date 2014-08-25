using UnityEngine;
using System.Collections;

public class PlanetController : MonoBehaviour {
	PlanetsLol.planets currentPlanet = PlanetsLol.planets.none;
	PlanetsLol planetMaster;

	public int baseColorS, baseColorV;
	public int marsHValue, mercuryHValue, jupiterHValue, neptuneHValue, saturnHValue;
	private int prevHValue;

	SpriteRenderer overlayRenderer;


	public float transitionTime = 3.0f;
	float timeLeft;
	bool transitioning = false;
	int hValueDifference;

	void Start () {
		planetMaster = GameObject.Find("0 - Skybox Background").GetComponent<PlanetsLol>();
		overlayRenderer = transform.FindChild("overlay").GetComponent<SpriteRenderer>();
	}

	void Update () {
		PlanetsLol.planets truePlanet = planetMaster.getCurrentCameraViewPlanet();
		if (currentPlanet != truePlanet) {
			startTransition(truePlanet);
		}

		updateTransition();
	}

	void updateTransition() {
		if (!transitioning) return;

		timeLeft -= Time.deltaTime;

		if (timeLeft <= 0) {
			transitioning = false;
			return;
		}
		float hValue = prevHValue + (hValueDifference) * (transitionTime - timeLeft) / transitionTime;
		setColor(hValue);
}

	void startTransition(PlanetsLol.planets planet) {
		Debug.Log("changing to " + planet); 

		if (PlanetsLol.planets.none == currentPlanet) { //if planet
			currentPlanet = planet;
			setColor(getPlanetHValue(currentPlanet));
			return;
		}

		prevHValue = getPlanetHValue(currentPlanet);
		currentPlanet = planet;
		int hValue = getPlanetHValue(currentPlanet);
		hValueDifference = hValue - prevHValue;

		transitioning = true;
		timeLeft = transitionTime;
	}

	int getPlanetHValue(PlanetsLol.planets p) {
		int hValue = 0;
		switch (p) {
		case PlanetsLol.planets.jupiter:
			hValue = jupiterHValue;
			break;
		case PlanetsLol.planets.mars:
			hValue = marsHValue;
			break;
		case PlanetsLol.planets.mercury:
			hValue = mercuryHValue;
			break;
		case PlanetsLol.planets.neptune:
			hValue = neptuneHValue;
			break;
		case PlanetsLol.planets.saturn:
			hValue = saturnHValue;
			break;
		}
		return hValue;
	}

	void setColor (float hValue) {
		Color c = HSVToRGB(hValue / 360.0f, baseColorS / 255.0f, baseColorV / 255.0f);
		overlayRenderer.color = c; 
	}

	Color HSVToRGB(float h,float s, float v) {
		float r = 0 ;
		float g = 0 ;
		float b = 0;
		float i;
		float f ;
		float p;
		float q ;
		float t ; 
		i = Mathf.Floor(h * 6);
		f = h * 6 - i;
		p = v * (1 - s);
		q = v * (1 - f * s);
		t = v * (1 - (1 - f) * s);

		switch ((int)i % 6) {
		case 0: r = v; g = t; b = p; break;
		case 1: r = q; g = v; b = p; break;
		case 2: r = p; g = v; b = t; break;
		case 3: r = p; g = q; b = v; break;
		case 4: r = t; g = p; b = v; break;
		case 5: r = v; g = p; b = q; break;
		}

		return new Color(r,g,b); 
	}

}

