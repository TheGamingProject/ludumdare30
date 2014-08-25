using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HashAudioScript : MonoBehaviour {
	private  Dictionary<string,AudioSource> hashMap;
	
	void Start () {
		hashMap = new  Dictionary<string,AudioSource>();
		
		AudioSource[] sounds = GetComponents<AudioSource>();
		for (int i=0;i<sounds.Length;i++) {
			//Debug.Log("added " + i);
			//Debug.Log(sounds[i].clip.name);
			hashMap.Add(sounds[i].clip.name, sounds[i]);
		}
	}
	
	void Update () {
		
	}
	
	public void PlayAudio(string soundName) {
		Debug.Log("Trying to play: " + soundName);
		
		if (hashMap.ContainsKey(soundName)) {
			AudioSource sound = (AudioSource) hashMap[soundName];
			sound.Play();
		} else {
			Debug.Log(soundName + " sound does not exist on this gameObject");
		}
	}
	
	public AudioSource GetAudio(string soundName) {
		if (!hashMap.ContainsKey(soundName)) 
			throw new UnityException("Missing sound on gameObject");
		return hashMap[soundName];
	}
}