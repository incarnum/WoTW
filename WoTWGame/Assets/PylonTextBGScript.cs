using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PylonTextBGScript : MonoBehaviour {
	public RectTransform textBG1;
	public RectTransform textBG2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			AdjustSize (1);
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			AdjustSize (2);
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			AdjustSize (3);
		}
		if (Input.GetKeyDown (KeyCode.Alpha4)) {
			AdjustSize (4);
		}
	}

	public void AdjustSize(float size) {
		textBG1.sizeDelta = new Vector2 (size * 30 + 100f, textBG1.sizeDelta.y);
		textBG2.sizeDelta = new Vector2 (size * 30 + 175f, textBG2.sizeDelta.y);
	}
}
