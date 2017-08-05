using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerRingScript : MonoBehaviour {
	public float speed;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.Rotate (-Vector3.forward * speed * Time.deltaTime);
		}

		if (Input.GetKey (KeyCode.LeftArrow)) {
			transform.Rotate (Vector3.forward * speed * Time.deltaTime);
		}
	}
}
