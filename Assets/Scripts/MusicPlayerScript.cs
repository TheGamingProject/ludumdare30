using UnityEngine;
using System.Collections;

public class MusicPlayerScript : MonoBehaviour {
	
	AudioSource mainMusic;
	
	void Start () {
	}
	
	void Update () {
		if (mainMusic == null) {
			mainMusic = GetComponent<HashAudioScript>().GetAudio("pkfufinal");
		}
	}
}