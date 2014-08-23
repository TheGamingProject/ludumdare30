using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	private Transform player;

	public float speed = 1.2f;

	public int health = 2;

	public float invulnTime = 1.0f;
	float invulnCooldown = 0.0f;

	MeshRenderer myMeshRenderer;
	Material defaultMaterial;
	public Material getHitMaterial;

	int facing = -1;

	void Start () {
		foreach (Transform child in transform.parent){
			if (child.name == "Player"){
				player = child;
			}
		}

		//myMeshRenderer = transform.GetComponent<MeshRenderer>();
		//defaultMaterial = myMeshRenderer.material;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direction = player.position - transform.position;
		rigidbody2D.AddForceAtPosition(direction.normalized, transform.position);

		updateFacing();

		updateCooldown();
		if  (isCooldownUp()) {
	//		myMeshRenderer.material = defaultMaterial;
			//resetCooldown();
		}
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

	public void getHit(Transform fromWhom, int dmg) {
		if (!isCooldownUp()) return;

		health -= dmg;
		if (health <= 0) {
			Destroy(gameObject);
		}
		
		GetComponent<Knockbackable>().knockback(fromWhom.position, dmg);

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
}
