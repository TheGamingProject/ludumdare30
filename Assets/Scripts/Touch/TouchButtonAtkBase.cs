using UnityEngine;
using System.Collections;

public class TouchButtonAtkBase : TouchLogic  {
	
	public override void OnTouchBegan() {
		GetComponent<PlayerControls>().tryAttack1();
	}
}


