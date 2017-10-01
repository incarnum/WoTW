using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadeInScreenScript : MonoBehaviour {
	public bool fadingIn;
	private float fadeStartTime;
	private float finishFadeTime;
	private Color baseColor;
	public float duration;
	private bool paused;
	public float pauseDuration;
	private float unPauseTime;
	// Use this for initialization
	void Start () {
		paused = true;
		unPauseTime = Time.time + pauseDuration;
	}

	void Awake () {
		baseColor = GetComponent<Image> ().color;
	}

	// Update is called once per frame
	void Update () {
		if (paused) {
			if (Time.time >= unPauseTime) {
				paused = false;
				beginFade ();
			}
		}

		if (fadingIn) {
			Color newCol = new Color (baseColor.r, baseColor.g, baseColor.b, 1- ((Time.time - fadeStartTime) / (finishFadeTime - fadeStartTime)));
			GetComponent<Image> ().color = newCol;
		}

	}

	public void beginFade() {
		fadingIn = true;
		fadeStartTime = Time.time;
		finishFadeTime = Time.time + duration;
	}
}
