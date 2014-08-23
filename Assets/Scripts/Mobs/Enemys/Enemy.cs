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

	MeshRenderer myMeshRenderer;
	Material defaultMaterial;
	public Material getHitMaterial;

	int facing = -1;

	Animator animationController;

	void Start () {
		foreach (Transform child in transform.parent){
			if (child.name == "Player"){
				player = child;
			}
		}

		animationController = transform.FindChild("animator").GetComponent<Animator>();

		speed = Random.Range(speedRange.x, speedRange.y);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 direction = player.position - transform.position;
		float s = falling ? fallenSpeed : speed;
		rigidbody2D.AddForceAtPosition(direction.normalized * s, transform.position);

		updateFacing();

		updateCooldown();
		if  (isCooldownUp()) {
	//		myMeshRenderer.material = defaultMaterial;
			//resetCooldown();
		}

		updateFalling();
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
		if (falling) {
			Debug.Log(rigidbody2D.velocity.x + " " + rigidbody2D.velocity.y);
			//Debug.Log(rv.ToString());
		}
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
			Destroy(gameObject);
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

	bool findIfImMovingTowardsTarget( Vector2 myVelocity, Vector2 myPosition, Vector2 myTargetPosition) {
		Debug.Log(myVelocity.x + " " +  myPosition.x + " " + myTargetPosition.x);
		if (
			((Mathf.Sign(myVelocity.x) == -1 && myPosition.x >=myTargetPosition.x) || (Mathf.Sign(myVelocity.x) == 1 && myPosition.x <= myTargetPosition.x)) //&&
			//((Mathf.Sign(myVelocity.y) == -1 && myPosition.y >= myTargetPosition.y) || (Mathf.Sign(myVelocity.y) == 1 && myPosition.y <= myTargetPosition.y))
			) {
			return true;
		}
		return false;
	}
}
