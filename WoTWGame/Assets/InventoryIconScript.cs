using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryIconScript : MonoBehaviour {
	private bool changing;
	private Image im;
	private RectTransform rt;
	public Color targetColor;
	private float startTime;
	public float colorChangeTime;
	public float startingSizeMultiplier;
	private Vector2 startingSize;
	private Vector2 targetSize;
	// Use this for initialization
	void Awake () {
		im = GetComponent<Image> ();
		rt = GetComponent<RectTransform> ();
		targetColor = im.color;
		im.color = Color.clear;
		targetSize = rt.localScale;
		startingSize = new Vector2 (rt.localScale.x * startingSizeMultiplier, rt.localScale.y * startingSizeMultiplier);
		rt.localScale = startingSize;
	}

	// Update is called once per frame
	void Update () {
		if (changing) {
			im.color = Color.Lerp (Color.clear, targetColor, ((Time.time - startTime) / colorChangeTime));
			rt.localScale = Vector2.Lerp (startingSize, targetSize, ((Time.time - startTime) / colorChangeTime));
			if (Time.time - startTime > colorChangeTime) {
				changing = false;
			}
		}
	}

	void OnEnable () {
		changing = true;
		startTime = Time.time;
	}

	void OnDisable () {
		changing = false;
	}
}
