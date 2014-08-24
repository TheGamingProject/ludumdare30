using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {
	int facing = 1;
	
	public Vector2 speed = new Vector2(3.0f, 2.0f);
	public float backwardsSpeed = 2.0f;
	public Vector2 yBounds = new Vector2(-.45f, -3.45f); 
	
	public float amountFromCamera = 8.5f;
		
	HitboxForDad basicAttackHitbox;
	Animator animationController;
	
	bool walking = false;

	public float idleSpeedThreshold = 1.0f;

	enum states {
		idle, running, atk1, atk2
	}

	void Start () {
		basicAttackHitbox = GameObject.Find("basicAttackHitArea").GetComponent<HitboxForDad>();
		animationController = transform.FindChild("animator").GetComponent<Animator>();
	}

	void Update () {

		/*
		Vector3 nextPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		
		if (xMovement != 0) {
			if (xMovement > 0) {
				nextPosition.x += xMovement * backwardsSpeed * Time.deltaTime;
				
				if (facing == -1) {
					setFacing(1);
				}
			} else {
				nextPosition.x += xMovement * speed.x * Time.deltaTime;
				
				if (facing == 1) {
					setFacing(-1);
				}
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
		transform.position = nextPosition;
		 */

		updateMovement();

		if (Input.GetButtonDown("Fire1")) {
			tryAttack1();
		}

	}

	// ATTACKING

	void tryAttack1() {
		basicAttackHitbox.activate();
		int r = Random.Range(0, 2);
		if (r == 0) {
			animationController.Play("heroAtk1");
		} else {
			animationController.Play("heroAtk2");
		}

	}

	
	// MOVEMENT 

	void updateMovement() {
		float xMovement = Input.GetAxis("Horizontal");
		float yMovement = Input.GetAxis("Vertical");

		Vector2 position = new Vector2(xMovement * speed.x, yMovement * speed.y);
		rigidbody2D.AddRelativeForce(position);

		Vector2 velocity = rigidbody2D.velocity;
		Debug.Log(velocity);
		if (Mathf.Abs(velocity.x) <= idleSpeedThreshold && xMovement == 0) {
			//idle ?
			animationController.SetBool("running", false);
		} else if (Mathf.Abs(velocity.x) > idleSpeedThreshold) {
			Debug.Log("running");
			animationController.SetBool("running", true);
		}

		//set facing 
		setFacing((int)Mathf.Sign(xMovement));
	}

	void setFacing(int newDirection) {
		if (facing == newDirection) return;
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

