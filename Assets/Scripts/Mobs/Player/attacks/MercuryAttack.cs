using UnityEngine;
using System.Collections;

public class MercuryAttack : MonoBehaviour {
	public float speed;
	public int facing = 1;

	public int dmg = 3;
	public float radiusGrowth = 1.0f;

	public CircleCollider2D myCollider;

	// Use this for initialization
	void Start () {
		myCollider = GetComponent<CircleCollider2D>();
	}

	// Update is called once per frame
	void Update ()
	{
		//move left
		rigidbody2D.velocity = new Vector2(facing * speed, 0);

		//more growth
		float r = myCollider.radius + radiusGrowth * Time.deltaTime;
		myCollider.radius = r;
	}

	void OnTriggerEnter2D (Collider2D collider) {
		Enemy enemy = collider.GetComponent<Enemy>();
		if (enemy != null) {
			enemy.getHit(transform, dmg);
		}
	}

	public void setFacing(int newDirection) {
		facing = newDirection;
		
		Quaternion v = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
		if (facing == 1) {
			v.y = 0;
		} else if (facing == -1) {
			v.y = 180;
		}
		transform.rotation = v;
	}
}

