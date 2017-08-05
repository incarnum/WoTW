using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlaceScript : MonoBehaviour {
	public bool holdingBool;
	public GameObject holdingObj;
	public GameObject placeSpot;
	public bool canPlace;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonUp(0)) {
			if (holdingBool) {

				if (canPlace) {
					placeSpot.GetComponent<PlaceSpotScript> ().holdingObj = holdingObj;
					placeSpot.GetComponent<PlaceSpotScript> ().holdingBool = true;
					holdingObj.GetComponent<Transform> ().position = new Vector3(placeSpot.GetComponent<Transform> ().position.x, placeSpot.GetComponent<Transform> ().position.y, -.2f);
					holdingObj.GetComponent<Transform> ().SetParent(placeSpot.GetComponent<Transform> ());

					if (holdingObj.GetComponent<CardScript> ().placeSpot.GetComponent<PlaceSpotScript> ()) {
						holdingObj.GetComponent<CardScript> ().placeSpot.GetComponent<PlaceSpotScript> ().holdingObj = null;
						holdingObj.GetComponent<CardScript> ().placeSpot.GetComponent<PlaceSpotScript> ().holdingBool = false;
					}

					holdingObj.GetComponent<CardScript> ().beingMoved = false;
		

					holdingObj.GetComponent<CardScript> ().placeSpot = placeSpot;
					holdingBool = false;
					holdingObj = null;
				} else {
					//holdingObj.GetComponent<Transform> ().position = new Vector3(holdingObj.GetComponent<CardScript>().placeSpot.GetComponent<Transform> ().position.x, holdingObj.GetComponent<CardScript>().placeSpot.GetComponent<Transform> ().position.y, -.2f);
					//holdingObj.GetComponent<CardScript> ().beingMoved = false;

					if (holdingObj.GetComponent<CardScript> ().placeSpot.GetComponent<PlaceSpotScript> ()) {
						holdingObj.GetComponent<CardScript> ().placeSpot.GetComponent<PlaceSpotScript> ().holdingObj = null;
						holdingObj.GetComponent<CardScript> ().placeSpot.GetComponent<PlaceSpotScript> ().holdingBool = false;
					}
					Destroy(holdingObj);
					holdingBool = false;
					holdingObj = null;
				}
			}
		}




	}
}
