using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class centerStoneGlowScript : MonoBehaviour {
	public float colorChangeTime;
	private SpriteRenderer sr;
	private bool changing;
	private float startTime;
	private Color targetColor;
	private Color startColor;
	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (changing) {
			sr.color = Color.Lerp (startColor, targetColor, ((Time.time - startTime) / colorChangeTime));
			if (Time.time - startTime > colorChangeTime) {
				changing = false;
			}
		}
	}

	public void SetColor (Color col, float changeTime) {
		startTime = Time.time;
		changing = true;
		startColor = sr.color;
		targetColor = col;
		colorChangeTime = changeTime;
	}
		
}
