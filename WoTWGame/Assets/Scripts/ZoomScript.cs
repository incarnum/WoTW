using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Time.timeScale = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale += new Vector3((.05f * Time.deltaTime), (.05f * Time.deltaTime), (.05f * Time.deltaTime));
	}
}
