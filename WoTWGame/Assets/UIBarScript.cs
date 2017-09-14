using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBarScript : MonoBehaviour {
	public float value;
	public GameObject barFill;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		barFill.GetComponent<RectTransform> ().localScale = new Vector2 (barFill.GetComponent<RectTransform> ().localScale.x, value);
	}

	public void UpdateFillSize (float percent) {
		value += percent;
		if (value > 1) {
			value = 1;
		} else if (value < 0) {
			value = 0;
		}
		barFill.GetComponent<RectTransform> ().localScale = new Vector2 (barFill.GetComponent<RectTransform> ().localScale.x, value);
	}

	public void SetFillSizeValue (float percent) {
		value = percent;
		if (value > 1) {
			value = 1;
		} else if (value < 0) {
			value = 0;
		}
		barFill.GetComponent<RectTransform> ().localScale = new Vector2 (barFill.GetComponent<RectTransform> ().localScale.x, value);
	}
}
