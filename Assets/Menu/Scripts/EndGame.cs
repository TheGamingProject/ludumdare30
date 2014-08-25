using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {
	
	// Use this for initialization
	public GUISkin menu;

	public float killCount;.


	
	public Texture backgroundTexture;
	
	public string button1Text;
	public string button2Text;
	public string button3Text;
	public string button4Text;
	
	public float button1X;
	public float button1Y;
	public float button1W;
	public float button1H;

	public float button2X;
	public float button2Y;
	public float button2W;
	public float button2H;

	public float button3X;
	public float button3Y;
	public float button3W;
	public float button3H;

	public float button4X;
	public float button4Y;
	public float button4W;
	public float button4H;
	
	void OnGUI(){
		
		GUI.skin = menu;
		
		GUI.DrawTexture (new Rect (((Screen.width/2)-((Screen.height*16)/9)/2), 0, (Screen.height*16)/9, Screen.height), backgroundTexture);

		if ( 
		    GUI.Button(
			new Rect(
			button1X,
			button1Y,
			button1W,
			button1H
			),
			button1Text
			) 
		    ) 
		{
			Application.OpenURL("http://www.facebook.com/sharer/sharer.php?u=http%3A%2F%2Fwww.tgp.io%2Fwebgames%2Fludumdare30&t=I+struck+down+"+killCount+"+foes+with+my+Planetary+Kung+Fu.+Think+you+can+best+me?"); //link to twitter
		}

		if ( 
		    GUI.Button(
			new Rect(
			button2X,
			button2Y,
			button2W,
			button2H
			),
			button2Text
			) 
		    ) 
		{
			Application.OpenURL("https://twitter.com/intent/tweet?text=I struck down "+killCount+" foes with my Planetary Kung Fu. Think you can best me?&hashtags=PlanetKungFu&url=http%3A%2F%2Fwww.tgp.io%2Fwebgames%2Fludumdare30"); //link to facebook
		}

		if ( 
		    GUI.Button(
			new Rect(
			button3X,
			button3Y,
			button3W,
			button3H
			),
			button3Text
			) 
		    ) 
		{
			Application.LoadLevel("startmenu");
		}

		if ( 
		    GUI.Button(
			new Rect(
			button4X,
			button4Y,
			button4W,
			button4H
			),
			button4Text
			) 
		    ) 
		{
			Application.LoadLevel("mainmenu");
		}
	}
}
