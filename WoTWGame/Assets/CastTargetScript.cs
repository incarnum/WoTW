using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastTargetScript : MonoBehaviour {
	private GameObject player;
	public int animalType;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown () {
		if (player.GetComponent<PlaceMasterScript> ().spellbookHolding != null) {
			Debug.Log (gameObject);
			player.GetComponent<PlaceMasterScript> ().spellbookHolding.GetComponent<SpellScriptJTest> ().target = animalType;
			player.GetComponent<PlaceMasterScript> ().spellbookHolding.GetComponent<SpellScriptJTest> ().Cast ();
		}
	}
}
