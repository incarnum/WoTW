using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceMasterScript : MonoBehaviour {
	public GameObject holding;
	public GameObject targetDrop;

	public GameObject spellbookHolding;
	public GameObject spellbookTargetDrop;
	public int spellCastCounter;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (1)) {
			GameObject.Find ("SpellAbility1").GetComponent<Image> ().color = Color.white;
			GameObject.Find ("SpellAbility2").GetComponent<Image> ().color = Color.white;
			GameObject.Find ("SpellAbility3").GetComponent<Image> ().color = Color.white;
			GameObject.Find ("SpellAbility4").GetComponent<Image> ().color = Color.white;
			GameObject.Find ("SpellAbility5").GetComponent<Image> ().color = Color.white;
			GameObject.Find ("SpellAbility6").GetComponent<Image> ().color = Color.white;
			spellbookHolding = null;
		}
	}
		
}
