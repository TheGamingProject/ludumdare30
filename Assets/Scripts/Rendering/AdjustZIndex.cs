using UnityEngine;
using System.Collections;

public class AdjustZIndex : MonoBehaviour {
	public float ratio = .01f;
	
	void Update () {
		float localZIndex = ratio * transform.localPosition.y;
		Vector3 newPos = new Vector3(transform.localPosition.x, transform.localPosition.y, localZIndex); 
		transform.localPosition = newPos;
	}
}