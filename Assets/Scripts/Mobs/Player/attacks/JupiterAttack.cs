using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JupiterAttack : MonoBehaviour {

	public int dmg = 2;
	public int enemysFirstHit = 4; 
	public Transform lightningPrefab;


	public void startZap() {
		Transform lightning = Instantiate(lightningPrefab) as Transform;
		lightning.parent = GameObject.Find("2 - Middleground").transform;
		lightning.position = new Vector3(transform.position.x, transform.position.y, lightning.parent.position.z);
		lightning.GetComponent<Lightning>().bouncesLeft = enemysFirstHit;
		lightning.GetComponent<Lightning>().dmg = dmg;
		lightning.GetComponent<Lightning>().init();
		lightning.GetComponent<Lightning>().activate();
	}

}

