using UnityEngine;
using System.Collections;

public class KillYourself : MonoBehaviour {

	public float timeToLive = 3.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timeToLive -= Time.deltaTime;

		if (timeToLive <= 0) {
			Destroy(gameObject);
		}
	}
}
