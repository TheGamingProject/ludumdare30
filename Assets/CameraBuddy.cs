using UnityEngine;
using System.Collections;

public class CameraBuddy : MonoBehaviour
{
	public float speed = .5f;
	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{
		Vector3 nextPosition = new Vector3(transform.position.x +(speed * Time.deltaTime), transform.position.y, transform.position.z);
		transform.position = nextPosition;
	}
}

