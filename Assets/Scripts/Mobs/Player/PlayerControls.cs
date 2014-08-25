using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {
	int facing = 1;
	
	public Vector2 speed = new Vector2(3.0f, 2.0f);
	public float backwardsSpeed = 2.0f;
	public Vector2 yBounds = new Vector2(-.45f, -3.45f); 
	
	public float amountFromCamera = 8.5f;
		
	HitboxForDad basicAttackHitbox;
	public float attackPushForce = 1000.0f;

	HitboxForDad saturnAttack;
	public Transform saturnAttackProjectile;
	public Vector2 saturnProjectionOffset = new Vector2(0, .3f);

	Animator animationController;
	
	//bool walking = false;

	public float idleSpeedThreshold = 1.0f;

	public float saturnCooldownTime = 5.0f;
	Cooldown saturnCooldown;

	enum states {
		idle, running, atk1, atk2
	}

	void Start () {
		basicAttackHitbox = GameObject.Find("basicAttackHitArea").GetComponent<HitboxForDad>();
		saturnAttack = GameObject.Find("saturnHitArea").GetComponent<HitboxForDad>();
		animationController = transform.FindChild("animator").GetComponent<Animator>();

		saturnCooldown = new Cooldown(saturnCooldownTime);
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

		saturnCooldown.updateCooldown();

		if (Input.GetButtonDown("Fire1") && !isAttacking()) {
			tryAttack1();
		}

		if (Input.GetButtonDown("Fire2") && !isAttacking()) {
			trySpecialAttack();
		}

	}

	// ATTACKING
	bool isAttacking() {
		return basicAttackHitbox.isActive() || saturnAttack.isActive();
	}

	void tryAttack1() {
		basicAttackHitbox.activate();
		int r = Random.Range(0, 2);
		if (r == 0) {
			animationController.Play("heroAtk1");
		} else {
			animationController.Play("heroAtk2");
		}

		float xSpeed = (Input.GetAxis("Horizontal") >= 0 ? attackPushForce : -attackPushForce);
		Vector2 position = new Vector2(xSpeed, 0);
		rigidbody2D.AddRelativeForce(position, ForceMode2D.Impulse);
	}

	void trySpecialAttack() {
		if (saturnCooldown.isCooldownUp()) {
			Debug.Log("saturn attack");
			animationController.Play("heroPower1");
			saturnCooldown.resetCooldown();
			saturnAttack.activate();

			Transform effect = Instantiate(saturnAttackProjectile) as Transform;
			effect.parent = transform.parent;
			Vector3 v = new Vector3(transform.position.x + saturnProjectionOffset.x, transform.position.y + saturnProjectionOffset.y, transform.position.z);
			effect.position = v;
		}
	}
	
	// MOVEMENT 

	void updateMovement() {
		float xMovement = Input.GetAxis("Horizontal");
		float yMovement = Input.GetAxis("Vertical");

		if (!isAttacking()) {
			float xSpeed = xMovement * (xMovement > 0 ? speed.x : backwardsSpeed);
			Vector2 position = new Vector2(xSpeed, yMovement * speed.y);
			rigidbody2D.AddRelativeForce(position);
		}
		Vector2 velocity = rigidbody2D.velocity;
//		Debug.Log(velocity);
		if (Mathf.Abs(velocity.x) <= idleSpeedThreshold && xMovement == 0) {
			//idle ?
			animationController.SetBool("running", false);
		} else if (Mathf.Abs(velocity.x) > idleSpeedThreshold) {
//			Debug.Log("running");
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

