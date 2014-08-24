using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public int health = 10;
	public float invulnTime = .5f;
	Cooldown invunlnCooldown;
	public Transform bubblePrefab;

	Animator animationController;

	// Use this for initialization
	void Start () {
		animationController = transform.FindChild("animator").GetComponent<Animator>();

		invunlnCooldown = new Cooldown(invulnTime);
	}
	
	// Update is called once per frame
	void Update () {
		invunlnCooldown.updateCooldown();
	}


	public void getHit(Transform fromWhom, int dmg) {
		if (invunlnCooldown.isCooldownUp()) {
			health -= dmg;
			Debug.Log("got hit" + health);
			if (health <= 0) {
				die();
			} else {
				autoInvun();
			}
		} else {
//			Debug.Log("IM INVINCIBLE"  + health);
		}

	}

	void autoInvun() {
		Transform t = Instantiate(bubblePrefab) as Transform;
		t.position = transform.position;
		t.parent = transform;
		invunlnCooldown.resetCooldown();
	}

	void die() {
		//Destroy(gameObject);
		Application.LoadLevel("menu");
	}
}
