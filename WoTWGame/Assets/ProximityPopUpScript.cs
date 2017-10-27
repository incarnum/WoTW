using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityPopUpScript : MonoBehaviour {
	public bool isenabled;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Player" && isenabled) {
			GetComponent<SpriteRenderer> ().enabled = true;
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			GetComponent<SpriteRenderer> ().enabled = false;
		}
	}
}
