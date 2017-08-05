using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterRingScript : MonoBehaviour {
	public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.D)) {
			transform.Rotate (-Vector3.forward * speed * Time.deltaTime);
		}

		if (Input.GetKey (KeyCode.A)) {
			transform.Rotate (Vector3.forward * speed * Time.deltaTime);
		}
	}
}
