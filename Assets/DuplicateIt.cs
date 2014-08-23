using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DuplicateIt : MonoBehaviour
{
	public int duplicatesAmount = 100;

	private List<Transform> backgroundPart;

	void Start() {

		// Get all the children of the layer with a renderer
		backgroundPart = new List<Transform>();
		
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);
			
			// Add only the visible children
			if (child.renderer != null)
			{
				backgroundPart.Add(child);
			}
		}

		Vector2 individualBgSize = backgroundPart[0].renderer.bounds.max - backgroundPart[0].renderer.bounds.min;
		float totalXLength = backgroundPart.Count * individualBgSize.x;

		// Sort by position.
		// Note: Get the children from left to right.
		// We would need to add a few conditions to handle
		// all the possible scrolling directions.
	/*	backgroundPart = backgroundPart.OrderBy(
			t => -1 * t.position.x
			).ToList();*/

		for (int i = 0; i <= duplicatesAmount; i++) {
			for (int bg = 0; bg < backgroundPart.Count; bg++) {
				Transform bgPart = backgroundPart[bg];
				Vector3 pos = new Vector3(i* totalXLength + bg * individualBgSize.x, bgPart.position.y, bgPart.position.z);
				Transform newBg = Instantiate(bgPart, Vector3.zero, Quaternion.identity) as Transform;
				newBg.parent = transform;
				newBg.position = pos;
				newBg.localScale = bgPart.localScale;
			}
		}

	}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}
