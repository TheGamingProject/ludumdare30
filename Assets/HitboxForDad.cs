using UnityEngine;
using System.Collections;

public class HitboxForDad : MonoBehaviour {
	public float activeTime = 1.0f;
	bool active = false;

	float activeCooldown;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (active) {
			activeCooldown -= Time.deltaTime;

			if (activeCooldown <= 0) {
				active = false;
			}
		}

	}

	public void activate () {
		active = true;
		activeCooldown = activeTime;
	}

	void OnTriggerStay2D(Collider2D collider) {
		Debug.Log("hello");
		if (!active) return;

		Debug.Log("active bro");

		Enemy enemyHealth = collider.gameObject.GetComponent<Enemy>();
		if (enemyHealth != null) {
						
			Destroy(enemyHealth.gameObject);
		}
	}


}
