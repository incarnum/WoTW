using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WildSpellbookManagerScript : MonoBehaviour {

	public GameObject spellPrefab;
	private Transform upperLeftBound;
	private Transform lowerRightBound;
	public int startingBookNum;
	// Use this for initialization
	void Start () {
		upperLeftBound = GetComponentsInChildren<Transform> ()[1];
		lowerRightBound = GetComponentsInChildren<Transform> ()[2];

		for (int i = 0; i < startingBookNum; i++) {
			CreateSpell ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CreateSpell() {
		int targ = Random.Range (0, 3);
		int eff = Random.Range (0, 3);
		int mod = Random.Range (0, 3);
		GameObject newSpell = Instantiate (spellPrefab) as GameObject;
		newSpell.GetComponent<PickupSpellbook> ().target = targ;
		newSpell.GetComponent<PickupSpellbook> ().effect = eff;
		if (mod == 0) {
			newSpell.GetComponent<PickupSpellbook> ().modifier = -1;
		} else if (mod == 1) {
			newSpell.GetComponent<PickupSpellbook> ().modifier = 0;
		} else if (mod == 2) {
			newSpell.GetComponent<PickupSpellbook> ().modifier = 1;
		}
		string spellPreviewString = "";
		if (eff == 0 && mod == 0) {
			spellPreviewString += "Shrink ";
		} else if (eff == 0 && mod == 1) {
			spellPreviewString += "Reset size of ";
		} else if (eff == 0 && mod == 2) {
			spellPreviewString += "Enlarge ";
		} else if (eff == 1 && mod == 0) {
			spellPreviewString += "Slow ";
		} else if (eff == 1 && mod == 1) {
			spellPreviewString += "Reset speed of ";
		} else if (eff == 1 && mod == 2) {
			spellPreviewString += "Hasten  ";
		} else if (eff == 2 && mod == 0) {
			spellPreviewString += "Weaken ";
		} else if (eff == 2 && mod == 1) {
			spellPreviewString += "Reset toughness of ";
		} else if (eff == 2 && mod == 2) {
			spellPreviewString += "Toughen ";
		}

		if (targ == 0) {
			spellPreviewString += "shrubs";
		} else if (targ == 1) {
			spellPreviewString += "deer";
		} else if (targ == 2) {
			spellPreviewString += "wolves";
		}

		newSpell.GetComponent<PickupSpellbook>().spellBookText = spellPreviewString;
		newSpell.transform.position = new Vector2 (Random.Range (upperLeftBound.position.x, lowerRightBound.position.x), Random.Range (upperLeftBound.position.y, lowerRightBound.position.y));
	}
}
