using UnityEngine;
using System.Collections;

public class BodyCount : MonoBehaviour
{
	private int total = 0;
	private GUIText text;

	void Start () {
		text = GetComponent<GUIText>();
	}

	void Update () {
		text.text = "" + total;
	}

	public void addBody() {
		total++;
	}
}

