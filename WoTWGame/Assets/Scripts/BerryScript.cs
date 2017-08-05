using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryScript : MonoBehaviour {
	public GameObject sourceBush;
	// Use this for initialization
	void Start () {
		Destroy (gameObject, 20);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

//	void OnTriggerEnter2D(Collider2D coll) {
//
//	}

	void OnDestroy() {
		if (sourceBush != null) {
			sourceBush.GetComponent<BushScript> ().nextReBerry = Time.time + sourceBush.GetComponent<BushScript> ().reBerryDelay;
		}
	}
}
