using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlanetsLol : MonoBehaviour {
	public Sprite marsSkybox;
	public Sprite mercurySkybox;
	public Sprite jupiterSkybox;
	public Sprite neptuneSkybox;
	public Sprite saturnSkybox;

	public Sprite marsSymbol;
	public Sprite mercurySymbol;
	public Sprite jupiterSymbol;
	public Sprite neptuneSymbol;
	public Sprite saturnSymbol;

	public Vector2 skyboxOffset = new Vector2();
	public Vector2 symbolOffset = new Vector2(6.0f, 2.0f);
	public float skyboxGap = 35.00f;
	public float symbolGap = 17.88f;

	private Transform symbolParent;

	planets currentPlanet, lastPlanet;
	int currentPlanetNumber = 0;

	public float planetsSkyboxXSpeed = -1.0f;

	List<Transform> skyboxes = new List<Transform>();
	List<Transform> symbols = new List<Transform>();

	public enum planets {
		mars, mercury, jupiter, neptune, saturn,
		none
	}
	
	List<planets> planetOrder = new List<planets>();

	void Start () {
		symbolParent = GameObject.Find("2 - Middleground").transform.FindChild("symbols");

		currentPlanet = planets.none;
		//addSkybox(currentPlanet, currentPlanetNumber);
		//addSymbol(currentPlanet, currentPlanetNumber);

		addNextPlanetStuff();
		addNextPlanetStuff();
		addNextPlanetStuff();
		addNextPlanetStuff();
		addNextPlanetStuff();
		addNextPlanetStuff();
		addNextPlanetStuff();
		addNextPlanetStuff();
		addNextPlanetStuff();
		addNextPlanetStuff();

	}

	void Update () {
		Vector3 v3 = transform.position;
		v3.x += planetsSkyboxXSpeed * Time.deltaTime;
		transform.position = v3;

		updateCameraViewPlanet();
	}

	void addNextPlanetStuff () {
		setNextPlanet();
		addSkybox(currentPlanet, currentPlanetNumber);
		addSymbol(currentPlanet, currentPlanetNumber);
	}

	planets setNextPlanet () {
		lastPlanet = currentPlanet;
		planets pick;
		do {
			pick = getRandomPlanet();
		} while (pick == lastPlanet);

		planetOrder.Add(pick);
		currentPlanet = pick;
		currentPlanetNumber++;
		return pick;
	}

	planets getRandomPlanet () {
		switch (Random.Range(0,4)) {
		case 0:
			return planets.jupiter;
		case 1:
			return planets.mars;
		case 2:
			return planets.mercury;
		case 3:
			return planets.neptune;
		case 4:
			return planets.saturn;
		}
		return planets.saturn;
	}

	void addSkybox (planets p, int number) {
		GameObject go = new GameObject();
		go.AddComponent<SpriteRenderer>();

		Sprite newSprite = null;
		switch (p) {
		case planets.mars:
			newSprite = marsSkybox;
			break;
		case planets.mercury:
			newSprite = mercurySkybox;
			break;
		case planets.jupiter:
			newSprite = jupiterSkybox;
			break;
		case planets.neptune:
			newSprite = neptuneSkybox;
			break;
		case planets.saturn:
			newSprite = saturnSkybox;
			break;
		}

		go.GetComponent<SpriteRenderer>().sprite = newSprite;
		go.transform.parent = transform;

		Vector3 localScale = new Vector3(1.5f, 1.5f, 1.0f);
		go.transform.localScale = localScale;

		//skyboxSize = (go.renderer.bounds.max - go.renderer.bounds.min);
		Vector3 position = new Vector3(skyboxOffset.x - (number - 1) * skyboxGap, skyboxOffset.y, transform.position.z);
		go.transform.position = position;

		skyboxes.Add(go.transform);
	}

	void addSymbol (planets p, int number) {
		GameObject go = new GameObject();
		go.AddComponent<SpriteRenderer>();
		
		Sprite newSprite = null;
		switch (p) {
		case planets.mars:
			newSprite = marsSymbol;
			break;
		case planets.mercury:
			newSprite = mercurySymbol;
			break;
		case planets.jupiter:
			newSprite = jupiterSymbol;
			break;
		case planets.neptune:
			newSprite = neptuneSymbol;
			break;
		case planets.saturn:
			newSprite = saturnSymbol;
			break;
		}
		
		go.GetComponent<SpriteRenderer>().sprite = newSprite;
		go.transform.parent = symbolParent;
		
		Vector3 localScale = new Vector3(1.7f, 1.7f, 1.0f);
		go.transform.localScale = localScale;

		Vector3 position = new Vector3(symbolOffset.x + (number - 1) * symbolGap, symbolOffset.y, symbolParent.position.z);
		go.transform.position = position;

		symbols.Add(go.transform);
	}

	private int currentCameraPlanet = 0;
	void updateCameraViewPlanet () {
		Vector3 cameraPos = Camera.main.transform.position,
		currentSymbol = symbols[currentCameraPlanet].position,
		nextSymbol = symbols[currentCameraPlanet + 1].position;

		if (Vector3.Distance(currentSymbol, cameraPos) >  Vector3.Distance(nextSymbol, cameraPos)) {
			currentCameraPlanet++;
			addNextPlanetStuff();
			Debug.Log("switched to " + getCurrentCameraViewPlanet());
		}
	}

	public planets getCurrentCameraViewPlanet() {
		return planetOrder[currentCameraPlanet];
	}
}

