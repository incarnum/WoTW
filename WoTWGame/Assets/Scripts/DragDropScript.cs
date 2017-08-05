using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropScript : MonoBehaviour {
	
	public bool held;
	private Vector3 handposition;
	public GameObject targetDrop;
	public GameObject player;
	public int objectType;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		held = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (held) {
			MoveToCursor ();
		}

		if (Input.GetMouseButtonUp (0)) {
			if (player.GetComponent<PlaceMasterScript> ().targetDrop != null && held == true) {
				if (player.GetComponent<PlaceMasterScript> ().targetDrop.GetComponent<IngredientHolderScript> ().accepting == 6
				    || player.GetComponent<PlaceMasterScript> ().targetDrop.GetComponent<IngredientHolderScript> ().accepting == objectType) {
					Place ();
				} else {
					ReturnIng ();
				}
			} else if (held == true) {
				ReturnIng ();
			}
		}
	}

	void OnMouseDown() {
		held = true;
		player.GetComponent<PlaceMasterScript> ().holding = gameObject;
	}

//	void OnMouseUp() {
//		if (player.GetComponent<PlaceMasterScript> ().targetDrop != null && held == true) {
//			Place ();
//		}
//	}


	void OnMouseOver(){
		if (Input.GetMouseButtonDown (1)) {
			if (objectType == 0) {
				player.GetComponent<InventoryScript> ().berryNum += 1;
			} else if (objectType == 1) {
				player.GetComponent<InventoryScript> ().antlerNum += 1;
			} else if (objectType == 2) {
				player.GetComponent<InventoryScript> ().fangNum += 1;
			} else if (objectType == 3) {
				player.GetComponent<InventoryScript> ().corrBerryNum += 1;
			} else if (objectType == 4) {
				player.GetComponent<InventoryScript> ().corrAntlerNum += 1;
			} else if (objectType == 5) {
				player.GetComponent<InventoryScript> ().corrFangNum += 1;
			}
			player.GetComponent<InventoryScript> ().UpdateNumbers ();
			Destroy (gameObject);
		}
	}

	void MoveToCursor() {
		Vector3 bla = new Vector3 (0, 0, 0);
		handposition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		handposition += bla;
		transform.position = new Vector3(handposition.x, handposition.y, 10);
	}

	void Place() {
		if (targetDrop != null) {
			targetDrop.GetComponent<IngredientHolderScript> ().holding = null;
		}
		targetDrop = player.GetComponent<PlaceMasterScript> ().targetDrop;
		held = false;
		transform.position = new Vector3 (targetDrop.transform.position.x, targetDrop.transform.position.y, -1);
		targetDrop.GetComponent<IngredientHolderScript> ().holding = gameObject;
		//GameObject.Find ("SpellMenu").GetComponent<SpellMenuScript> ().CheckNodes ();
		player.GetComponent<PlaceMasterScript> ().targetDrop = null;
		Debug.Log ("Placed");
	}

	void ReturnIng() {
//		if (targetDrop != null) {
//			transform.position = new Vector3 (targetDrop.transform.position.x, targetDrop.transform.position.y, -1);
//			held = false;
//			Debug.Log ("Returned");
//		} else {
			if (objectType == 0) {
				player.GetComponent<InventoryScript> ().berryNum += 1;
			} else if (objectType == 1) {
				player.GetComponent<InventoryScript> ().antlerNum += 1;
			} else if (objectType == 2) {
				player.GetComponent<InventoryScript> ().fangNum += 1;
			} else if (objectType == 3) {
				player.GetComponent<InventoryScript> ().corrBerryNum += 1;
			} else if (objectType == 4) {
				player.GetComponent<InventoryScript> ().corrAntlerNum += 1;
			} else if (objectType == 5) {
				player.GetComponent<InventoryScript> ().corrFangNum += 1;
			}
			player.GetComponent<InventoryScript> ().UpdateNumbers ();
			Destroy (gameObject);
		//}
	}
}
