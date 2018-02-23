using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGlowScript : MonoBehaviour {
	public float colorChangeTime;
	private Image im;
	private bool changing;
	private float startTime;
	private Color targetColor;
	private Color startColor;
	// Use this for initialization
	void Start () {
		im = GetComponent<Image> ();
	}

	// Update is called once per frame
	void Update () {
		if (changing) {
			im.color = Color.Lerp (startColor, targetColor, ((Time.time - startTime) / colorChangeTime));
			if (Time.time - startTime > colorChangeTime) {
				changing = false;
			}
		}
	}

	public void SetColor (Color col, float changeTime) {
		startTime = Time.time;
		changing = true;
		startColor = im.color;
		targetColor = col;
		colorChangeTime = changeTime;
	}

}
