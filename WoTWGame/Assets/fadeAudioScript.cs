using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadeAudioScript : MonoBehaviour {
	public bool fadingOut;
	private float fadeStartTime;
	private float finishFadeTime;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (fadingOut) {
			GetComponent<AudioSource> ().volume = 1 - ((Time.time - fadeStartTime) / (finishFadeTime - fadeStartTime));
		}
			
	}

	public void beginFade(float duration) {
		fadingOut = true;
		fadeStartTime = Time.time;
		finishFadeTime = Time.time + duration;
	}
}
