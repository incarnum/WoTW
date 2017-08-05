using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientHolderScript : MonoBehaviour {
	private GameObject player;
	public GameObject holding;
	public int accepting;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		accepting = 6;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseEnter () {
		player.GetComponent<PlaceMasterScript> ().targetDrop = gameObject;
	}

	void OnMouseExit () {
		player.GetComponent<PlaceMasterScript> ().targetDrop = null;
	}
}
