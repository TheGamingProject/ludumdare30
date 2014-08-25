using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CooldownDisplay : MonoBehaviour {
	public Transform marsSymbolPrefab;
	public Transform saturnSymbolPrefab;
	public Transform jupiterSymbolPrefab;
	public Transform neptuneSymbolPrefab;
	public Transform mercurySymbolPrefab;

	public float startingX = -1.0f; 
	
	public int maxSymbols = 30;
	
	private PlayerControls player;
	
	List<Transform> symbolTransforms = new List<Transform>();
	Vector3 size;

	public Vector2 opacityRange = new Vector2(.5f, 1.0f);

	PlanetController planetMaster;
	
	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player").GetComponent<PlayerControls>();
		planetMaster = Camera.main.GetComponent<PlanetController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Time.timeScale == 0) return;
		
		int totalSymbols = Mathf.FloorToInt(maxSymbols * (player.getCurrentCooldownPercent())); 

		if (totalSymbols <= .1f) {
			removeAll();
			shift();
			Debug.Log("killed all");
			Debug.Log(symbolTransforms.Count);
		}

		if (totalSymbols != symbolTransforms.Count) {
			// adjust
			int symbolsNeeded = totalSymbols - symbolTransforms.Count;
			
			if (symbolsNeeded > 0) {
				do {
					add();
				} while (symbolTransforms.Count < totalSymbols);
				shift();
			} else if (symbolsNeeded < 0) {
				do {
					remove();
				} while (symbolTransforms.Count > totalSymbols);
				shift();
			}
		} 

		if (totalSymbols == maxSymbols) {
			setAllOpacity(opacityRange.y);
		} else {
			setAllOpacity(opacityRange.x);
		}
		
	}

	Transform getPlanetSymbolPrefab () {
		PlanetsLol.planets currentPlanet = planetMaster.getCurrentPlanet();
		
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
	
	void add() {
		Transform t = Instantiate(getPlanetSymbolPrefab()) as Transform;
		t.parent = transform;
		
		if (size == Vector3.zero) {
			size = t.renderer.bounds.max - t.renderer.bounds.min;
		}
		
		float xPos = transform.position.x + size.x * symbolTransforms.Count;
		Vector3 position = new Vector3(xPos, transform.position.y, transform.position.z);
		t.position = position;
		
		symbolTransforms.Add(t);
	}
	
	void remove() {
		Transform t = symbolTransforms[symbolTransforms.Count - 1];
		symbolTransforms.Remove(t);
		Destroy(t.gameObject);
	}

	void removeAll() {
		foreach (Transform t in transform) {
			symbolTransforms.Remove(t);
			Destroy(t.gameObject);
		}
	}
	void shift() {
		float totalX = size.x * symbolTransforms.Count;
		Vector3 position = new Vector3(transform.parent.position.x - totalX/2, transform.position.y, transform.position.z);
		transform.position = position;
	}
	
	public void setHide(bool b) {
		foreach (Transform t in transform) {
			t.renderer.enabled = b;
		}
	}

	void setAllOpacity(float a) {
		foreach (Transform t in transform) {
			SpriteRenderer sr = ((SpriteRenderer)t.renderer);
			sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, a);
		}
	}
}

