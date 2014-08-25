using UnityEngine;
using System.Collections;

public class MarsAttack : MonoBehaviour {
	public int dmg = 2;
	public float force = 1500;
	public float radiusGrowth = 0.6f;
	
	CircleCollider2D myCollider;

	void Start () {
		myCollider = GetComponent<CircleCollider2D>();
	}
	
	void Update () {//more growth
		float r = myCollider.radius + radiusGrowth * Time.deltaTime;
		myCollider.radius = r;
	}
	
	void OnTriggerEnter2D (Collider2D collider) {
		Enemy enemy = collider.GetComponent<Enemy>();
		if (enemy != null) {
			enemy.getHit(transform, dmg);
			explodeEnemy(enemy.transform);
		}
	}
	
	void explodeEnemy (Transform enemy) {
		Vector3 direction = enemy.position - transform.position;
		enemy.rigidbody2D.AddForceAtPosition(direction.normalized * force, transform.position);
	}
}
