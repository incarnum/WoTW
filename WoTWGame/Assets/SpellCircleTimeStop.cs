using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCircleTimeStop : MonoBehaviour {
	private SimpleEcologyMasterScript eco;
	private corruptionManagerScript cm;
	// Use this for initialization
	void Start () {
		eco = GameObject.Find ("SimpleEcologyMaster").GetComponent<SimpleEcologyMasterScript> ();
		cm = GameObject.Find ("CorruptionManager").GetComponent<corruptionManagerScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "Player") {
			eco.areaTimeStop = true;
			cm.TimeStopped ();
		}
	}

	void OnTriggerExit2D (Collider2D col) {
		if (col.tag == "Player") {
			eco.areaTimeStop = false;
			cm.TimeResumed ();
		}
	}
}
