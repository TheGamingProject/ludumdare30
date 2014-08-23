using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public Vector2 speed = new Vector2(3.0f, 2.0f);
	public Vector2 yBounds = new Vector2(-.45f, -3.45f); 

	public float amountFromCamera = 8.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		float xMovement = Input.GetAxis("Horizontal");
		float yMovement = Input.GetAxis("Vertical");
		
		Vector3 nextPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z);

		if (xMovement != 0) {
			nextPosition.x += xMovement * speed.x * Time.deltaTime;
		}

		if (nextPosition.x < Camera.main.transform.position.x - amountFromCamera) {
			nextPosition.x = Camera.main.transform.position.x - amountFromCamera;
		} else if (nextPosition.x > Camera.main.transform.position.x + amountFromCamera) {
			nextPosition.x = Camera.main.transform.position.x + amountFromCamera;
		}


		if (yMovement != 0) {
			nextPosition.y += yMovement * speed.y * Time.deltaTime;

			// snap position to y bounds
			if (nextPosition.y < yBounds.y) {
				nextPosition.y = yBounds.y;
			}
			if (nextPosition.y > yBounds.x) {
				nextPosition.y = yBounds.x;
			}
		}

		transform.position = nextPosition;
	}
}
