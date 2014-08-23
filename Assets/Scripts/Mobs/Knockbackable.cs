using UnityEngine;
using System.Collections;

public class Knockbackable : MonoBehaviour {
	public float speedRatio = 1.0f;
	public float forceRatio = 100;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void knockback(Vector2 position, float amount) {
		Vector3 myPos = transform.position;
		float xDiff = myPos.x - position.x;
		float yDiff = myPos.y - position.y;
		Vector2 dir = new Vector2(xDiff, yDiff);
		//float ratio = yDiff / xDiff;

		rigidbody2D.AddForce(dir.normalized * forceRatio);

		//Vector2 newVelocity = new Vector2(amount * forceRatio * 1.0f / ratio, amount * forceRatio * ratio);
		//rigidbody2D.velocity = newVelocity;
	}

	// need x and y velocities
}

