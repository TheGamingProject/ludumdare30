using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public Vector2 speed = new Vector2(3.0f, 2.0f);
	public float backwardsSpeed = 2.0f;
	public Vector2 yBounds = new Vector2(-.45f, -3.45f); 

	public float amountFromCamera = 8.5f;

	public Vector2 explosionRelativePosition = new Vector2(1.5f, .5f);
	public Transform explosionSpawnee;

	HitboxForDad basicAttackHitbox;
	Animator animationController;

	public bool walking = false;

	// Use this for initialization
	void Start () {
		basicAttackHitbox = GameObject.Find("basicAttackHitArea").GetComponent<HitboxForDad>();
		animationController = transform.FindChild("animator").GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		float xMovement = Input.GetAxis("Horizontal");
		float yMovement = Input.GetAxis("Vertical");
		
		Vector3 nextPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z);

		if (xMovement != 0) {
			if (xMovement > 0) {
				nextPosition.x += xMovement * backwardsSpeed * Time.deltaTime;
			} else {
				nextPosition.x += xMovement * speed.x * Time.deltaTime;
			}

			if (!walking) {
				animationController.SetTrigger("startRunning");
			}
			walking = true;
		} else {
			if (walking) {
				animationController.SetTrigger("stopRunning");
			}
			walking = false;
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

		if (Input.GetButtonDown("Fire1")) {
			Transform t = Instantiate(explosionSpawnee) as Transform;
			Vector3 pos = new Vector3(transform.position.x + explosionRelativePosition.x, 
			                          transform.position.y + explosionRelativePosition.y, 
			                          transform.position.z);
			t.transform.position = pos;
			//t.parent = transform;
			basicAttackHitbox.activate();
		}

		transform.position = nextPosition;
	}
}
