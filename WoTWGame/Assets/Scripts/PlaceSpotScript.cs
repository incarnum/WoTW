using UnityEngine;
using System.Collections;

public class PlaceSpotScript : MonoBehaviour {
	private GameObject player;
	public bool holdingBool;
	public GameObject holdingObj;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseUp () {
		if (player.GetComponent<PlayerPlaceScript> ().holdingBool == true) {
			holdingObj = player.GetComponent<PlayerPlaceScript> ().holdingObj;
			player.GetComponent<PlayerPlaceScript> ().holdingBool = false;
			holdingObj.GetComponent<Transform> ().position = transform.position;
		}
	}

	void OnMouseEnter () {
		if (holdingBool == false) {
			player.GetComponent<PlayerPlaceScript> ().canPlace = true;
			player.GetComponent<PlayerPlaceScript> ().placeSpot = gameObject;
		}
	}

	void OnMouseExit () {
		player.GetComponent<PlayerPlaceScript> ().canPlace = false;
	}
}
