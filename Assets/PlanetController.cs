using UnityEngine;
using System.Collections;

public class PlanetController : MonoBehaviour {
	PlanetsLol.planets currentPlanet = PlanetsLol.planets.none;
	PlanetsLol planetMaster;

	public int baseColorS, baseColorV;
	public int marsHValue, mercuryHValue, jupiterHValue, neptuneHValue, saturnHValue;
	private int prevHValue;

	SpriteRenderer overlayRenderer;


	public float transitionTime = 3.0f;
	float timeLeft;
	bool transitioning = false;
	int hValueDifference;

	void Start () {
		planetMaster = GameObject.Find("0 - Skybox Background").GetComponent<PlanetsLol>();
		overlayRenderer = transform.FindChild("overlay").GetComponent<SpriteRenderer>();
	}

	void Update () {
		PlanetsLol.planets truePlanet = planetMaster.getCurrentCameraViewPlanet();
		if (currentPlanet != truePlanet) {
			startTransition(truePlanet);
		}

		updateTransition();
	}

	void updateTransition() {
		if (!transitioning) return;

		timeLeft -= Time.deltaTime;

		if (timeLeft <= 0) {
			transitioning = false;
			return;
		}
		float hValue = prevHValue + (hValueDifference) * (transitionTime - timeLeft) / transitionTime;
		setColor(hValue);
}

	void startTransition(PlanetsLol.planets planet) {
		Debug.Log("changing to " + planet); 

		if (PlanetsLol.planets.none == currentPlanet) { //if planet
			currentPlanet = planet;
			setColor(getPlanetHValue(currentPlanet));
			return;
		}

		prevHValue = getPlanetHValue(currentPlanet);
		currentPlanet = planet;
		int hValue = getPlanetHValue(currentPlanet);
		hValueDifference = hValue - prevHValue;

		transitioning = true;
		timeLeft = transitionTime;
	}

	int getPlanetHValue(PlanetsLol.planets p) {
		int hValue = 0;
		switch (p) {
		case PlanetsLol.planets.jupiter:
			hValue = jupiterHValue;
			break;
		case PlanetsLol.planets.mars:
			hValue = marsHValue;
			break;
		case PlanetsLol.planets.mercury:
			hValue = mercuryHValue;
			break;
		case PlanetsLol.planets.neptune:
			hValue = neptuneHValue;
			break;
		case PlanetsLol.planets.saturn:
			hValue = saturnHValue;
			break;
		}
		return hValue;
	}

	void setColor (float hValue) {
		Color c = HsvToRgb(hValue / 360.0f, baseColorS / 255.0f, baseColorV / 255.0f);
		overlayRenderer.color = c; 
	}

	Color HsvToRgb(float h, float S, float V)
	{
		Debug.Log(h + " " + S + " " + V);
		// ######################################################################
		// T. Nathan Mundhenk
		// mundhenk@usc.edu
		// C/C++ Macro HSV to RGB
		float H = h;
		while (H < 0) { H += 360; };
		while (H >= 360) { H -= 360; };
		float R, G, B;
		if (V <= 0)
		{ R = G = B = 0; }
		else if (S <= 0)
		{
			R = G = B = V;
		}
		else
		{
			float hf = H / 60.0f;
			int i = (int)Mathf.Floor(hf);
			float f = hf - i;
			float pv = V * (1 - S);
			float qv = V * (1 - S * f);
			float tv = V * (1 - S * (1 - f));
			switch (i)
			{
				
				// Red is the dominant color
				
			case 0:
				R = V;
				G = tv;
				B = pv;
				break;
				
				// Green is the dominant color
				
			case 1:
				R = qv;
				G = V;
				B = pv;
				break;
			case 2:
				R = pv;
				G = V;
				B = tv;
				break;
				
				// Blue is the dominant color
				
			case 3:
				R = pv;
				G = qv;
				B = V;
				break;
			case 4:
				R = tv;
				G = pv;
				B = V;
				break;
				
				// Red is the dominant color
				
			case 5:
				R = V;
				G = pv;
				B = qv;
				break;
				
				// Just in case we overshoot on our math by a little, we put these here. Since its a switch it won't slow us down at all to put these here.
				
			case 6:
				R = V;
				G = tv;
				B = pv;
				break;
			case -1:
				R = V;
				G = pv;
				B = qv;
				break;
				
				// The color is not defined, we should throw an error.
				
			default:
				//LFATAL("i Value error in Pixel conversion, Value is %d", i);
				R = G = B = V; // Just pretend its black/white
				break;
			}
		}

		return new Color(R,G,B); 
	}
	
	/// <summary>
	/// Clamp a value to 0-255
	/// </summary>
	int Clamp(int i)
	{
		if (i < 0) return 0;
		if (i > 255) return 255;
		return i;
	}

}

