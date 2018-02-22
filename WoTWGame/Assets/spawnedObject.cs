using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnedObject : MonoBehaviour {
	public Transform spawnLocation;
	private CreatureManagerScript cm;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDestroy() {
		if (spawnLocation != null)
		spawnLocation.gameObject.SetActive (true);
	}
}
