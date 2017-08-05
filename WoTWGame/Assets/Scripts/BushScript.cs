﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushScript : MonoBehaviour {
	public GameObject berryPrefab;
	public GameObject corrBerryPrefab;
	public GameObject myBerry;
	public float nextReBerry;
	public float reBerryDelay;
	public bool isCorrupted;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextReBerry) {
			ReBerry ();
			nextReBerry = Mathf.Infinity;
		}
	}

	public void ReBerry() {
		if (isCorrupted == false) {
			GameObject newBerry = Instantiate (berryPrefab) as GameObject;
			myBerry = newBerry;
			newBerry.GetComponent<BerryScript> ().sourceBush = gameObject;
			newBerry.transform.position = gameObject.transform.position;
		} else {
			GameObject newBerry = Instantiate (corrBerryPrefab) as GameObject;
			myBerry = newBerry;
			newBerry.GetComponent<BerryScript> ().sourceBush = gameObject;
			newBerry.transform.position = gameObject.transform.position;
		}
	}
}
