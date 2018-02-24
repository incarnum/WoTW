using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeAudioScript : MonoBehaviour {
	public bool fadingOut;
	private float fadeStartTime;
	private float finishFadeTime;
	private float startVolume;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (fadingOut) {
			GetComponent<AudioSource> ().volume = startVolume - ((Time.time - fadeStartTime) / (finishFadeTime - fadeStartTime));
			if (Time.time > finishFadeTime) {
				fadingOut = false;
				GetComponent<AudioSource> ().Stop ();
			}
		}
			
	}

	public void beginFade(float duration) {
		fadingOut = true;
		fadeStartTime = Time.time;
		finishFadeTime = Time.time + duration;
		startVolume = GetComponent<AudioSource> ().volume;
	}
}
