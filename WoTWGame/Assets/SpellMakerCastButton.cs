using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellMakerCastButton : MonoBehaviour, IPointerClickHandler {
	public bool castable;
	public bool cleanse;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnPointerClick(PointerEventData eventData){
		if (castable && !cleanse) {
			GameObject.Find ("SpellMaker").GetComponent<SpellMakerScript> ().Cast ();
		}
		if (castable && cleanse) {
			GameObject.Find ("SpellMaker").GetComponent<SpellMakerScript> ().CastCleanse ();
		}
	}
}
