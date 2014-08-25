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

	public float symbolFlashTime = .5f;
	public float symbolFlashOpacity = .5f;
	public Transform marsSymbolPrefab;
	public Transform saturnSymbolPrefab;
	public Transform jupiterSymbolPrefab;
	public Transform neptuneSymbolPrefab;
	public Transform mercurySymbolPrefab;

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

		//flash symbol
		Transform quickSymbol = Instantiate(getPlanetSymbolPrefab()) as Transform;
		Vector3 pos = new Vector3(transform.position.x, transform.position.y, 10);
		quickSymbol.parent = transform;
		quickSymbol.position = pos;
		quickSymbol.gameObject.AddComponent<KillYourself>();
		quickSymbol.GetComponent<KillYourself>().timeToLive = symbolFlashTime;
		quickSymbol.GetComponent<SpriteRenderer>().color = new Color(1,1,1,symbolFlashOpacity);

		//play noise
		GetComponent<HashAudioScript>().PlayAudio("switch");

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

	public PlanetsLol.planets getCurrentPlanet() {
		return currentPlanet;
	}
	
	Transform getPlanetSymbolPrefab () {
		switch (currentPlanet) {
		case PlanetsLol.planets.saturn:
			return saturnSymbolPrefab;
		case PlanetsLol.planets.mercury:
			return mercurySymbolPrefab;
		case PlanetsLol.planets.neptune:
			return neptuneSymbolPrefab;
		case PlanetsLol.planets.jupiter:
			return jupiterSymbolPrefab;
		case PlanetsLol.planets.mars:
			return marsSymbolPrefab;
		}
		
		return null;
	}
}

