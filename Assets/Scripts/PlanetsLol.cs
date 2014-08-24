using UnityEngine;
using System.Collections;

public class PlanetsLol : MonoBehaviour {
	public Sprite marsSkybox;
	public Sprite mercurySkybox;
	public Sprite jupiterSkybox;
	public Sprite neptuneSkybox;
	public Sprite saturnSkybox;

	public Vector2 planetOffset = new Vector2();

	planets currentPlanet;
	int currentPlanetNumber = 1;

	public float planetsSkyboxXSpeed = -1.0f;

	enum planets {
		mars, mercury, jupiter, neptune, saturn
	}

	void Start () {
		currentPlanet = getRandomPlanet();
		addSkybox(currentPlanet, currentPlanetNumber);
		currentPlanet = getRandomPlanet();
		addSkybox(currentPlanet, ++currentPlanetNumber);
	}

	void Update () {
		Vector3 v3 = transform.position;
		v3.x += planetsSkyboxXSpeed * Time.deltaTime;
		transform.position = v3;
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

		Vector3 size = (go.renderer.bounds.max - go.renderer.bounds.min);
		Vector3 position = new Vector3(planetOffset.x + (number-1) * size.x, planetOffset.y, transform.position.z);
		go.transform.position = position;
	}
}

