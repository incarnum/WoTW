using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonTextBGScript : MonoBehaviour {
	public Transform left;
	public Transform right;
	public Transform center;
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
		center.localScale = new Vector2 (2f * size, 1f);
		left.localPosition = new Vector2 (-size, 0f);
		right.localPosition = new Vector2 (size, 0f);
	}
}
