using UnityEngine;
using System.Collections;

public class SpellSlotScript : MonoBehaviour {
	private GameObject player;
	public int slotType;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseEnter () {
		if (player.GetComponent<PlayerPlaceScript> ().holdingObj != null) {
			if (slotType == 1) {
				//player.GetComponent<PlayerPlaceScript> ().holdingObj.GetComponent<Animator> ().SetTrigger ("Target");
			} else if (slotType == 2) {
				//player.GetComponent<PlayerPlaceScript> ().holdingObj.GetComponent<Animator> ().SetTrigger ("Effect");
			} else if (slotType == 3) {
				//player.GetComponent<PlayerPlaceScript> ().holdingObj.GetComponent<Animator> ().SetTrigger ("Modifier");
			}
		}
	}

	void OnMouseExit () {
		if (player.GetComponent<PlayerPlaceScript> ().holdingObj != null) {
			//player.GetComponent<PlayerPlaceScript> ().holdingObj.GetComponent<Animator> ().SetTrigger ("Normal");
		}
	}
}
