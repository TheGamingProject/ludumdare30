using UnityEngine;
using System.Collections;

public static class ColorUtils 
{
	public static Color HSVToRGB(float h,float s, float v) {
		float r = 0 ;
		float g = 0 ;
		float b = 0;
		float i;
		float f ;
		float p;
		float q ;
		float t ; 
		i = Mathf.Floor(h * 6);
		f = h * 6 - i;
		p = v * (1 - s);
		q = v * (1 - f * s);
		t = v * (1 - (1 - f) * s);
		
		switch ((int)i % 6) {
		case 0: r = v; g = t; b = p; break;
		case 1: r = q; g = v; b = p; break;
		case 2: r = p; g = v; b = t; break;
		case 3: r = p; g = q; b = v; break;
		case 4: r = t; g = p; b = v; break;
		case 5: r = v; g = p; b = q; break;
		}
		
		return new Color(r,g,b); 
	}
}

