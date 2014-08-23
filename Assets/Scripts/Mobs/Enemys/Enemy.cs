using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	private Transform player;

	public float speed = 1.2f;
	public float moveTowardsPlayerRange = 3.0f;

	public int health = 2;

	public float invulnTime = 1.0f;
	float invulnCooldown = 0.0f;

	MeshRenderer myMeshRenderer;
	Material defaultMaterial;
	public Material getHitMaterial;

	void Start () {
		foreach (Transform child in transform.parent){
			if (child.name == "Player"){
				player = child;
			}
		}

		myMeshRenderer = transform.GetComponent<MeshRenderer>();
		defaultMaterial = myMeshRenderer.material;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 nextPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

		if (player.transform.position.x < transform.position.x) {
			nextPosition.x -= speed * Time.deltaTime;
		} else if (player.transform.position.x > transform.position.x) {
			nextPosition.x += speed * Time.deltaTime;
		}

		if (Vector3.Distance(transform.position, player.position) < moveTowardsPlayerRange) {
			if (player.transform.position.y < transform.position.y) {
				nextPosition.y -= speed * Time.deltaTime;
			} else if (player.transform.position.y > transform.position.y) {
				nextPosition.y += speed * Time.deltaTime;
			}
		}

		transform.position = nextPosition;

		updateCooldown();
		if  (isCooldownUp()) {
			myMeshRenderer.material = defaultMaterial;
			//resetCooldown();
		}
	}

	public void getHit(Transform fromWhom, int dmg) {
		if (!isCooldownUp()) return;

		health -= dmg;
		if (health <= 0) {
			Destroy(gameObject);
		} else {
			GetComponent<Knockbackable>().knockback(fromWhom.position, dmg);
		}

		//Debug.Log("got hit" + health);

		myMeshRenderer.material = getHitMaterial;
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
