using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpellbookHolderScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler{
	private GameObject player;
	public GameObject holding;
	public int hotkey;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}

	// Update is called once per frame
	void Update () {
		if (hotkey == 1 && Input.GetKeyDown(KeyCode.Alpha1)){
			if (holding != null) {
				holding.GetComponent<SpellScript> ().Cast();
				//Destroy (holding);
				//holding = null;
			}
		}
		if (hotkey == 2 && Input.GetKeyDown(KeyCode.Alpha2)){
			if (holding != null) {
				holding.GetComponent<SpellScript> ().Cast();
				//Destroy (holding);
				//holding = null;
			}
		}
		if (hotkey == 3 && Input.GetKeyDown(KeyCode.Alpha3)){
			if (holding != null) {
				holding.GetComponent<SpellScript> ().Cast();
				//Destroy (holding);
				//holding = null;
			}
		}
		if (hotkey == 4 && Input.GetKeyDown(KeyCode.Alpha4)){
			if (holding != null) {
				holding.GetComponent<SpellScript> ().Cast();
				//Destroy (holding);
				//holding = null;
			}
		}
		if (hotkey == 5 && Input.GetKeyDown(KeyCode.Alpha5)){
			if (holding != null) {
				holding.GetComponent<SpellScript> ().Cast();
				//Destroy (holding);
				//holding = null;
			}
		}
		if (hotkey == 6 && Input.GetKeyDown(KeyCode.Alpha6)){
			if (holding != null) {
				holding.GetComponent<SpellScript> ().Cast();
				//Destroy (holding);
				//holding = null;
			}
		}
	}

	public void OnPointerEnter (PointerEventData eventData) {
		player.GetComponent<PlaceMasterScript> ().spellbookTargetDrop = gameObject;
	}

	public void OnPointerExit (PointerEventData eventData) {
		player.GetComponent<PlaceMasterScript> ().spellbookTargetDrop = null;
	}

	public void OnDrop (PointerEventData eventData) {
		player.GetComponent<PlaceMasterScript> ().spellbookTargetDrop = null;
		SpellScript s = eventData.pointerDrag.GetComponent<SpellScript> ();
		if (s != null) {
			if (s.parentToReturnTo != null) {
				s.parentToReturnTo.GetComponent<SpellbookHolderScript> ().holding = null;
				Debug.Log ("replaced");
			}
			s.parentToReturnTo = this.transform;
			holding = eventData.pointerDrag;
		}
	}
}
