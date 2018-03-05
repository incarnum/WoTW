using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCircleTimeStop : MonoBehaviour {
	private SimpleEcologyMasterScript eco;
	private corruptionManagerScript cm;
	private TimeStopCanvas tsc;
	// Use this for initialization
	void Start () {
		eco = GameObject.Find ("SimpleEcologyMaster").GetComponent<SimpleEcologyMasterScript> ();
		cm = GameObject.Find ("CorruptionManager").GetComponent<corruptionManagerScript> ();
		tsc = GameObject.Find ("TimeStopCanvas").GetComponent<TimeStopCanvas> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "Player") {
			eco.areaTimeStop = true;
			cm.TimeStopped ();
			tsc.areaStop = true;
			tsc.CheckVisibility ();
		}
	}

	void OnTriggerExit2D (Collider2D col) {
		if (col.tag == "Player") {
			eco.areaTimeStop = false;
			cm.TimeResumed ();
			tsc.areaStop = false;
			tsc.CheckVisibility ();
		}
	}
}
