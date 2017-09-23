using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class corruptionManagerScript : MonoBehaviour {
	public float corruptTime;
	private float nextCorruptionTime;
	private SimpleEcologyMasterScript ecoManager;
	// Use this for initialization
	void Start () {
		nextCorruptionTime = Time.time + corruptTime;
		ecoManager = GameObject.Find ("SimpleEcologyMaster").GetComponent<SimpleEcologyMasterScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if (nextCorruptionTime <= Time.time) {
			Corrupt ();
			nextCorruptionTime = Time.time + corruptTime;
		}
	}

	void Corrupt() {
		int rando = Random.Range (0, 3);
		if (rando == 0) {
			ecoManager.corruptingShrubs = true;
			if (ecoManager.corruptedShrubPop < 10 && ecoManager.shrubPop > 20) {
				ecoManager.corruptedShrubPop = 10f;
			}
		} else if (rando == 1) {
			ecoManager.corruptingDeer = true;
			if (ecoManager.corruptedDeerPop < 10 && ecoManager.deerPop > 20) {
				ecoManager.corruptedDeerPop = 10f;
			}
		} else if (rando == 2) {
			ecoManager.corruptingWolves = true;
			if (ecoManager.corruptedWolfPop < 10 && ecoManager.wolfPop > 20) {
				ecoManager.corruptedWolfPop = 10f;
			}
		}
	}
}
