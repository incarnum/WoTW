using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour {
	private CreatureManagerScript creatureManager;
	// Use this for initialization
	public int pickUpType;
	void Start () {
		creatureManager = GameObject.Find ("CreatureManager").GetComponent<CreatureManagerScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Player") {
			Destroy (gameObject);
			if (pickUpType == 1) {
				coll.GetComponent<InventoryScript> ().antlerNum += 1;
			} else if (pickUpType == 2) {
				coll.GetComponent<InventoryScript> ().fangNum += 1;
			} else if (pickUpType == 0) {
				coll.GetComponent<InventoryScript> ().berryNum += 1;
			} else if (pickUpType == 3) {
				coll.GetComponent<InventoryScript> ().corrBerryNum += 1;
			} else if (pickUpType == 4) {
				coll.GetComponent<InventoryScript> ().corrAntlerNum += 1;
			} else if (pickUpType == 5) {
				coll.GetComponent<InventoryScript> ().corrFangNum += 1;
			}
			coll.GetComponent<InventoryScript> ().UpdateNumbers ();

		}
	}

	void OnDestroy() {
		if (pickUpType == 1) {
			creatureManager.antlerList.Remove (gameObject);
		} else if (pickUpType == 2) {
			creatureManager.fangList.Remove (gameObject);
		} else if (pickUpType == 3) {
			//creatureManager.corruptedBerryList.Remove (gameObject);
//		} else if (pickUpType == 3) {
//			if (creatureManager.corruptedAntlerList.Contains(gameObject)) {
//			creatureManager.corruptedAntlerList.Remove (gameObject);
//			}
//		} else if (pickUpType == 3) {
//			if (creatureManager.corruptedAntlerList.Contains (gameObject)) {
//				creatureManager.corruptedFangList.Remove (gameObject);
//			}
		}
	}
}
