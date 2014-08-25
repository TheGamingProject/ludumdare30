using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealthDisplay : MonoBehaviour {
	public Transform displaySymbolPrefab;
	public float startingX = -1.0f; 

	public int maxSymbols = 30;
	public int maxHealth = 50;

	private Player player;

	List<Transform> symbolTransforms = new List<Transform>();
	Vector3 size;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player").GetComponent<Player>();
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (Time.timeScale == 0) return;

		int totalSymbols = maxSymbols * (player.health) / maxHealth;

		if (totalSymbols != symbolTransforms.Count) {
			// adjust
			int symbolsNeeded = totalSymbols - symbolTransforms.Count;

			if (symbolsNeeded > 0) {
				add();
			} else if (symbolsNeeded < 0) {
				remove();
			}
		}

	}

	void add() {
		Transform t = Instantiate(displaySymbolPrefab) as Transform;
		t.parent = transform;

		if (size == Vector3.zero) {
			size = t.renderer.bounds.max - t.renderer.bounds.min;
		}

		float xPos = transform.position.x + size.x * symbolTransforms.Count;
		Vector3 position = new Vector3(xPos, transform.position.y, transform.position.z);
		t.position = position;

		symbolTransforms.Add(t);
		shift();
	}

	void remove() {
		Transform t = symbolTransforms[symbolTransforms.Count - 1];
		symbolTransforms.Remove(t);
		Destroy(t.gameObject);

		shift();
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
}

