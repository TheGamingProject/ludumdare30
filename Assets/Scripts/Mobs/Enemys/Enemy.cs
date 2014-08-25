using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	private Transform player;

	public Vector2 speedRange = new Vector2(10,20);
	public float fallenSpeed = 1.0f;
	private float speed;

	public int health = 2;

	public float invulnTime = 1.0f;
	float invulnCooldown = 0.0f;
	
	HitboxForDad basicAttackHitbox;

	MeshRenderer myMeshRenderer;
	Material defaultMaterial;
	public Material getHitMaterial;

	int facing = -1;
	bool death = false;

	Animator animationController;

	public Vector2 attackCooldownTimeRange = new Vector2(1.0f, 1.2f);
	Cooldown attackCooldown;
	public float attackRange = 2.0f;

	void Start () {
		foreach (Transform child in transform.parent){
			if (child.name == "Player"){
				player = child;
			}
		}

		animationController = transform.FindChild("animator").GetComponent<Animator>();
		basicAttackHitbox = transform.FindChild("basicAttackBounds").GetComponent<HitboxForDad>();

		speed = Random.Range(speedRange.x, speedRange.y);

		attackCooldown = new Cooldown(attackCooldownTimeRange);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (death) return;

		updateFacing();

		updateCooldown();
		if  (isCooldownUp()) {
	//		myMeshRenderer.material = defaultMaterial;
			//resetCooldown();
		}

		updateFalling();

		bool isCloseEnough = isCloseEnoughToAttack();
		attackCooldown.updateCooldown();

		if (isCloseEnough) {
			animationController.SetBool("idleAfterAttack", true);

			if (attackCooldown.isCooldownUp()) {
				attack();
			}
		} else {
			animationController.SetBool("idleAfterAttack", false);
			moveCloserToTarget();
		}
	}

	private void moveCloserToTarget() {
		Vector3 direction = player.position - transform.position;
		float s = falling ? fallenSpeed : speed;
		rigidbody2D.AddForceAtPosition(direction.normalized * s, transform.position);
	}

	private void updateFacing() {
		int newFacing = 0;
		if (rigidbody2D.velocity.x > 0) {
			newFacing = 1;
		} else if (rigidbody2D.velocity.x < 0) {
			newFacing = -1;
		}

		setFacing(newFacing);
	}

	private void setFacing(int newDirection) {
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

	private bool falling = false;
	private void updateFalling () {
		Vector2 target = new Vector2(player.position.x, player.position.y);
		// if velocity is not towards player, dont reset animation
		if (falling && !isInvunUp() && findIfImMovingTowardsTarget(rigidbody2D.velocity, 
               new Vector2(transform.position.x, transform.position.y),
           		target)) {
			animationController.SetTrigger("knockbackEnds");
			falling = false;
		}
	}

	private void startFalling (Transform fromWhom, int dmg) {
		GetComponent<Knockbackable>().knockback(fromWhom.position, dmg);
		falling = true;
		animationController.SetTrigger("knockbackStarts");
	}

	public void getHit(Transform fromWhom, int dmg) {
		if (isInvunUp()) return;

		health -= dmg;
		if (health <= 0) {
			die();
		}
		
		startFalling(fromWhom, dmg);

		//Debug.Log("got hit" + health);

	//	myMeshRenderer.material = getHitMaterial;
		resetCooldown();
	}

	bool isCooldownUp () {
		return invulnCooldown <= 0.0f;
	}
	void updateCooldown () {
		invulnCooldown -= Time.deltaTime;
	}
	void resetCooldown () {
		invulnCooldown = invulnTime;
	}

	bool isInvunUp() { 
		return !isCooldownUp();
	}

	void die () {
		GameObject.Find("BodyCount").GetComponent<BodyCount>().addBody();
		animationController.SetBool("death", true);
		death = true;
		gameObject.AddComponent<KillYourself>();
		gameObject.GetComponent<KillYourself>().timeToLive = 30;
		//Destroy(gameObject);
	}

	void attack() {
		//Debug.Log("Attack!");
		basicAttackHitbox.activate();
		animationController.SetTrigger("startAttack");
		attackCooldown.resetCooldown();
	}

	bool isCloseEnoughToAttack () {
		return Vector2.Distance(new Vector2(transform.position.x, transform.position.y), 
		                        new Vector2(player.position.x, player.position.y)) < attackRange;
	}

	bool findIfImMovingTowardsTarget( Vector2 myVelocity, Vector2 myPosition, Vector2 myTargetPosition) {
	//	Debug.Log(myVelocity.x + " " +  myPosition.x + " " + myTargetPosition.x);
		if (
			((Mathf.Sign(myVelocity.x) == -1 && myPosition.x >=myTargetPosition.x) || (Mathf.Sign(myVelocity.x) == 1 && myPosition.x <= myTargetPosition.x)) //&&
			//((Mathf.Sign(myVelocity.y) == -1 && myPosition.y >= myTargetPosition.y) || (Mathf.Sign(myVelocity.y) == 1 && myPosition.y <= myTargetPosition.y))
			) {
			return true;
		}
		return false;
	}
}
