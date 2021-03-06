﻿using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	public float xFromCamera = 10.0f;
	public Vector2 ySpawnRange = new Vector2(-4.5f, 1f);

	public Vector2 spawnTimeRange = new Vector2(1.0f, 3.0f);
	float spawnCooldown; 

	public Vector2 spawnAmountRange = new Vector2(1.0f,4.0f);

	public Transform spawnee;

	public float extraPerSecondsRatio = 1.0f/10.0f; 

	void Start () {
		resetCooldown();
	}
	
	void Update () {
		updateCooldown();

		if (isCooldownUp()) {
			spawn();
			resetCooldown();
		}
	}

	void spawn () {
		int spawnAmount = Random.Range((int)spawnAmountRange.x, (int)spawnAmountRange.y);
		int extraSpawnsForTimeLength = Mathf.FloorToInt(Time.timeSinceLevelLoad * extraPerSecondsRatio);
		spawnAmount += extraSpawnsForTimeLength;
		for (int i=0; i < spawnAmount; i++) {
			spawnSpawnee();
		}
	}

	void spawnSpawnee () {
		Transform t = Instantiate(spawnee) as Transform;
		float xSpawn = Camera.main.transform.position.x + xFromCamera;
		float ySpawn = Random.Range(ySpawnRange.x, ySpawnRange.y);
		t.position = new Vector3(xSpawn, ySpawn, transform.position.z);
		t.parent = transform;
		//t.localScale = transform.localScale;
			
	}

	bool isCooldownUp () {
		return spawnCooldown <= 0.0f;
	}
	void updateCooldown () {
		spawnCooldown -= Time.deltaTime;
	}
	void resetCooldown () {
		spawnCooldown = Random.Range(spawnTimeRange.x, spawnTimeRange.y);
	}
}
