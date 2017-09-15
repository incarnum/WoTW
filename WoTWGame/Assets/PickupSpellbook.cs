using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupSpellbook : MonoBehaviour {
	public GameObject spellPrefab;
	public int target;
	public int effect;
	public int modifier;
	public string spellBookText;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Player") {
			PickupSpell ();
			Destroy (gameObject);
		}
	}

	private void PickupSpell(){
		GameObject newSpell = Instantiate (spellPrefab) as GameObject;
		newSpell.GetComponent<SpellScript> ().target = target;
		newSpell.GetComponent<SpellScript> ().effect = effect;
		newSpell.GetComponent<SpellScript> ().strength = modifier;
		newSpell.GetComponentsInChildren<Text> () [0].text = spellBookText;
		PlaceSpell (newSpell);
	}

	private void PlaceSpell(GameObject spellbook) {
		if (GameObject.Find ("ABSlot1").GetComponent<SpellbookHolderScript> ().holding == null) {
			spellbook.transform.SetParent(GameObject.Find("ABSlot1").transform);
			spellbook.GetComponent<RectTransform> ().localPosition = Vector2.zero;
			spellbook.GetComponent<SpellScript> ().parentToReturnTo = GameObject.Find ("ABSlot1").transform;
			GameObject.Find ("ABSlot1").GetComponent<SpellbookHolderScript> ().holding = spellbook;
		} else if (GameObject.Find ("ABSlot2").GetComponent<SpellbookHolderScript> ().holding == null) {
			spellbook.transform.SetParent(GameObject.Find("ABSlot2").transform);
			spellbook.GetComponent<RectTransform> ().localPosition = Vector2.zero;
			spellbook.GetComponent<SpellScript> ().parentToReturnTo = GameObject.Find ("ABSlot2").transform;
			GameObject.Find ("ABSlot2").GetComponent<SpellbookHolderScript> ().holding = spellbook;
		} else if (GameObject.Find ("ABSlot3").GetComponent<SpellbookHolderScript> ().holding == null) {
			spellbook.transform.SetParent(GameObject.Find("ABSlot3").transform);
			spellbook.GetComponent<RectTransform> ().localPosition = Vector2.zero;
			spellbook.GetComponent<SpellScript> ().parentToReturnTo = GameObject.Find ("ABSlot3").transform;
			GameObject.Find ("ABSlot3").GetComponent<SpellbookHolderScript> ().holding = spellbook;
		} else if (GameObject.Find ("ABSlot4").GetComponent<SpellbookHolderScript> ().holding == null) {
			spellbook.transform.SetParent(GameObject.Find("ABSlot4").transform);
			spellbook.GetComponent<RectTransform> ().localPosition = Vector2.zero;
			spellbook.GetComponent<SpellScript> ().parentToReturnTo = GameObject.Find ("ABSlot4").transform;
			GameObject.Find ("ABSlot4").GetComponent<SpellbookHolderScript> ().holding = spellbook;
		} else if (GameObject.Find ("ABSlot5").GetComponent<SpellbookHolderScript> ().holding == null) {
			spellbook.transform.SetParent(GameObject.Find("ABSlot5").transform);
			spellbook.GetComponent<RectTransform> ().localPosition = Vector2.zero;
			spellbook.GetComponent<SpellScript> ().parentToReturnTo = GameObject.Find ("ABSlot5").transform;
			GameObject.Find ("ABSlot5").GetComponent<SpellbookHolderScript> ().holding = spellbook;
		} else if (GameObject.Find ("ABSlot6").GetComponent<SpellbookHolderScript> ().holding == null) {
			spellbook.transform.SetParent(GameObject.Find("ABSlot6").transform);
			spellbook.GetComponent<RectTransform> ().localPosition = Vector2.zero;
			spellbook.GetComponent<SpellScript> ().parentToReturnTo = GameObject.Find ("ABSlot6").transform;
			GameObject.Find ("ABSlot6").GetComponent<SpellbookHolderScript> ().holding = spellbook;
		}
	}
}
