using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sDeer1Script : MonoBehaviour {
	public int phase;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Player") {
			if (phase == 0) {
				GetComponent<AssignedAnimalMovementScript> ().MoveToNextSpot ();
			} else if (phase == 1) {
				GameObject.Find ("ScriptedEventManager").GetComponent<ScriptedEventManagerScript> ().NextEvent ();
			}
		}
	}
}
