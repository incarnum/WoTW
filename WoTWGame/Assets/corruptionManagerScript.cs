using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class corruptionManagerScript : MonoBehaviour {
	public float infectTime;
	public float minimumInfectionPop;
	private float nextCorruptionTime;
	private SimpleEcologyMasterScript ecoManager;
	// Use this for initialization
	void Start () {
		nextCorruptionTime = Time.time + infectTime;
		ecoManager = GameObject.Find ("SimpleEcologyMaster").GetComponent<SimpleEcologyMasterScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if (nextCorruptionTime <= Time.time) {
			Corrupt ();
			nextCorruptionTime = Time.time + infectTime;
		}
	}

	void Corrupt() {
		if (!ecoManager.corruptingShrubs && !ecoManager.corruptingDeer && !ecoManager.corruptingWolves) {
			int rando = Random.Range (0, 3);
			if (rando == 0) {
				ecoManager.corruptingShrubs = true;
				if (ecoManager.corruptedShrubPop < 10 && ecoManager.shrubPop > minimumInfectionPop) {
					ecoManager.corruptedShrubPop = 10f;
				}
			} else if (rando == 1) {
				ecoManager.corruptingDeer = true;
				if (ecoManager.corruptedDeerPop < 10 && ecoManager.deerPop > minimumInfectionPop) {
					ecoManager.corruptedDeerPop = 10f;
				}
			} else if (rando == 2) {
				ecoManager.corruptingWolves = true;
				if (ecoManager.corruptedWolfPop < 10 && ecoManager.wolfPop > minimumInfectionPop) {
					ecoManager.corruptedWolfPop = 10f;
				}
			}
		}
	}
}
