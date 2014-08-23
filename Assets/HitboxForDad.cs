using UnityEngine;
using System.Collections;

public class HitboxForDad : MonoBehaviour {
	public float activeTime = 1.0f;
	bool activeHitbox = false;

	float activeCooldown;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (activeHitbox) {
			activeCooldown -= Time.deltaTime;

			if (activeCooldown <= 0) {
				activeHitbox = false;
			}
		}

	}

	public void activate () {
		activeHitbox = true;
		activeCooldown = activeTime;
	}

	void OnTriggerStay2D(Collider2D collider) {
		if (!activeHitbox) return;

		Enemy enemyHealth = collider.gameObject.GetComponent<Enemy>();
		if (enemyHealth != null) {
						
			Destroy(enemyHealth.gameObject);
		}
	}


}
