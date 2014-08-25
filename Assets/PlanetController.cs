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
		Color c = ColorUtils.HSVToRGB(hValue / 360.0f, baseColorS / 255.0f, baseColorV / 255.0f);
		overlayRenderer.color = c; 
	}


}

