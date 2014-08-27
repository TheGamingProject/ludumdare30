using UnityEngine;
using System.Collections;

public class TouchButtonAtkSpecial : TouchLogic 
{
	
	public override void OnTouchBegan()
	{
		if (!GetComponent<PlayerControls>().isAttacking()) {
			GetComponent<PlayerControls>().trySpecialAttack();
		};
		GetComponent<PlayerControls>().updatePower1ing();
	}
}

