using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSourceScript : MonoBehaviour {

	public int ingType;
	public GameObject ingredientPrefab;
	private GameObject player;
	public GameObject ingInfo;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseEnter() {
		ingInfo.GetComponent<Animator> ().SetTrigger (ingType.ToString ());
	}

	void OnMouseExit() {
		ingInfo.GetComponent<Animator> ().SetTrigger ("none");
	}

	void OnMouseDown() {
		if (ingType == 0 && player.GetComponent<InventoryScript> ().berryNum > 0) {
			CreateIngredient ();
			player.GetComponent<InventoryScript> ().berryNum -= 1;
			player.GetComponent<InventoryScript> ().UpdateNumbers ();
		} else if (ingType == 1 && player.GetComponent<InventoryScript> ().antlerNum > 0) {
			CreateIngredient ();
			player.GetComponent<InventoryScript> ().antlerNum -= 1;
			player.GetComponent<InventoryScript> ().UpdateNumbers ();
		} else if (ingType == 2 && player.GetComponent<InventoryScript> ().fangNum > 0) {
			CreateIngredient ();
			player.GetComponent<InventoryScript> ().fangNum -= 1;
			player.GetComponent<InventoryScript> ().UpdateNumbers ();
		} else if (ingType == 3 && player.GetComponent<InventoryScript> ().corrBerryNum > 0) {
			CreateIngredient ();
			player.GetComponent<InventoryScript> ().corrBerryNum -= 1;
			player.GetComponent<InventoryScript> ().UpdateNumbers ();
		} else if (ingType == 4 && player.GetComponent<InventoryScript> ().corrAntlerNum > 0) {
			CreateIngredient ();
			player.GetComponent<InventoryScript> ().corrAntlerNum -= 1;
			player.GetComponent<InventoryScript> ().UpdateNumbers ();
		} else if (ingType == 5 && player.GetComponent<InventoryScript> ().corrFangNum > 0) {
			CreateIngredient ();
			player.GetComponent<InventoryScript> ().corrFangNum -= 1;
			player.GetComponent<InventoryScript> ().UpdateNumbers ();
		}
	}

	public void CreateIngredient() {
		GameObject newIng = Instantiate (ingredientPrefab) as GameObject;
		newIng.transform.position = transform.position;
		player.GetComponent<PlaceMasterScript> ().holding = newIng;
	}
}
