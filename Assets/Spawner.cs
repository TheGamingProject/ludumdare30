using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	public float xFromCamera = 10.0f;
	public Vector2 ySpawnRange = new Vector2(-4.5f, 1f);

	public Vector2 spawnTimeRange = new Vector2(1.0f, 3.0f);
	float spawnCooldown; 

	public Transform spawnee;

	void Start () {
		resetCooldown();
	}
	
	void Update () {
		updateCooldown();

		if (isCooldownUp()) {
			spawnSpawnee();
			resetCooldown();
		}
	}

	void spawnSpawnee () {
		Transform t = Instantiate(spawnee) as Transform;
		float xSpawn = Camera.main.transform.position.x + xFromCamera;
		float ySpawn = Random.Range(ySpawnRange.x, ySpawnRange.y);
		t.position = new Vector3(xSpawn, ySpawn, 0);
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
