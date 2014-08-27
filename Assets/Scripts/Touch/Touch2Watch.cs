using UnityEngine;
using System.Collections;

public class TouchManager : TouchLogic 
{
	public TouchLogic[] touches2Manage;
	
	void OnTouchEndedAnywhere()
	{
		foreach(TouchLogic obj in touches2Manage)
			if(obj.touch2Watch > TouchLogic.currTouch)
				obj.touch2Watch--;
	}
}