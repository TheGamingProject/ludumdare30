using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	private Transform player;

	public float speed = 1.2f;

	void Start () {
		foreach (Transform child in transform.parent){
			if (child.name == "Player"){
				player = child;
			}
		}

		Debug.Log(player);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 nextPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

		if (player.transform.position.x < transform.position.x) {
			nextPosition.x -= speed * Time.deltaTime;
		} else if (player.transform.position.x > transform.position.x) {
			nextPosition.x += speed * Time.deltaTime;
		}

		if (player.transform.position.y < transform.position.y) {
			nextPosition.y -= speed * Time.deltaTime;
		} else if (player.transform.position.y > transform.position.y) {
			nextPosition.y += speed * Time.deltaTime;
		}

		transform.position = nextPosition;
	}
}
