using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class PauseBGScript : MonoBehaviour {
	private bool changing;
	private Image im;
	private RectTransform rt;
	public Color targetColor;
	private float startTime;
	public float colorChangeTime;
	public float startingSizeMultiplier;
	private Vector2 startingSize;
	private Vector2 targetSize;
	public bool alsoBlur;
	// Use this for initialization
	void Awake () {
		im = GetComponent<Image> ();
		rt = GetComponent<RectTransform> ();
		targetColor = im.color;
		im.color = Color.clear;
		targetSize = rt.localScale;
		startingSize = new Vector2 (rt.localScale.x * startingSizeMultiplier, rt.localScale.y * startingSizeMultiplier);
		rt.localScale = startingSize;
		foreach (Transform child in transform) {
			child.GetComponent<Image>().enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (changing) {
			im.color = Color.Lerp (Color.clear, targetColor, ((Time.time - startTime) / colorChangeTime));
			rt.localScale = Vector2.Lerp (startingSize, targetSize, ((Time.time - startTime) / colorChangeTime));
			if (Time.time - startTime > colorChangeTime) {
				foreach (Transform child in transform) {
					child.GetComponent<Image>().enabled = true;
				}
				changing = false;
			}
		}
	}

	void OnEnable () {
		changing = true;
		startTime = Time.time;
		if (alsoBlur) {
			Camera.main.GetComponent<BlurOptimized> ().enabled = true;
		}
	}

	void OnDisable () {
		changing = false;
		foreach (Transform child in transform) {
			child.GetComponent<Image>().enabled = false;
			rt.localScale = startingSize;
			im.color = Color.clear;
			Camera.main.GetComponent<BlurOptimized> ().enabled = false;
		}
	}
}
