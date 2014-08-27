using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lightning : MonoBehaviour {
	public float findEnemysTime = .25f;
	private Cooldown findCooldown;
	
	private List<Enemy> enemys;
	
	private bool isActive = false;
	
	public int dmg = 2;
	public float damageDisapateRatio = .5f;

	public int bouncesLeft = 3;

	public Transform linkToSelf;

	public void init () {
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
	
	void OnTriggerStay2D(Collider2D collider) {
		onTrigger(collider);
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		onTrigger(collider);
	}
	
	void OnTriggerExit2D(Collider2D collider) {
		onTrigger(collider);
	}
	
	void onTrigger(Collider2D collider) {
		Enemy enemy = collider.GetComponent<Enemy>();
		if (isActive && enemy != null && !enemys.Contains(enemy)) {
			enemys.Add(enemy);
		}
	}
	public void activate() {
		resetFind();
		isActive = true;
	}

	public void strike(Enemy enemy) {
		if (dmg - 1 <= 0) {
			activate();
		}

		enemy.getHit(transform, dmg);
	}

	void startChainLightning() {
		// zap closest 3 wait .25s, each zaps 2 others, wait 25s, first zap disappears, wach zaps 1 other
		int total = 0;

		Debug.Log(enemys.Count);
		for(int i=0;i<enemys.Count;i++) {
			if(total >= bouncesLeft) break; 
			if (enemys[i].isDead() || enemys[i].recentlyShocked) continue;
			
			Debug.Log("zapped-" + bouncesLeft);
			zap(enemys[i]);
			total++;
		}
		
	}
	
	void zap(Enemy e) {
		if (bouncesLeft-1 <= 0 || dmg * damageDisapateRatio <= 0) return;

		Transform lightning = Instantiate(linkToSelf) as Transform;
		Lightning lightningScript = lightning.GetComponent<Lightning>();
		lightningScript.bouncesLeft = bouncesLeft - 1;
		lightningScript.dmg = Mathf.CeilToInt(dmg * damageDisapateRatio);
		lightning.parent = GameObject.Find("2 - Middleground").transform;
		lightning.position = new Vector3(e.transform.position.x, e.transform.position.y, lightning.parent.position.z);
		LineRenderer line = lightning.transform.FindChild("line").GetComponent<LineRenderer>();
		line.enabled = true;
		line.SetPosition(0, transform.position);
		line.SetPosition(1, e.transform.position);
		lightningScript.init();

		GetComponent<HashAudioScript>().PlayAudio("jupiter");
		e.shock();
		lightningScript.strike(e);
	}

	void resetFind() {
		enemys = new List<Enemy>();
		findCooldown.resetCooldown();
	}
}

