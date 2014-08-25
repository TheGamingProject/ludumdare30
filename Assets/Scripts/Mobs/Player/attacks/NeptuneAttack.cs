using UnityEngine;
using System.Collections;

public class NeptuneAttack : MonoBehaviour {
	public int dmg = 2;
	public float force = 1000;

	void Start () {

	}

	void Update () {

	}

	void OnTriggerEnter2D (Collider2D collider) {
		Enemy enemy = collider.GetComponent<Enemy>();
		if (enemy != null) {
			enemy.getHit(transform, dmg);
			pullEnemy(enemy.transform);
		}
	}

	void pullEnemy (Transform enemy) {
		Vector3 direction = transform.position - enemy.position;
		enemy.rigidbody2D.AddForceAtPosition(direction.normalized * force, transform.position);
	}
}
