using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour {
	int facing = 1;
	
	public Vector2 speed = new Vector2(3.0f, 2.0f);
	public float backwardsSpeed = 2.0f;
	public Vector2 xBounds = new Vector2(-8f, -8f);
	public Vector2 yBounds = new Vector2(-.45f, -3.45f); 
	
	public float amountFromCamera = 8.5f;
		
	HitboxForDad basicAttackHitbox;
	public float attackPushForce = 1000.0f;

	bool power1ing = false;
	Cooldown power1ingCooldown;
	public float power1ingInvunTime = 1.0f;

	HitboxForDad saturnAttack;
	public Transform saturnAttackProjectile;
	public Vector2 saturnProjectionOffset = new Vector2(0, .3f);

	Animator animationController;
	
	//bool walking = false;

	public float idleSpeedThreshold = 1.0f;

	public float saturnCooldownTime = 5.0f;
	Cooldown saturnCooldown;


	public float mercuryCooldownTime = 1.0f;
	Cooldown mercuryCooldown;
	public Transform mercuryAttackProjectile;
	public Vector2 mercuryProjectionOffset = new Vector2(1f, 0f);
	
	public float neptuneCooldownTime = 1.0f;
	Cooldown neptuneCooldown;
	public Transform neptuneAttackProjectile;
	public Vector2 neptuneProjectionOffset = new Vector2(1f, 0f);

	// Jupiter
	JupiterAttack jupiterAttack;
	public float jupiterCooldownTime = 2;
	Cooldown jupiterCooldown;


	enum states {
		idle, running, atk1, atk2
	}

	void Start () {
		basicAttackHitbox = GameObject.Find("basicAttackHitArea").GetComponent<HitboxForDad>();
		saturnAttack = GameObject.Find("saturnHitArea").GetComponent<HitboxForDad>();
		animationController = transform.FindChild("animator").GetComponent<Animator>();
		jupiterAttack = transform.FindChild("jupiterAttack").GetComponent<JupiterAttack>();

		power1ingCooldown = new Cooldown(power1ingInvunTime);

		saturnCooldown = new Cooldown(saturnCooldownTime);
		saturnCooldown.setUp();
		mercuryCooldown = new Cooldown(mercuryCooldownTime);
		mercuryCooldown.setUp();
		neptuneCooldown = new Cooldown(neptuneCooldownTime);
		neptuneCooldown.setUp();
		jupiterCooldown = new Cooldown(jupiterCooldownTime);
		jupiterCooldown.setUp();
	}

	void Update () {
		updateMovement();

		saturnCooldown.updateCooldown();
		mercuryCooldown.updateCooldown();
		neptuneCooldown.updateCooldown();
		jupiterCooldown.updateCooldown();

		if (Input.GetButtonDown("Fire1") && !isAttacking()) {
			tryAttack1();
		}

		if (Input.GetButtonDown("Fire2") && !isAttacking()) {
			trySpecialAttack();
		}

		updatePower1ing();
	}

	// ATTACKING
	bool isAttacking() {
		return basicAttackHitbox.isActive() || saturnAttack.isActive() || power1ing;
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
		/*
		if (saturnCooldown.isCooldownUp()) {
			Debug.Log("saturn attack");
			saturnCooldown.resetCooldown();
			saturnAttack.activate();
			startPower1ing();

			Transform effect = Instantiate(saturnAttackProjectile) as Transform;
			effect.parent = transform.parent;
			Vector3 v = new Vector3(transform.position.x + saturnProjectionOffset.x, transform.position.y + saturnProjectionOffset.y, transform.position.z);
			effect.position = v;
		}

		if (mercuryCooldown.isCooldownUp()) {
			Debug.Log("mercury attack");
			animationController.Play("heroPower2");
			saturnCooldown.resetCooldown();
			saturnAttack.activate();
			
			Transform effect = Instantiate(mercuryAttackProjectile) as Transform;
			effect.parent = transform.parent;
			Vector3 v = new Vector3(transform.position.x + facing * mercuryProjectionOffset.x, transform.position.y + mercuryProjectionOffset.y, transform.position.z);
			effect.position = v;
			effect.GetComponent<MercuryAttack>().setFacing(facing);
		}

		if (neptuneCooldown.isCooldownUp()) {
			Debug.Log("neptune attack");
			startPower1ing();
			neptuneCooldown.resetCooldown();
			
			Transform effect = Instantiate(neptuneAttackProjectile) as Transform;
			effect.parent = transform.parent;
			Vector3 v = new Vector3(transform.position.x + facing * neptuneProjectionOffset.x, transform.position.y + neptuneProjectionOffset.y, transform.position.z);
			effect.position = v;
			rigidbody2D.velocity = Vector2.zero;
		}
*/
		if (jupiterCooldown.isCooldownUp()) {
			Debug.Log("jupiter attack");
			doJupiterAttack();
			jupiterCooldown.resetCooldown();
		}
	}

	void doJupiterAttack() {
		jupiterAttack.startZap();
		animationController.Play("heroPower2");
	}

	void startPower1ing() {
		power1ing = true;
		power1ingCooldown.resetCooldown();
		animationController.SetBool("power1ing", true);
	}

	void updatePower1ing() {
		power1ingCooldown.updateCooldown();
		if (power1ing && power1ingCooldown.isCooldownUp()) {
			power1ing = false;
			animationController.SetBool("power1ing", false);
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

		snapToBounds();
	}

	void snapToBounds () {
		Vector3 position = transform.position;
		// snap position to bounds
		if (position.x < Camera.main.transform.position.x + xBounds.x) {
			position.x = Camera.main.transform.position.x + xBounds.x;
		}
		if (position.x > Camera.main.transform.position.x + xBounds.y) {
			position.x = Camera.main.transform.position.x + xBounds.y;
		}

		if (position.y < yBounds.y) {
			position.y = yBounds.y;
		}
		if (position.y > yBounds.x) {
			position.y = yBounds.x;
		}
		transform.position = position;
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

