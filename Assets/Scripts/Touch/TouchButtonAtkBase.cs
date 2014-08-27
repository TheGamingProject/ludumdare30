using UnityEngine;
using System.Collections;

public class TouchButtonAtkBase : TouchLogic 
{
	
	public override void OnTouchBegan()
	{
		if (!GetComponent<PlayerControls>().isAttacking()) {
			GetComponent<PlayerControls>().tryAttack1();
		};
		GetComponent<PlayerControls>().updatePower1ing();
	}
}


