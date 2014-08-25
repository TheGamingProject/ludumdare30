using UnityEngine;
using System.Collections;

public class HitboxForDad : MonoBehaviour {
	public float activeTime = 1.0f;
	public int dmg = 1;
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
		if (activeCooldown <= 0) {
			activeHitbox = true;
			activeCooldown = activeTime;
		}
	}

	void OnTriggerStay2D(Collider2D collider) {
		onTrigger(collider);
	}

	void OnTriggerEnter2D(Collider2D collider) {
		onTrigger(collider);
	}

	void OnTriggerExit2D(Collider2D collider) {
		onTrigger(collider);
	}

	void onTrigger(Collider2D collider) {
		if (!activeHitbox) return;
		
		Enemy enemyHealth = collider.gameObject.GetComponent<Enemy>();
		if (enemyHealth != null && !amIAnEnemy()) {
			enemyHealth.getHit(transform.parent, dmg);
		}
		
		Player player = collider.gameObject.GetComponent<Player>();
		if (player  != null) {
			player.getHit(transform.parent, dmg);
		}

	}

	bool amIAnEnemy () {
		if (transform.parent.GetComponent<Enemy>() != null) {
			return true;
		}
		return false;
	}

	public bool isActive () {
		return activeHitbox;
	}

}
