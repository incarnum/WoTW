using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barScript : MonoBehaviour {
	public GameObject barFill;
	public GameObject barTop;
	public GameObject barBottom;
	public Transform middleIcon;
	public float currentValue;
	private float height;
	// Use this for initialization
	void Start () {
		height = barTop.transform.position.y - barBottom.transform.position.y;
		UpdateFillSize (0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.V)) {
			UpdateFillSize (.1f);
		}
	}

	public void UpdateFillSize (float percent) {
		//Debug.Log ("fbewi");
		currentValue += percent;
		if (currentValue > 1) {
			currentValue = 1;
		} else if (currentValue < 0) {
			currentValue = 0;
		}
		barFill.transform.localScale = new Vector3(barFill.transform.localScale.x, currentValue, 1);
		barFill.transform.position = new Vector3(barFill.transform.position.x, barBottom.transform.position.y + (currentValue * height) / 2, 1f);
		UpdateIconPosition();
	}

	public void SetFillSizeValue (float percent) {
		currentValue = percent;
		if (currentValue > 1) {
			currentValue = 1;
		} else if (currentValue < 0) {
			currentValue = 0;
		}
		barFill.transform.localScale = new Vector3(barFill.transform.localScale.x, currentValue, 1);
		barFill.transform.position = new Vector3(barFill.transform.position.x, barBottom.transform.position.y + (currentValue * height) / 2, 1f);
		UpdateIconPosition ();
	}

	public void UpdateIconPosition() {
		if (middleIcon != null) {
			middleIcon.position = new Vector3 (middleIcon.position.x, barBottom.transform.position.y + (currentValue * height), 1f);
		}
	}
}
