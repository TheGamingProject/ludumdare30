using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// 1. strike/activate
// 2. wait and find people to hit
// 3. startChainLightning()
//    4. zaps


public class Lightning : MonoBehaviour {
	public float findEnemysTime = .25f;
	private Cooldown findCooldown;
	
	private List<Enemy> enemys;
	
	private bool isActive = false;
	
	public int dmg = 2;
	public float damageDisapateRatio = .5f;

	public int bouncesLeft = 3;

	public Transform linkToSelf;

	Transform middleGround;
	public void init () {
		middleGround = GameObject.Find("2 - Middleground").transform;
		findCooldown = new Cooldown(findEnemysTime);
		resetFind();
	}
	
	void Update () {
		findCooldown.updateCooldown();
		if (isActive && findCooldown.isCooldownUp()) {
			startChainLightning();
			isActive = false;
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		onTrigger(collider);
	}
	
	void OnTriggerExit2D(Collider2D collider) {
		onTrigger(collider);
	}
	
	void onTrigger(Collider2D collider) {
		Enemy enemy = collider.GetComponent<Enemy>();
		if (isActive && enemy != null && !enemy.isDead() && !enemys.Contains(enemy)) {
			enemys.Add(enemy);
		}
	}
	public void activate() {
		resetFind();
		isActive = true;
	}

	public void strike(Enemy enemy) {
		Debug.Log("i got struck (" + enemy.health + ") for " + dmg);
		if (dmg - 1 <= 0) {
			activate();
		}

		//enemy.getHit(transform, dmg);
		enemy.shock(dmg);
	}

	void startChainLightning() {
		// zap closest 3 wait .25s, each zaps 2 others, wait 25s, first zap disappears, wach zaps 1 other
		int total = 0;

		Debug.Log("total enemys i could wat: " + enemys.Count);
		for(int i=0;i<enemys.Count;i++) {
			if(total >= bouncesLeft) break; 
			if (enemys[i].isDead() || enemys[i].recentlyShocked) continue;
			
			Debug.Log("zapped- #" + total);
			zap(enemys[i]);
			total++;
		}
		
	}
	
	void zap(Enemy e) {
		//degrade lightning
		int nextDmg = Mathf.FloorToInt(dmg * damageDisapateRatio);
		int nextBouncesLeft = bouncesLeft - 1;

		Debug.Log("about to try to zap");
		Debug.Log(" DMG: " + (nextDmg));
		Debug.Log(" Bounces left: " + bouncesLeft);
		if (bouncesLeft <= 0) return;

		Transform lightning = Instantiate(linkToSelf) as Transform;
		lightning.name = "lightning " + bouncesLeft;
		Debug.Log(lightning.name + " is alive");
		Lightning lightningScript = lightning.GetComponent<Lightning>();
		lightningScript.bouncesLeft = nextBouncesLeft;
		lightningScript.dmg = nextDmg;
		lightning.parent = middleGround;
		lightning.position = new Vector3(e.transform.position.x, e.transform.position.y, lightning.parent.position.z);
		LineRenderer line = lightning.transform.FindChild("line").GetComponent<LineRenderer>();
		line.enabled = true;
		line.SetPosition(0, transform.position);
		line.SetPosition(1, e.transform.position);
		lightningScript.init();

		GetComponent<HashAudioScript>().PlayAudio("jupiter");
		lightningScript.strike(e);
		Debug.Log("STRUCK BROOO");
	}

	void resetFind() {
		enemys = new List<Enemy>();
		findCooldown.resetCooldown();
	}
}

